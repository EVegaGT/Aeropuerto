using Domain.common;

namespace Domain.Models
{
    public class Cliente : Entity<int>, IAggregateRoot
    {
        public string Id_cliente { get; set; }
        public string DPI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public int Tarjeta { get; set; }
    }   
}
