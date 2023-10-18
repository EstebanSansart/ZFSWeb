using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class CompanyRepository : GenericRepository<Company>, ICompany
{   
    private readonly APIContext _context;
    public CompanyRepository(APIContext context) : base(context)
    { 
        _context = context; 
    }
    public override async Task<(int totalRegistros,IEnumerable<Company> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
    {
        var query = _context.Companies as IQueryable<Company>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.Name.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
    }
}
