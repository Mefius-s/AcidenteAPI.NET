namespace Fiap.Api.Acidentes.ViewModel
{
    public class AcidenteViewModel
    {
        public long Id { get; set; }
        public DateTime DataAcidente { get; set; }
        public TimeSpan HoraAcidente { get; set; }
        public string? Gravidade { get; set; }
        public string? Endereco { get; set; }
    }
}
