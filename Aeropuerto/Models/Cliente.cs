using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aeropuerto.Models
{
    public class Cliente
    {
        [Key]
        public int Id_cliente;
        public string DPI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public int Tarjeta { get; set; }
    }   
}
