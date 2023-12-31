using Domain.Entities;

namespace Domain.Interfaces;

public interface IEvent : IGenericRepository<Event>
{
    Task<IEnumerable<Event>> GetEventImages();
}
