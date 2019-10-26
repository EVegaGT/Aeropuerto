using Domain.Models;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface IClienteService
    {
        List<Cliente> GetAll();
        Cliente GetCliente(int id);
        void Create(Cliente cliente);
        void Edit(Cliente cliente);
        void Delete(Cliente cliente);
    }
}
