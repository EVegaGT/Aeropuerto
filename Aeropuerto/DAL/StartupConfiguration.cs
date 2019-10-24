using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Aeropuerto.DAL
{
    public class StartupConfiguration
    {
        public static string ConnectionString { get; private set; }
        public IConfigurationRoot Configuration { get; set; }

        public StartupConfiguration(IHostingEnvironment _environment)
        {
            Configuration = new ConfigurationBuilder()
                            .SetBasePath(_environment.ContentRootPath)
                            .AddJsonFile("appsettings.json")
                            .Build();
            ConnectionString = Configuration["Connectionstrings:MyConnection"];
        }
    }
}
