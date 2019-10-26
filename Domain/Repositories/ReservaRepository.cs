using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Domain.Repositories
{
    public class ReservaRepository : CommonRepository, IReservaRepository
    {
        public List<Reserva> GetAll()
        {
            return aeEntities.Reservas.ToList();
        }

        public Reserva GetReserva(int id)
        {
            return aeEntities.Reservas.Find(id);
        }

        public void Create(Reserva reserva)
        {
            aeEntities.Reservas.Add(reserva);
            aeEntities.SaveChanges();
        }

        public void Edit(Reserva reserva)
        {
            aeEntities.Reservas.AddOrUpdate(reserva);
            aeEntities.SaveChanges();
        }

        public void Delete(Reserva reserva)
        {
            aeEntities.Reservas.Remove(reserva);
            aeEntities.SaveChanges();
        }
    }
}
