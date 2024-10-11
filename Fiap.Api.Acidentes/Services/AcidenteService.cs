using Fiap.Api.Acidentes.Data.Contexts;
using Fiap.Api.Acidentes.Data.Repository;
using Fiap.Api.Acidentes.Exception;
using Fiap.Api.Acidentes.Models;

namespace Fiap.Api.Acidentes.Services
{
    public class AcidenteService : IAcidenteService
    {
        private readonly IAcidenteRepository _repository;

        public AcidenteService(IAcidenteRepository repository)
        {
            _repository = repository;
        }

        public void Atualizar(AcidenteModel acidente) => _repository.Update(acidente);

        public void Criar(AcidenteModel acidente) => _repository.add(acidente);
        

        public void Excluir(long id)
        {
            var acidente = _repository.GetById(id);
            if (acidente != null)
            {
                _repository.Delete(acidente);
            }
        }
        

        public IEnumerable<AcidenteModel> ListarAcidentes() => _repository.GetAll();
        

        public AcidenteModel ObterAcidentePorId(long id)
        {
            var acidente = _repository.GetById(id);
            if (acidente == null)
            {
                throw new NotFoundException($"Acidente com ID {id} não encontrado.");
            }
            return acidente;
        }

        public IEnumerable<AcidenteModel> ListarAcidentesReferencia(long ultimoId = 0, int tamanho = 10)
        {
            return _repository.GetAllReference(ultimoId, tamanho);
        }
        
    }
}
