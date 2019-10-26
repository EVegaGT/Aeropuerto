using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace Domain.Repositories
{
    public class LocalidadRepository: CommonRepository, ILocalidadRepository
    {
        public List<Localidad> GetAll()
        {
            return aeEntities.Localidads.ToList();
        }

        public Localidad GetLocalidad(int id)
        {
            return aeEntities.Localidads.Find(id);
        }

        public void Create(Localidad localidad)
        {
            aeEntities.Localidads.Add(localidad);
            aeEntities.SaveChanges();
        }

        public void Edit(Localidad localidad)
        {
            aeEntities.Localidads.AddOrUpdate(localidad);
            aeEntities.SaveChanges();
        }

        public void Delete(Localidad localidad)
        {
            aeEntities.Localidads.Remove(localidad);
            aeEntities.SaveChanges();
        }
    }
}
