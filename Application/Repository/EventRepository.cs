using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class EventRepository : GenericRepository<Event>, IEvent
{   
    private readonly APIContext _context;
    public EventRepository(APIContext context) : base(context)
    { 
        _context = context; 
    }
    public override async Task<(int totalRegistros,IEnumerable<Event> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
    {
        var query = _context.Events as IQueryable<Event>;
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

    public async Task<IEnumerable<Event>> GetEventImages()
    {
        return   await _context.Events.Include(x => x.Images).ToListAsync();
    }
}