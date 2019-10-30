using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Repositories
{
    public class AvionRepository: CommonRepository, IAvionRepository
    {
        public List<Avion> GetAll()
        {

            var aviones = aeEntities.Avions
                       .SqlQuery("Select * from avion a")
                       .ToList();
            return aviones;
        }

        public Avion GetAvion(int id)
        {
            var avion = aeEntities.Avions
                       .SqlQuery("Select * from avion where id_avion = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return avion;
        }

        public void Create(Avion avion)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "InsertAvion @nombre, @NumeroPlazas, @MaxColumna, @MaxFila",
                new SqlParameter("@nombre", avion.Nombre),
                new SqlParameter("@NumeroPlazas", avion.Numero_plazas),
                new SqlParameter("@MaxColumna", avion.Max_Columna),
                new SqlParameter("@MaxFila", avion.Max_Fila));
        }

        public void Edit(Avion avion)
        {
            aeEntities.Database.ExecuteSqlCommand(
               "UpdateAvion @id, @nombre, @NumeroPlazas, @MaxColumna, @MaxFila",
               new SqlParameter("@id", avion.Id_avion),
               new SqlParameter("@nombre", avion.Nombre),
               new SqlParameter("@NumeroPlazas", avion.Numero_plazas),
               new SqlParameter("@MaxColumna", avion.Max_Columna),
               new SqlParameter("@MaxFila", avion.Max_Fila));
        }

        public void Delete(Avion avion)
        {
            aeEntities.Database.ExecuteSqlCommand(
               "DeleteAvion @id",
               new SqlParameter("@id", avion.Id_avion));
        }
    }
}
