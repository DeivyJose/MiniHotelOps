using MiniHotelOps.Application.DTOs.Habitaciones;
using MiniHotelOps.Domain.Enums;

namespace MiniHotelOps.Application.Contracts;

public interface IHabitacionService
{
    Task<List<HabitacionResponseDto>> GetAllAsync();
    Task<HabitacionResponseDto?> GetByIdAsync(int id);
    Task<HabitacionResponseDto> CreateAsync(HabitacionCreateDto dto);
    Task<bool> UpdateAsync(int id, HabitacionUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> CambiarEstadoAsync(int id, EstadoHabitacion nuevoEstado);
}