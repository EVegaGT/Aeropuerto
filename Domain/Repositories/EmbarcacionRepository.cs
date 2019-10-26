using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Domain.Repositories
{
    public class EmbarcacionRepository : CommonRepository, IEmbarcacionRepository
    {
        public List<TarjetaEmbarque> GetAll()
        {
            return aeEntities.TarjetaEmbarques.ToList();
        }

        public TarjetaEmbarque GetTarjetaEmbarque(int id)
        {
            return aeEntities.TarjetaEmbarques.Find(id);
        }

        public void Create(TarjetaEmbarque tarjetaEmbarque)
        {
            aeEntities.TarjetaEmbarques.Add(tarjetaEmbarque);
            aeEntities.SaveChanges();
        }

        public void Edit(TarjetaEmbarque tarjetaEmbarque)
        {
            aeEntities.TarjetaEmbarques.AddOrUpdate(tarjetaEmbarque);
            aeEntities.SaveChanges();
        }

        public void Delete(TarjetaEmbarque tarjetaEmbarque)
        {
            aeEntities.TarjetaEmbarques.Remove(tarjetaEmbarque);
            aeEntities.SaveChanges();
        }
    }
}
