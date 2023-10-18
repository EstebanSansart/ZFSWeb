using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class TagRepository : GenericRepository<Tag>, ITag
{   
    private readonly APIContext _context;
    public TagRepository(APIContext context) : base(context)
    { 
        _context = context; 
    }
    public override async Task<(int totalRegistros,IEnumerable<Tag> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
    {
        var query = _context.Tags as IQueryable<Tag>;
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