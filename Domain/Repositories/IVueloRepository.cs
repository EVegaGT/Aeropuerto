using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface IVueloRepository
    {
        List<Vuelo> GetAll();
        Vuelo GetVuelo(int id);
        void Create(Vuelo vuelo);
        void Edit(Vuelo vuelo);
        void Delete(Vuelo vuelo);
    }
}
