namespace Domain.Interfaces;


public interface IUnitOfWork
{
    public ICompany Companies {get;}
    public IEvent Events {get;}
    public IEventAttendance EventAttendances {get;}
    public IGender Genders {get;}
    public ILevel Levels {get;}
    public IReaction Reactions {get;}
    public ITag Tags {get;}
    public IUserReaction UserReactions {get;}
    public IUserTag UserTags {get;}
    public IUser Users {get;}
    public IImage Images {get;}
    Task<int> SaveAsync();
}