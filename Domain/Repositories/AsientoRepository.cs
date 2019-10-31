using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Repositories
{
    public class AsientoRepository : CommonRepository, IAsientoRepository
    {
        public List<Asiento> GetAll()
        {
            var asientos = aeEntities.Asientoes
                     .SqlQuery("Select * from Asiento a")
                     .ToList();
            return asientos;
        }

        public Asiento GetAsiento(int id)
        {
            var asiento = aeEntities.Asientoes
                       .SqlQuery("Select * from Asiento where id_asiento = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return asiento;
        }

        public void Create(Asiento asiento)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "InsertarAsiento @IdAvion, @fila, @columna, @planta",
                new SqlParameter("@IdAvion", asiento.Id_Avion),
                new SqlParameter("@fila", asiento.Fila),
                new SqlParameter("@columna", asiento.Columna),
                new SqlParameter("@planta", asiento.Planta));
        }

        public void Edit(Asiento asiento)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "UpdateAsiento @IdAsiento, @IdAvion, @fila, @columna, @planta",
                new SqlParameter("@IdAsiento", asiento.Id_asiento),
                new SqlParameter("@IdAvion", asiento.Id_Avion),
                new SqlParameter("@fila", asiento.Fila),
                new SqlParameter("@columna", asiento.Columna),
                new SqlParameter("@planta", asiento.Planta));
        }

        public void Delete(Asiento asiento)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "DeleteAsiento @id",
                new SqlParameter("@id", asiento.Id_asiento));
        }
    }
}
