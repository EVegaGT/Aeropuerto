using System.Data.Entity;

namespace Domain.Data
{
    public interface IAmbientDbContextLocator
    {
        /// <summary>
        /// If called within the scope of a DbContextScope, gets or creates 
        /// the ambient DbContext instance for the provided DbContext type. 
        /// 
        /// Otherwise returns null. 
        /// </summary>
        TDbContext Get<TDbContext>() where TDbContext : DbContext;

        // For Mock only
        TDbContext Get<TDbContext>(string connectionString) where TDbContext : DbContext;
    }
}
