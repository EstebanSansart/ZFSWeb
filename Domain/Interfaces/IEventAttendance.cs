using Domain.Entities;

namespace Domain.Interfaces;

public interface IEventAttendance : IGenericRepository<EventAttendance>
{
    Task<EventAttendance> GetByIdAttendance(string UserId, int EventoId);
}
