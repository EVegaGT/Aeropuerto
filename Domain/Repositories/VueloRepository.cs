using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class VueloRepository : CommonRepository, IVueloRepository
    {
        public List<Vuelo> GetAll()
        {
            var vuelos = aeEntities.Vueloes
                       .SqlQuery("Select * from Vuelo v")
                       .ToList();
            return vuelos;
        }

        public Vuelo GetVuelo(int id)
        {

            var vuelos = aeEntities.Vueloes
                        .SqlQuery("Select * from Vuelo where id_vuelo = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return vuelos;
        }

        public void Create(Vuelo vuelo)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "InsertarVuelo @IdAvion, @AeropuertoSalida, @AeropuertoEntrada, @FechaSalida, @FechaEntrada",
                new SqlParameter("@IdAvion", vuelo.Id_avion),
                new SqlParameter("@AeropuertoSalida", vuelo.Aeropuerto_salida),
                new SqlParameter("@AeropuertoEntrada", vuelo.Aeropuerto_entrada),
                new SqlParameter("@FechaSalida", vuelo.Fecha_salida),
                new SqlParameter("@FechaEntrada", vuelo.Fecha_entrada));
        }

        public void Edit(Vuelo vuelo)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "UpdateVuelo @id, @IdAvion, @AeropuertoSalida, @AeropuertoEntrada, @FechaSalida, @FechaEntrada",
                new SqlParameter("@id", vuelo.Id_vuelo),
                new SqlParameter("@IdAvion", vuelo.Id_avion),
                new SqlParameter("@AeropuertoSalida", vuelo.Aeropuerto_salida),
                new SqlParameter("@AeropuertoEntrada", vuelo.Aeropuerto_entrada),
                new SqlParameter("@FechaSalida", vuelo.Fecha_salida),
                new SqlParameter("@FechaEntrada", vuelo.Fecha_entrada));
        }

        public void Delete(Vuelo vuelo)
        {
            aeEntities.Database.ExecuteSqlCommand(
               "DeleteVuelo @id",
               new SqlParameter("@id", vuelo.Id_vuelo));
        }
    }
}
