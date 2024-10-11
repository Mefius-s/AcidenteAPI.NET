using Fiap.Api.Acidentes.Models;

namespace Fiap.Api.Acidentes.Services
{
    public interface IAuthService
    {
        UserModel Authenticate(String username, String password);
    }
}
