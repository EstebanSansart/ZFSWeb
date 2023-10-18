using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class LevelRepository : GenericRepository<Level>, ILevel
{   
    private readonly APIContext _context;
    public LevelRepository(APIContext context) : base(context)
    { 
        _context = context; 
    }
    public override async Task<(int totalRegistros,IEnumerable<Level> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
    {
        var query = _context.Levels as IQueryable<Level>;
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