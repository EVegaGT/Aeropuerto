using System;
using System.Data.Entity;
namespace Domain.Data
{
    public interface IDbContextCollection
    {
        /// <summary>
        /// Get or create a DbContext instance of the specified type. 
        /// </summary>
        TDbContext Get<TDbContext>() where TDbContext : DbContext;
    }
}
