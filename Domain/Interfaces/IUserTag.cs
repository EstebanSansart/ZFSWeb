using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserTag : IGenericRepository<UserTag>
{
    Task<UserTag> GetByIdAsync(string UserCc, int TagId);
}