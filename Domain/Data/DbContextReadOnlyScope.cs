/* 
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */

using System.Data;
using Domain.Data.Interfaces;

namespace Domain.Data
{
    public class DbContextReadOnlyScope : IDbContextReadOnlyScope
    {
        private readonly DbContextScope _internalScope;

        public Interfaces.IDbContextCollection DbContexts { get { return _internalScope.DbContexts; } }

        public DbContextReadOnlyScope(IDbContextFactory dbContextFactory = null)
            : this(joiningOption: Interfaces.DbContextScopeOption.JoinExisting, isolationLevel: null, dbContextFactory: dbContextFactory)
        {}

        public DbContextReadOnlyScope(IsolationLevel isolationLevel, IDbContextFactory dbContextFactory = null)
            : this(joiningOption: Interfaces.DbContextScopeOption.ForceCreateNew, isolationLevel: isolationLevel, dbContextFactory: dbContextFactory)
        { }

        public DbContextReadOnlyScope(Interfaces.DbContextScopeOption joiningOption, IsolationLevel? isolationLevel, IDbContextFactory dbContextFactory = null)
        {
            _internalScope = new DbContextScope(joiningOption: (DbContextScopeOption) joiningOption, readOnly: true, isolationLevel: isolationLevel, dbContextFactory: dbContextFactory);
        }

        public void Dispose()
        {
            _internalScope.Dispose();
        }
    }
}