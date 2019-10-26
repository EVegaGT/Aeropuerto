using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PaisService : IPaisService
    {
        private readonly IPaisRepository _paisRepository;

        public PaisService(IPaisRepository paisRepository)
        {
            _paisRepository = paisRepository;
        }

        public List<Pai> GetAll()
        {
            return _paisRepository.GetAll();
        }

        public Pai GetPai(int id)
        {
            return _paisRepository.GetPais(id);
        }

        public void Create(Pai pai)
        {
            _paisRepository.Create(pai);
        }

        public void Edit(Pai pai)
        {
            _paisRepository.Edit(pai);
        }

        public void Delete(Pai pai)
        {
            _paisRepository.Delete(pai);
        }
    }
}
