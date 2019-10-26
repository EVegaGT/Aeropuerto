using Aeropuerto;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(StructuremapMvc), "Start")]
[assembly: ApplicationShutdownMethod(typeof(StructuremapMvc), "End")]

namespace Aeropuerto
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using StructureMap;

    public class StructuremapMvc
    {
       
    }
}
