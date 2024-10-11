using Fiap.Api.Acidentes.Models;

namespace Fiap.Api.Acidentes.Services
{
    public class AuthService : IAuthService
    {
        private List<UserModel> _users = new List<UserModel>
        {
                    new UserModel { UserId = 1, Username = "operador01", Password = "pass123", Role = "operador" },
                    new UserModel { UserId = 2, Username = "analista01", Password = "pass123", Role = "analista" },
                    new UserModel { UserId = 3, Username = "gerente01", Password = "pass123", Role = "gerente" },
        };

        public UserModel Authenticate(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
