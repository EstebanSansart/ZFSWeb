using Domain.Entities;

namespace Domain.Interfaces;

public interface IUser : IGenericRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
    Task<bool> ValidarUsuario(string UserCc, string Password);
    Task<string> GenerarPasswordAleatoria();
}