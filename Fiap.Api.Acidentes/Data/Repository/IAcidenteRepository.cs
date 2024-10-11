using Fiap.Api.Acidentes.Models;

namespace Fiap.Api.Acidentes.Data.Repository
{
    public interface IAcidenteRepository
    {
        IEnumerable<AcidenteModel> GetAll();
        IEnumerable<AcidenteModel> GetAllReference(long lastRefenrece, int size);
        AcidenteModel GetById(long id);
        void add(AcidenteModel acidente);
        void Update(AcidenteModel acidente);
        void Delete(AcidenteModel acidente);
    }
}
