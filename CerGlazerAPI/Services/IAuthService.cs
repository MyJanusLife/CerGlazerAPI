using CerGlazerAPI.Entities;
using CerGlazerAPI.Models;

namespace CerGlazerAPI.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterUserAsync(UserDTO registrationRequest);

        Task<TokenResponseDto?> LoginUserAsync(UserDTO loginRequest);

        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto refreshTokenRequest);
    }
}
