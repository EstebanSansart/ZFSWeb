
using Application.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{

    CompanyRepository _company;
    EventAttendanceRepository _eventAttendance;
    EventRepository _event;
    GenderRepository _gender;
    LevelRepository _level;
    ReactionRepository _reaction;
    TagRepository _tag;
    UserReactionRepository _userReaction;
    UserRepository _user;
    UserTagRepository _userTag;
    ImageRepository _image;

    private readonly APIContext _context;
    public UnitOfWork(APIContext context)
    {
        _context = context;
    }
    public ICompany Companies
    {
        get
        {
            _company ??= new CompanyRepository(_context);
            return _company;
        }
    }
    public IEventAttendance EventAttendances
    {
        get
        {
            _eventAttendance ??= new EventAttendanceRepository(_context);
            return _eventAttendance;
        }
    }
    public IEvent Events
    {
        get
        {
            _event ??= new EventRepository(_context);
            return _event;
        }
    }
    public IGender Genders
    {
        get
        {
            _gender ??= new GenderRepository(_context);
            return _gender;
        }
    }
    public ILevel Levels
    {
        get
        {
            _level ??= new LevelRepository(_context);
            return _level;
        }
    }
    public IReaction Reactions
    {
        get
        {
            _reaction ??= new ReactionRepository(_context);
            return _reaction;
        }
    }
    public ITag Tags
    {
        get
        {
            _tag ??= new TagRepository(_context);
            return _tag;
        }
    }
    public IImage Images
    {
        get
        {
            _image ??= new ImageRepository(_context);
            return _image;
        }
    }
    public IUserReaction UserReactions
    {
        get
        {
            _userReaction ??= new UserReactionRepository(_context);
            return _userReaction;
        }
    }
    public IUser Users
    {
        get
        {
            _user ??= new UserRepository(_context);
            return _user;
        }
    }
    public IUserTag UserTags
    {
        get
        {
            _userTag ??= new UserTagRepository(_context);
            return _userTag;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
