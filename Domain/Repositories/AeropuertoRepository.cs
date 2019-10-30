using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Repositories
{
    public class AeropuertoRepository: CommonRepository, IAeropuertoRepository
    {
        public List<Aeropuerto> GetAll()
        {
            var aeropuertos = aeEntities.Aeropuertoes
                       .SqlQuery("Select * from Aeropuerto a")
                       .ToList();
            return aeropuertos;
        }

        public Aeropuerto GetAeropuerto(int id)
        {
            var aeropuerto = aeEntities.Aeropuertoes
                        .SqlQuery("Select * from Aeropuerto where id_aeropuerto = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return aeropuerto;
        }

        public void Create(Aeropuerto aeropuerto)
        {
            
            aeEntities.Database.ExecuteSqlCommand(
               "InsertarAeropuerto @nombre, @IdLocalidad",
               new SqlParameter("@nombre", aeropuerto.Nombre),
               new SqlParameter("@IdLocalidad", aeropuerto.Id_localidad));
        }

        public void Edit(Aeropuerto aeropuerto)
        {
            aeEntities.Database.ExecuteSqlCommand(
               "UpdateAeropuerto @id, @nombre, @IdLocalidad",
               new SqlParameter("@id", aeropuerto.Id_aeropuerto),
               new SqlParameter("@nombre", aeropuerto.Nombre),
               new SqlParameter("@IdLocalidad", aeropuerto.Id_localidad));
        }

        public void Delete(Aeropuerto aeropuerto)
        {
            aeEntities.Database.ExecuteSqlCommand(
               "DeleteAeropuerto @id",
               new SqlParameter("@id", aeropuerto.Id_aeropuerto));
        }
    }
}
