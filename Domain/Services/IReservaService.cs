using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IReservaService
    {
        List<Reserva> GetAll();
        Reserva GetReserva(int id);
        void Create(Reserva cliente);
        void Edit(Reserva cliente);
        void Delete(Reserva cliente);
    }
}
