using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class UserTagRepository : GenericRepository<UserTag>, IUserTag
{   
    private readonly APIContext _context;
    public UserTagRepository(APIContext context) : base(context)
    { 
        _context = context; 
    }
    public override async Task<(int totalRegistros,IEnumerable<UserTag> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
    {
        var query = _context.UserTags as IQueryable<UserTag>;
        if(!string.IsNullOrEmpty(search))
        {
            
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
    }
}