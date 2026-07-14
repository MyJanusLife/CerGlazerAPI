using CerGlazerAPI.Entities;
using CerGlazerAPI.Models;

namespace CerGlazerAPI.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterUserAsync(UserDTO registrationRequest);

        Task<string?> LoginUserAsync(UserDTO loginRequest);
    }
}
