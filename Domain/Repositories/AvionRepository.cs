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
            avion.Numero_plazas = avion.Max_Columna * avion.Max_Fila;
            aeEntities.Avions.Add(avion);
            aeEntities.SaveChanges();
            for (var i = 0; i < avion.Max_Columna; i++)
            {
                for (var j = 0; j < avion.Max_Fila; j++)
                {
                    var planta = string.Format("P{0}-{1}", avion.Id_avion, i);
                    aeEntities.Database.ExecuteSqlCommand(
                        "InsertarAsiento @IdAvion, @fila, @columna, @planta",
                        new SqlParameter("@IdAvion", avion.Id_avion),
                        new SqlParameter("@fila", j),
                        new SqlParameter("@columna", i),
                        new SqlParameter("@planta", planta));
                }
            }

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
