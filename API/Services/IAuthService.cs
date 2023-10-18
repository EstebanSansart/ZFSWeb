using API.Dtos.Custom;

namespace API.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> ReturnToken(AuthRequest auth);
        bool ValidarToken(string Token);
    }
}