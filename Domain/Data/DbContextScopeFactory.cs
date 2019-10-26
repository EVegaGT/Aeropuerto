/* 
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */

using System;
using System.Data;
using Domain.Data.Interfaces;

namespace Domain.Data
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DbContextScopeFactory : IDbContextScopeFactory
    {
        private readonly IDbContextFactory _dbContextFactory;

        public DbContextScopeFactory(IDbContextFactory dbContextFactory = null)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Interfaces.IDbContextScope Create(string connectionString = null,
                                      Interfaces.DbContextScopeOption joiningOption = Interfaces.DbContextScopeOption.JoinExisting)
        {
            if (!String.IsNullOrEmpty(connectionString))
                _dbContextFactory.ConnectionString = connectionString;

            return new DbContextScope((DbContextScopeOption) joiningOption, false, null, _dbContextFactory);
        }

        public Interfaces.IDbContextReadOnlyScope CreateReadOnly(string connectionString = null,
                                                      Interfaces.DbContextScopeOption joiningOption =
                                                          Interfaces.DbContextScopeOption.JoinExisting)
        {
            if (!String.IsNullOrEmpty(connectionString))
                _dbContextFactory.ConnectionString = connectionString;

            return new DbContextReadOnlyScope(joiningOption, null, _dbContextFactory);
        }

        public Interfaces.IDbContextReadOnlyScope CreateReadOnlyWithTransaction(IsolationLevel isolationLevel,
                                                                     string connectionString = null)
        {
            if (!String.IsNullOrEmpty(connectionString))
                _dbContextFactory.ConnectionString = connectionString;

            return new DbContextReadOnlyScope(Interfaces.DbContextScopeOption.ForceCreateNew, isolationLevel, _dbContextFactory);
        }

        public Interfaces.IDbContextScope CreateWithoutAutoDetectChanges(string connectionString = null,
                                                              Interfaces.DbContextScopeOption joiningOption =
                                                                  Interfaces.DbContextScopeOption.JoinExisting)
        {
            if (!String.IsNullOrEmpty(connectionString))
                _dbContextFactory.ConnectionString = connectionString;

            return new DbContextScope((DbContextScopeOption) joiningOption, false, null, false, _dbContextFactory);
        }

        public Interfaces.IDbContextScope CreateWithTransaction(IsolationLevel isolationLevel, string connectionString = null)
        {
            if (!String.IsNullOrEmpty(connectionString))
                _dbContextFactory.ConnectionString = connectionString;

            return new DbContextScope((DbContextScopeOption) Interfaces.DbContextScopeOption.ForceCreateNew, false, isolationLevel, _dbContextFactory);
        }

        public IDisposable SuppressAmbientContext()
        {
            return new AmbientContextSuppressor();
        }
    }
}