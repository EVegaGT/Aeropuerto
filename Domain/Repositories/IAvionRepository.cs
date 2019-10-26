using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAvionRepository
    {
        List<Avion> GetAll();
        Avion GetAvion(int id);
        void Create(Avion avion);
        void Edit(Avion avion);
        void Delete(Avion avion);
    }
}
