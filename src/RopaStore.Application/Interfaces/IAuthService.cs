using RopaStore.Application.DTOs.Auth;
 
namespace RopaStore.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegistrarAsync(RegisterRequest request);
        Task<AuthResponse?> LoginAsync(LoginRequest request);

    }
}
