using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface ILocalidadRepository
    {
        List<Localidad> GetAll();
        Localidad GetLocalidad(int id);
        void Create(Localidad localidad);
        void Edit(Localidad localidad);
        void Delete(Localidad localidad);
    }
}
