﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AeEntities : DbContext
    {
        public AeEntities()
            : base("name=AeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Aeropuerto> Aeropuertoes { get; set; }
        public virtual DbSet<Asiento> Asientoes { get; set; }
        public virtual DbSet<Avion> Avions { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Localidad> Localidads { get; set; }
        public virtual DbSet<Pai> Pais { get; set; }
        public virtual DbSet<Reserva> Reservas { get; set; }
        public virtual DbSet<TarjetaEmbarque> TarjetaEmbarques { get; set; }
        public virtual DbSet<Vuelo> Vueloes { get; set; }
    }
}
