using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Models;
using Domain.Repositories;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        private IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _dbContextScopeFactory = ObjectFactory.Container.GetInstance<IDbContextScopeFactory>();
            _clienteRepository = clienteRepository;
        }

        public List<Cliente> GetAll()
        {
             using (_dbContextScopeFactory.CreateReadOnly())
                {
                    return _clienteRepository.GetAll();
                }

        }
    }
}
