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
        }

        
    }
}
