using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Repositories
{
    public class PaisRepository : CommonRepository, IPaisRepository
    {
        public List<Pai> GetAll()
        {
            var paises = aeEntities.Pais
                        .SqlQuery("Select * from pais")
                        .ToList();
            return paises;
        }

        public Pai GetPais(int id)
        {
            var pais = aeEntities.Pais
                        .SqlQuery("Select * from pais where id_pais = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return pais;
        }

        public void Create(Pai pai)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "InsertPais @nombre",
                new SqlParameter("@nombre", pai.Nombre));
        }

        public void Edit(Pai pai)

        {
            aeEntities.Database.ExecuteSqlCommand(
                "UpdatePais @id, @nombre",
                new SqlParameter("@id", pai.Id_pais),
                new SqlParameter("@nombre", pai.Nombre));
        }

        public void Delete(Pai pai)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "DeletePais @id",
                new SqlParameter("@id", pai.Id_pais));
        }
    }
}
