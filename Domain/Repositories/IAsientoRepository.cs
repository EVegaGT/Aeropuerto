using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAsientoRepository
    {
        List<Asiento> GetAll();
        Asiento GetAsiento(int id);
        void Create(Asiento asiento);
        void Edit(Asiento asiento);
        void Delete(Asiento asiento);
    }
}
