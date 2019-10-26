using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repositories
{
    public interface IClienteRepository
    {
        List<Cliente> GetAll();
        Cliente GetCliente(int id);
        void Create(Cliente cliente);
        void Edit(Cliente cliente);
        void Delete(Cliente cliente);
    }
}
