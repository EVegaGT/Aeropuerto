using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Domain.Repositories
{
    public class LocalidadRepository: CommonRepository, ILocalidadRepository
    {
        public List<Localidad> GetAll()
        {

            var locations = aeEntities.Localidads
                       .SqlQuery("Select * from localidad l")
                       .ToList();
            return locations;
        }

        public Localidad GetLocalidad(int id)
        {
            var localidad = aeEntities.Localidads
                        .SqlQuery("Select * from localidad where id_localidad = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return localidad;
        }

        public void Create(Localidad localidad)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "InsertarLocalidad @nombre, @id_pais",
                new SqlParameter("@nombre", localidad.Nombre),
                new SqlParameter("@id_pais", localidad.Id_pais));
        }

        public void Edit(Localidad localidad)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "UpdateLocalidad @id, @nombre, @id_pais",
                new SqlParameter("@id", localidad.Id_localidad),
                new SqlParameter("@nombre", localidad.Nombre),
                new SqlParameter("@id_pais", localidad.Id_pais));
        }

        public void Delete(Localidad localidad)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "DeleteLocalidad @id",
                new SqlParameter("@id", localidad.Id_localidad));
        }
    }
}
