namespace Fiap.Api.Acidentes.ViewModel
{
    public class AcidentePaginacaoReferenciaViewModel
    {
        public IEnumerable<AcidenteViewModel> Acidentes { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public long NextRef { get; set; }
        public string PreviusPageUrl => $"/api/Acidente?referencia={Ref}&tamanho={PageSize} ";
        public string NextPageUrl => (Ref < NextRef) ? $"/api/Acidente?referencia={Ref}&tamanho={PageSize}" : "";

    }
}
