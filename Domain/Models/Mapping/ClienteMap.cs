using System.Data.Entity.ModelConfiguration;

namespace Domain.Models.Mapping
{
    public class ClienteMap : EntityTypeConfiguration<Cliente>
    {
        public ClienteMap()
        {
            // Primary Key
            HasKey(t => t.Id);
            Property(m => m.Id).HasColumnName("Id_cliente");

          
            // Table & Column Mappings
            ToTable("Cliente");
            Property(t => t.Nombre).HasColumnName("Nombre");
            Property(t => t.DPI).HasColumnName("DPI");
            Property(t => t.Apellido).HasColumnName("Apellido");
            Property(t => t.Direccion).HasColumnName("Direccion");
            Property(t => t.Telefono).HasColumnName("Telefono");
            Property(t => t.Tarjeta).HasColumnName("Tarjeta");
        }
    }
}
