namespace Fiap.Api.Acidentes.Models
{
    public class AcidenteModel
    {

        public long Id { get; set; }
        public DateTime DataAcidente { get; set; }
        public TimeSpan HoraAcidente { get; set; }
        public string? Gravidade { get; set; }
        public string? Endereco { get; set; }

    }
}
