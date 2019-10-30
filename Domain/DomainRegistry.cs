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
            For<IReservaService>().Use<ReservaService>();
            For<IEmbarcacionService>().Use<EmbarcacionService>();
            For<IPaisService>().Use<PaisService>();

            //repositories
            For<IClienteRepository>().Use<ClienteRepository>();
            For<IReservaRepository>().Use<ReservaRepository>();
            For<IEmbarcacionRepository>().Use<EmbarcacionRepository>();
            For<IPaisRepository>().Use<PaisRepository>();
            For<ILocalidadRepository>().Use<LocalidadRepository>();
            For<IAeropuertoRepository>().Use<AeropuertoRepository>();
            For<IAvionRepository>().Use<AvionRepository>();
            For<IAsientoRepository>().Use<AsientoRepository>();
            For<IVueloRepository>().Use<VueloRepository>();

            For<IDbContextScopeFactory>().Use<DbContextScopeFactory>();
            For<IDbContextFactory>().Use(ctx => (IDbContextFactory)null);
            For<IAmbientDbContextLocator>().Use<AmbientDbContextLocator>();
        }
    }
}
