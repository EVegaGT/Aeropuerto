using Aeropuerto.DAL;
using Aeropuerto.Models;
using Microsoft.EntityFrameworkCore;

namespace Aeropuerto.DB
{
    public class Context : DbContext
    {
        public Context() : base()
        {

        }
        public static string GetConnectionString()
        {
            return StartupConfiguration.ConnectionString;
        }

        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder _builder)
        {
            _builder.UseSqlServer("Data Source=DESKTOP-8AVVOKB;Initial Catalog=Aeropuerto;Integrated Security=True");
        }
        
    }
}
