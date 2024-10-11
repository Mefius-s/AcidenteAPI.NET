namespace Fiap.Api.Acidentes.Exception
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, ApplicationException innerException)
            : base(message, innerException) { }
    }
}
