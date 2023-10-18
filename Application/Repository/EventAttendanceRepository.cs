using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class EventAttendanceRepository : GenericRepository<EventAttendance>, IEventAttendance
{   
    private readonly APIContext _context;
    public EventAttendanceRepository(APIContext context) : base(context)
    { 
        _context = context; 
    }
    public override async Task<(int totalRegistros,IEnumerable<EventAttendance> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
    {
        var query = _context.EventAttendances as IQueryable<EventAttendance>;
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