﻿/* 
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Domain.Data.Interfaces;

namespace Domain.Data
{
    /// <summary>
    ///     As its name suggests, DbContextCollection maintains a collection of DbContext instances.
    ///     What it does in a nutshell:
    ///     - Lazily instantiates DbContext instances when its Get Of TDbContext () method is called
    ///     (and optionally starts an explicit database transaction).
    ///     - Keeps track of the DbContext instances it created so that it can return the existing
    ///     instance when asked for a DbContext of a specific type.
    ///     - Takes care of committing / rolling back changes and transactions on all the DbContext
    ///     instances it created when its Commit() or Rollback() method is called.
    /// </summary>
    public class DbContextCollection : IDbContextCollection
    {
        private readonly bool _autoDetectChanges;
        private readonly IDbContextFactory _dbContextFactory;
        //  [ThreadStatic]
        private readonly Dictionary<Tuple<Type, string>, DbContext> _initializedDbContexts;
        private readonly bool _readOnly;
        private readonly Dictionary<DbContext, DbContextTransaction> _transactions;
        private bool _completed;
        private bool _disposed;
        private IsolationLevel? _isolationLevel;

        public DbContextCollection(bool readOnly = false,
                                   IsolationLevel? isolationLevel = null,
                                   bool autoDetectChanges = true,
                                   IDbContextFactory dbContextFactory = null)
        {
            _disposed = false;
            _completed = false;

            _initializedDbContexts = new Dictionary<Tuple<Type, string>, DbContext>();
            _transactions = new Dictionary<DbContext, DbContextTransaction>();

            _readOnly = readOnly;
            _isolationLevel = isolationLevel;
            _autoDetectChanges = autoDetectChanges;
            _dbContextFactory = dbContextFactory;
        }

        internal Dictionary<Tuple<Type, string>, DbContext> InitializedDbContexts
        {
            get { return _initializedDbContexts; }
        }

        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            if (_disposed)
                throw new ObjectDisposedException("DbContextCollection");

            var requestedType = typeof (TDbContext);

            if (!FindMatchingContext(requestedType))
            {
                // First time we've been asked for this particular DbContext type.
                // Create one, cache it and start its database transaction if needed.
                var dbContext = _dbContextFactory != null
                    ? _dbContextFactory.CreateDbContext<TDbContext>()
                    : Activator.CreateInstance<TDbContext>();

                _initializedDbContexts.Add(new Tuple<Type, string>(requestedType,
                    _dbContextFactory == null ? null : _dbContextFactory.ConnectionString),
                    dbContext);

                dbContext.Configuration.AutoDetectChangesEnabled = !_readOnly && _autoDetectChanges;

                if (_isolationLevel.HasValue)
                {
                    var tran = dbContext.Database.BeginTransaction(_isolationLevel.Value);
                    _transactions.Add(dbContext, tran);
                }
            }

            return _initializedDbContexts[new Tuple<Type, string>(requestedType,
                _dbContextFactory == null ? null : _dbContextFactory.ConnectionString)] as TDbContext;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            // Do our best here to dispose as much as we can even if we get errors along the way.
            // Now is not the time to throw. Correctly implemented applications will have called
            // either Commit() or Rollback() first and would have got the error there.

            if (!_completed)
            {
                try
                {
                    if (_readOnly)
                        Commit();
                    else
                        Rollback();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            foreach (var dbContext in _initializedDbContexts.Values.ToList())
            {
                try
                {
                    dbContext.Dispose();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            _initializedDbContexts.Clear();
            _disposed = true;
        }

        public int Commit()
        {
            if (_disposed)
                throw new ObjectDisposedException("DbContextCollection");
            if (_completed)
                throw new InvalidOperationException(
                    "You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");

            // Best effort. You'll note that we're not actually implementing an atomic commit 
            // here. It entirely possible that one DbContext instance will be committed successfully
            // and another will fail. Implementing an atomic commit would require us to wrap
            // all of this in a TransactionScope. The problem with TransactionScope is that 
            // the database transaction it creates may be automatically promoted to a 
            // distributed transaction if our DbContext instances happen to be using different 
            // databases. And that would require the DTC service (Distributed Transaction Coordinator)
            // to be enabled on all of our live and dev servers as well as on all of our dev workstations.
            // Otherwise the whole thing would blow up at runtime. 

            // In practice, if our services are implemented following a reasonably DDD approach,
            // a business transaction (i.e. a service method) should only modify entities in a single
            // DbContext. So we should never find ourselves in a situation where two DbContext instances
            // contain uncommitted changes here. We should therefore never be in a situation where the below
            // would result in a partial commit. 

            ExceptionDispatchInfo lastError = null;

            var c = 0;
            //var abc = "";
            foreach (var dbContext in _initializedDbContexts.Values.ToList())
            {
                try
                {
                    if (!_readOnly)
                    {
                        c += dbContext.SaveChanges();
                    }

                    // If we've started an explicit database transaction, time to commit it now.
                    var tran = GetValueOrDefault(_transactions, dbContext);
                    if (tran != null)
                    {
                        tran.Commit();
                        tran.Dispose();
                    }
                }
                    //catch (DbEntityValidationException dbEx)
                    //{
                    //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    //    {
                    //        foreach (var validationError in validationErrors.ValidationErrors)
                    //        {
                            
                    //            var xyz = string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    //            abc = !string.IsNullOrEmpty(abc) ? xyz + " HHH " : xyz;
                    //             // Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    //            lastError = ExceptionDispatchInfo.Capture(dbEx);
                    //        }
                    //    }
                    //}
                catch (Exception e)
                {
                    lastError = ExceptionDispatchInfo.Capture(e);
                }
            }

            _transactions.Clear();
            _completed = true;

            if (lastError != null)
                lastError.Throw(); // Re-throw while maintaining the exception's original stack track

            return c;
        }

        // ReSharper disable once UnusedMember.Global
        public Task<int> CommitAsync()
        {
            return CommitAsync(CancellationToken.None);
        }

        public async Task<int> CommitAsync(CancellationToken cancelToken)
        {
            if (cancelToken == null)
                throw new ArgumentNullException("cancelToken");
            if (_disposed)
                throw new ObjectDisposedException("DbContextCollection");
            if (_completed)
                throw new InvalidOperationException(
                    "You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");

            // See comments in the sync version of this method for more details.

            ExceptionDispatchInfo lastError = null;

            var c = 0;

            foreach (var dbContext in _initializedDbContexts.Values.ToList())
            {
                try
                {
                    if (!_readOnly)
                    {
                        c += await dbContext.SaveChangesAsync(cancelToken).ConfigureAwait(false);
                    }

                    // If we've started an explicit database transaction, time to commit it now.
                    var tran = GetValueOrDefault(_transactions, dbContext);
                    if (tran != null)
                    {
                        tran.Commit();
                        tran.Dispose();
                    }
                }
                catch (Exception e)
                {
                    lastError = ExceptionDispatchInfo.Capture(e);
                }
            }

            _transactions.Clear();
            _completed = true;

            if (lastError != null)
                lastError.Throw(); // Re-throw while maintaining the exception's original stack track

            return c;
        }

        public void Rollback()
        {
            if (_disposed)
                throw new ObjectDisposedException("DbContextCollection");
            if (_completed)
                throw new InvalidOperationException(
                    "You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");

            ExceptionDispatchInfo lastError = null;

            foreach (var dbContext in _initializedDbContexts.Values.ToList())
            {
                // There's no need to explicitly rollback changes in a DbContext as
                // DbContext doesn't save any changes until its SaveChanges() method is called.
                // So "rolling back" for a DbContext simply means not calling its SaveChanges()
                // method. 

                // But if we've started an explicit database transaction, then we must roll it back.
                var tran = GetValueOrDefault(_transactions, dbContext);
                if (tran != null)
                {
                    try
                    {
                        tran.Rollback();
                        tran.Dispose();
                    }
                    catch (Exception e)
                    {
                        lastError = ExceptionDispatchInfo.Capture(e);
                    }
                }
            }

            _transactions.Clear();
            _completed = true;

            if (lastError != null)
                lastError.Throw(); // Re-throw while maintaining the exception's original stack track
        }

        /// <summary>
        ///     CFM - add this to find matching type + connection string
        /// </summary>
        /// <returns></returns>
        private bool FindMatchingContext(Type dbType)
        {
            // If not a customized context use default
            if (_dbContextFactory == null)
                return _initializedDbContexts.ContainsKey(new Tuple<Type, string>(dbType, null));

            // otherwise match on the connection string too          
            return _initializedDbContexts.Any(
                ctx => ctx.Key.Item2 == _dbContextFactory.ConnectionString);
        }

        /// <summary>
        ///     Returns the value associated with the specified key or the default
        ///     value for the TValue  type.
        /// </summary>
        private static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }
    }
}