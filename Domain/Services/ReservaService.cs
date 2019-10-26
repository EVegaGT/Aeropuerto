using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public List<Reserva> GetAll()
        {
            return _reservaRepository.GetAll();
        }

        public Reserva GetReserva(int id)
        {
            return _reservaRepository.GetReserva(id);
        }

        public void Create(Reserva reserva)
        {
            _reservaRepository.Create(reserva);
        }

        public void Edit(Reserva reserva)
        {
            _reservaRepository.Edit(reserva);
        }

        public void Delete(Reserva reserva)
        {
            _reservaRepository.Delete(reserva);
        }
    }
}
