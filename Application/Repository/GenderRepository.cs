using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class GenderRepository : GenericRepository<Gender>, IGender
{   
    private readonly APIContext _context;
    public GenderRepository(APIContext context) : base(context)
    { 
        _context = context; 
    }
    public override async Task<(int totalRegistros,IEnumerable<Gender> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
    {
        var query = _context.Genders as IQueryable<Gender>;
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