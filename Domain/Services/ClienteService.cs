using System.Collections.Generic;
using Domain.Repositories;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public List<Cliente> GetAll()
        {
            return _clienteRepository.GetAll();
        }

        public Cliente GetCliente(int id)
        {
            return _clienteRepository.GetCliente(id);
        }

        public void Create(Cliente cliente)
        {
            _clienteRepository.Create(cliente);
        }

        public void Edit(Cliente cliente)
        {
            _clienteRepository.Edit(cliente);
        }

        public void Delete(Cliente cliente)
        {
            _clienteRepository.Delete(cliente);
        }
    }
}
