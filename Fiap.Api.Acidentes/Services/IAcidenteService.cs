using Fiap.Api.Acidentes.Models;

namespace Fiap.Api.Acidentes.Services
{
    public interface IAcidenteService
    {
        IEnumerable<AcidenteModel> ListarAcidentes();
        IEnumerable<AcidenteModel> ListarAcidentesReferencia(long ultimoId = 0, int tamanho = 10);
        AcidenteModel ObterAcidentePorId(long id);
        void Atualizar(AcidenteModel acidente);
        void Criar(AcidenteModel acidente);
        void Excluir(long id);
    }
}
