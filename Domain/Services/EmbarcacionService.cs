using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class EmbarcacionService : IEmbarcacionService
    {
        private readonly IEmbarcacionRepository _embarcacionRepository;

        public EmbarcacionService(IEmbarcacionRepository embarcacionRepository)
        {
            _embarcacionRepository = embarcacionRepository;
        }

        public List<TarjetaEmbarque> GetAll()
        {
            return _embarcacionRepository.GetAll();
        }

        public TarjetaEmbarque GetTarjetaEmbarque(int id)
        {
            return _embarcacionRepository.GetTarjetaEmbarque(id);
        }

        public void Create(TarjetaEmbarque tarjetaEmbarque)
        {
            _embarcacionRepository.Create(tarjetaEmbarque);
        }

        public void Edit(TarjetaEmbarque tarjetaEmbarque)
        {
            _embarcacionRepository.Edit(tarjetaEmbarque);
        }

        public void Delete(TarjetaEmbarque tarjetaEmbarque)
        {
            _embarcacionRepository.Delete(tarjetaEmbarque);
        }
    }
}
