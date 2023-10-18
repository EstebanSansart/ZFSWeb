namespace Domain.Interfaces;


public interface IUnitOfWork
{
    public ICompany Companys {get;}
    public IEvent Events {get;}
    public IEventAttendance EventAtteIEventAttendances {get;}
    public IGender Genders {get;}
    public ILevel Levels {get;}
    public IReaction Reactions {get;}
    public ITag Tags {get;}
    public IUserReaction UserReactions {get;}
    public IUserTag UserTags {get;}
    public IUser Users {get;}
    Task<int> SaveAsync();
}