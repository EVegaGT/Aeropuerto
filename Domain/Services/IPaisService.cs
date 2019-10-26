using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPaisService
    {
        List<Pai> GetAll();
        Pai GetPai(int id);
        void Create(Pai pai);
        void Edit(Pai pai);
        void Delete(Pai pai);
    }
}
