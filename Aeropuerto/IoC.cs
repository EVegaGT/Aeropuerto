using StructureMap;

namespace Aeropuerto
{
    public class IoC
    {
        public static IContainer Initialize()
        {
            return new Container(x =>
            {
                x.AddRegistry(new DefaultRegistry());
                x.AddRegistry(new Domain.DomainRegistry());
            });
        }
    }
}
