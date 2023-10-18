using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class UserRepository : GenericRepository<User>, IUser
{
    public UserRepository(APIContext context) : base(context)
    { 
    }
     public override async Task<(int totalRegistros,IEnumerable<User> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.Users as IQueryable<User>;
        if(!string.IsNullOrEmpty(search))
        {
            //query  = query.Where(p => p.Email.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
     }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Name.ToLower() == username.ToLower());
    }

    public async Task<bool> ValidarUsuario(string UserCc, string Password)
    {
        return _context.Users.Any(m => m.UserCc == UserCc && m.Password == Password);
    }

    public async Task<string> GenerarPasswordAleatoria()
    {
        // Definir los caracteres permitidos en la contraseña
        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        // Longitud de la contraseña
        const int passwordLength = 6;
        // Crear un array de caracteres aleatorios
        char[] chars = new char[passwordLength];
        // Crear una instancia de Random para generar números aleatorios
        Random random = new Random();
        // Llenar el array con caracteres aleatorios
        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
        }
        // Devolver la contraseña generada
        return new string(chars);
    }
    
}
