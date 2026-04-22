using Microsoft.EntityFrameworkCore;
using MiniHotelOps.Application.Contracts;
using MiniHotelOps.Domain.Entities;
using MiniHotelOps.Infrastructure.Data;

namespace MiniHotelOps.Infrastructure.Repositories;

public class HabitacionRepository : GenericRepository<Habitacion>, IHabitacionRepository
{
    private readonly MiniHotelContext _context;

    public HabitacionRepository(MiniHotelContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Habitacion> GetByNumeroAsync(string numero)
    {
        return await _context.Habitaciones
            .FirstOrDefaultAsync(h => h.Numero == numero);
    }
}