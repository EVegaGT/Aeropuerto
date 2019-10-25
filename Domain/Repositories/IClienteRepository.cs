using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repositories
{
    public interface IClienteRepository
    {
        List<Cliente> GetAll();
    }
}
