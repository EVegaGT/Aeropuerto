using Domain.Models;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface IClienteService
    {
        List<Cliente> GetAll();
    }
}
