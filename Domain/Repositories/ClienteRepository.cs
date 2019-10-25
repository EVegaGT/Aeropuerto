using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Models;
using Domain.Models.Mapping;

namespace Domain.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private InternalDbContext DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<InternalDbContext>();


                if (dbContext == null)
                    throw new InvalidOperationException(
                        "No ambient DbContext of type InternalDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

                return dbContext;
            }
        }

        public ClienteRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if (ambientDbContextLocator == null) throw new ArgumentNullException("ambientDbContextLocator");
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        public List<Cliente> GetAll()
        {
            return DbContext.Clientes.OrderBy(o => o.Nombre).ToList();
        }

    }
}
