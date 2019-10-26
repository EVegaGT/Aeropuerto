using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAeropuertoRepository
    {
        List<Aeropuerto> GetAll();
        Aeropuerto GetAeropuerto(int id);
        void Create(Aeropuerto aeropuerto);
        void Edit(Aeropuerto aeropuerto);
        void Delete(Aeropuerto aeropuerto);
    }
}
