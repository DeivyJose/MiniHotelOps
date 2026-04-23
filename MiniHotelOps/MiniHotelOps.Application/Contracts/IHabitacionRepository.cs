using MiniHotelOps.Domain.Entities;

namespace MiniHotelOps.Application.Contracts;

public interface IHabitacionRepository : IGenericRepository<Habitacion>
{
    Task<Habitacion?> GetByNumeroAsync(string numero);
}