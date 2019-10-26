using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Domain.Repositories
{
    public class ClienteRepository : CommonRepository, IClienteRepository
    {
        public List<Cliente> GetAll()
        {
            return aeEntities.Clientes.ToList();
        }

        public Cliente GetCliente(int id)
        {
            return aeEntities.Clientes.Find(id);
        }

        public void Create(Cliente cliente)
        {
            aeEntities.Clientes.Add(cliente);
            aeEntities.SaveChanges();
        }

        public void Edit(Cliente cliente)
        {
            aeEntities.Clientes.AddOrUpdate(cliente);
            aeEntities.SaveChanges();
        }

        public void Delete(Cliente cliente)
        {
            aeEntities.Clientes.Remove(cliente);
            aeEntities.SaveChanges();
        }

    }
}
