using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class UserReactionRepository : GenericRepository<UserReaction>, IUserReaction
{   
    private readonly APIContext _context;
    public UserReactionRepository(APIContext context) : base(context)
    { 
        _context = context; 
    }
    public override async Task<(int totalRegistros,IEnumerable<UserReaction> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
    {
        var query = _context.UserReactions as IQueryable<UserReaction>;
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
    public virtual async Task<UserReaction> GetByIdAsync(string UserCc, int ReactionId)
    {
        return await _context.Set<UserReaction>().Where(x => x.ReactionId == ReactionId && x.UserCc == UserCc).FirstOrDefaultAsync();
    }
}