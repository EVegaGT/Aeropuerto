using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Repositories
{
    public class ReservaRepository : CommonRepository, IReservaRepository
    {
        public List<Reserva> GetAll()
        {
            var reservas = aeEntities.Reservas
                       .SqlQuery("Select * from Reserva r")
                       .ToList();
            return reservas;
        }

        public Reserva GetReserva(int id)
        {
            var reserva = aeEntities.Reservas
                        .SqlQuery("Select * from Reserva where id_reserva = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return reserva;
        }

        public void Create(Reserva reserva)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "InsertarReserva @IdVuelo, @Asientos, @IdCliente",
                new SqlParameter("@IdVuelo", reserva.Id_vuelo),
                new SqlParameter("@Asientos", reserva.Asientos),
                new SqlParameter("@IdCliente", reserva.Id_cliente));
        }

        public void Edit(Reserva reserva)
        {
            aeEntities.Database.ExecuteSqlCommand(
               "UpdateReserva @id, @IdVuelo, @Asientos, @IdCliente",
               new SqlParameter("@id", reserva.Id_reserva),
               new SqlParameter("@IdVuelo", reserva.Id_vuelo),
               new SqlParameter("@Asientos", reserva.Asientos),
               new SqlParameter("@IdCliente", reserva.Id_cliente));
        }

        public void Delete(Reserva reserva)
        {
            aeEntities.Database.ExecuteSqlCommand(
               "DeleteReserva @id",
               new SqlParameter("@id", reserva.Id_reserva));
        }
    }
}
