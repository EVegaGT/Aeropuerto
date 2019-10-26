using Domain.Data;
using Domain.Data.Interfaces;
using Domain.Repositories;
using Domain.Services;
using StructureMap;

namespace Domain
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            //Services
            For<IClienteService>().Use<ClienteService>();

            //repositories
            For<IClienteRepository>().Use<ClienteRepository>();

            For<IDbContextScopeFactory>().Use<DbContextScopeFactory>();
            For<IDbContextFactory>().Use(ctx => (IDbContextFactory)null);
            For<IAmbientDbContextLocator>().Use<AmbientDbContextLocator>();
        }
    }
}
