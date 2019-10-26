using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Domain.Repositories
{
    public class AvionRepository: CommonRepository, IAvionRepository
    {
        public List<Avion> GetAll()
        {
            return aeEntities.Avions.ToList();
        }

        public Avion GetAvion(int id)
        {
            return aeEntities.Avions.Find(id);
        }

        public void Create(Avion avion)
        {
            aeEntities.Avions.Add(avion);
            aeEntities.SaveChanges();
        }

        public void Edit(Avion avion)
        {
            aeEntities.Avions.AddOrUpdate(avion);
            aeEntities.SaveChanges();
        }

        public void Delete(Avion avion)
        {
            aeEntities.Avions.Remove(avion);
            aeEntities.SaveChanges();
        }
    }
}
