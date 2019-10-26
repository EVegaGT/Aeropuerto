using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IEmbarcacionRepository
    {
       List<TarjetaEmbarque> GetAll();
       TarjetaEmbarque GetTarjetaEmbarque(int id);
       void Create(TarjetaEmbarque tarjetaEmbarque);
       void Edit(TarjetaEmbarque tarjetaEmbarque);
       void Delete(TarjetaEmbarque tarjetaEmbarque);
    }
}
