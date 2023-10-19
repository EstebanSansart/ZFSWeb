using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserReaction : IGenericRepository<UserReaction>
{
    Task<UserReaction> GetByIdAsync(string UserCc, int ReactionId);
}