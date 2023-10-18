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
    
}
