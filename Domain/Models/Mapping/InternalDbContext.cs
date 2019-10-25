using System.Data.Entity;

namespace Domain.Models.Mapping
{
    public class InternalDbContext : DbContext
    {
        // For DDD we only need to map our Aggregate Roots here
        // The child entities will be included in the mappings via the aggregate roots. 
        // Note: Special cases are those many-to-many relational entities that contain data as well 
        // EF has no good way of handling those

        // Map our Domain models by convention (make virtual so we can override with stubs for unit testing)

        public InternalDbContext()
        {
            Database.SetInitializer<InternalDbContext>(null);

            // Add our own logging
            // Comment out to reduce overhead
            // DbInterception.Add(new CrmCommandInterceptor(GetType().Name));
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
    }
}
