using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface IReservaRepository
    {
        List<Reserva> GetAll();
        Reserva GetReserva(int id);
        void Create(Reserva reserva);
        void Edit(Reserva reserva);
        void Delete(Reserva reserva);
    }
}
