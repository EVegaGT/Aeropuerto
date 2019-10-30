using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Repositories
{
    public class ClienteRepository : CommonRepository, IClienteRepository
    {
        public List<Cliente> GetAll()
        {
            var clientes = aeEntities.Clientes
                      .SqlQuery("Select * from Clientes c")
                      .ToList();
            return clientes;
        }

        public Cliente GetCliente(int id)
        {
            var cliente = aeEntities.Clientes
                       .SqlQuery("Select * from Clientes where id_cliente = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return cliente;
        }

        public void Create(Cliente cliente)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "InsertarCliente @dpi, @Nombre, @Apellido, @Direccion, @Telefono, @Tarjeta",
                new SqlParameter("@dpi", cliente.DPI),
                new SqlParameter("@nombre", cliente.Nombre),
                new SqlParameter("@Apellido", cliente.Apellido),
                new SqlParameter("@Direccion", cliente.Direccion),
                new SqlParameter("@Telefono", cliente.Telefono),
                new SqlParameter("@Tarjeta", cliente.Tarjeta));
        }

        public void Edit(Cliente cliente)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "UpdateCliente @idcliente, @dpi, @Nombre, @Apellido, @Direccion, @Telefono, @Tarjeta",
                new SqlParameter("@idcliente", cliente.Id_cliente),
                new SqlParameter("@dpi", cliente.DPI),
                new SqlParameter("@nombre", cliente.Nombre),
                new SqlParameter("@Apellido", cliente.Apellido),
                new SqlParameter("@Direccion", cliente.Direccion),
                new SqlParameter("@Telefono", cliente.Telefono),
                new SqlParameter("@Tarjeta", cliente.Tarjeta));
        }

        public void Delete(Cliente cliente)
        {
            aeEntities.Database.ExecuteSqlCommand(
                "DeleteCliente @idcliente",
                new SqlParameter("@idcliente", cliente.Id_cliente));
        }

    }
}
