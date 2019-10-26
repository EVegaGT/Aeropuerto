using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Domain.Repositories
{
    public class AeropuertoRepository: CommonRepository, IAeropuertoRepository
    {
        public List<Aeropuerto> GetAll()
        {
            return aeEntities.Aeropuertoes.ToList();
        }

        public Aeropuerto GetAeropuerto(int id)
        {
            return aeEntities.Aeropuertoes.Find(id);
        }

        public void Create(Aeropuerto aeropuerto)
        {
            aeEntities.Aeropuertoes.Add(aeropuerto);
            aeEntities.SaveChanges();
        }

        public void Edit(Aeropuerto aeropuerto)
        {
            aeEntities.Aeropuertoes.AddOrUpdate(aeropuerto);
            aeEntities.SaveChanges();
        }

        public void Delete(Aeropuerto aeropuerto)
        {
            aeEntities.Aeropuertoes.Remove(aeropuerto);
            aeEntities.SaveChanges();
        }
    }
}
