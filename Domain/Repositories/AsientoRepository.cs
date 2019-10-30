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
            aeEntities.Asientoes.Add(asiento);
            aeEntities.SaveChanges();
        }

        public void Edit(Asiento asiento)
        {
            aeEntities.Asientoes.AddOrUpdate(asiento);
            aeEntities.SaveChanges();
        }

        public void Delete(Asiento asiento)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "DeleteAsiento @id",
                new SqlParameter("@id", asiento.Id_asiento));
        }
    }
}
