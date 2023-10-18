using ApiJWTManual.Dtos.Custom;

namespace ApiJWTManual.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> ReturnToken(AuthRequest auth);
    }
}