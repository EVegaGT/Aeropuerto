using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Domain.Repositories
{
    public class PaisRepository : CommonRepository, IPaisRepository
    {
        public List<Pai> GetAll()
        {
            return aeEntities.Pais.ToList();
        }

        public Pai GetPais(int id)
        {
            return aeEntities.Pais.Find(id);
        }

        public void Create(Pai pai)
        {
            aeEntities.Pais.Add(pai);
            aeEntities.SaveChanges();
        }

        public void Edit(Pai pai)
        {
            aeEntities.Pais.AddOrUpdate(pai);
            aeEntities.SaveChanges();
        }

        public void Delete(Pai pai)
        {
            aeEntities.Pais.Remove(pai);
            aeEntities.SaveChanges();
        }
    }
}
