using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPaisRepository
    {
        List<Pai> GetAll();
        Pai GetPais(int id);
        void Create(Pai pai);
        void Edit(Pai pai);
        void Delete(Pai pai);
    }
}
