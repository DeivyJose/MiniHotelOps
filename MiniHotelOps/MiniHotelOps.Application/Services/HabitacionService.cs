using MiniHotelOps.Application.Contracts;
using MiniHotelOps.Application.DTOs.Habitaciones;
using MiniHotelOps.Domain.Entities;
using MiniHotelOps.Domain.Enums;

namespace MiniHotelOps.Application.Services;

public class HabitacionService : IHabitacionService
{
    private readonly IHabitacionRepository _habitacionRepository;

    public HabitacionService(IHabitacionRepository habitacionRepository)
    {
        _habitacionRepository = habitacionRepository;
    }

    public async Task<List<HabitacionResponseDto>> GetAllAsync()
    {
        var habitaciones = await _habitacionRepository.GetAllAsync();

        return habitaciones.Select(h => new HabitacionResponseDto
        {
            Id = h.Id,
            Numero = h.Numero,
            Tipo = h.Tipo,
            Capacidad = h.Capacidad,
            PrecioPorNoche = h.PrecioPorNoche,
            Estado = h.Estado.ToString(),
            Descripcion = h.Descripcion
        }).ToList();
    }

    public async Task<HabitacionResponseDto> GetByIdAsync(int id)
    {
        var habitacion = await _habitacionRepository.GetByIdAsync(id);

        if (habitacion == null)
            return null;

        return new HabitacionResponseDto
        {
            Id = habitacion.Id,
            Numero = habitacion.Numero,
            Tipo = habitacion.Tipo,
            Capacidad = habitacion.Capacidad,
            PrecioPorNoche = habitacion.PrecioPorNoche,
            Estado = habitacion.Estado.ToString(),
            Descripcion = habitacion.Descripcion
        };
    }

    public async Task<HabitacionResponseDto> CreateAsync(HabitacionCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Numero))
            throw new Exception("El número de habitación es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.Tipo))
            throw new Exception("El tipo de habitación es obligatorio.");

        if (dto.Capacidad <= 0)
            throw new Exception("La capacidad debe ser mayor que cero.");

        if (dto.PrecioPorNoche <= 0)
            throw new Exception("El precio por noche debe ser mayor que cero.");

        var existente = await _habitacionRepository.GetByNumeroAsync(dto.Numero);

        if (existente != null)
            throw new Exception("Ya existe una habitación con ese número.");

        var habitacion = new Habitacion(dto.Numero, dto.Tipo, dto.Capacidad, dto.PrecioPorNoche, dto.Descripcion);

        await _habitacionRepository.AddAsync(habitacion);
        await _habitacionRepository.SaveAsync();

        return new HabitacionResponseDto
        {
            Id = habitacion.Id,
            Numero = habitacion.Numero,
            Tipo = habitacion.Tipo,
            Capacidad = habitacion.Capacidad,
            PrecioPorNoche = habitacion.PrecioPorNoche,
            Estado = habitacion.Estado.ToString(),
            Descripcion = habitacion.Descripcion
        };
    }

    public async Task<bool> UpdateAsync(int id, HabitacionUpdateDto dto)
    {
        var habitacion = await _habitacionRepository.GetByIdAsync(id);

        if (habitacion == null)
            return false;

        if (string.IsNullOrWhiteSpace(dto.Numero))
            throw new Exception("El número de habitación es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.Tipo))
            throw new Exception("El tipo de habitación es obligatorio.");

        if (dto.Capacidad <= 0)
            throw new Exception("La capacidad debe ser mayor que cero.");

        if (dto.PrecioPorNoche <= 0)
            throw new Exception("El precio por noche debe ser mayor que cero.");

        var existente = await _habitacionRepository.GetByNumeroAsync(dto.Numero);

        if (existente != null && existente.Id != id)
            throw new Exception("Ya existe otra habitación con ese número.");

        habitacion.Numero = dto.Numero;
        habitacion.Tipo = dto.Tipo;
        habitacion.Capacidad = dto.Capacidad;
        habitacion.PrecioPorNoche = dto.PrecioPorNoche;
        habitacion.Descripcion = dto.Descripcion;

        _habitacionRepository.Update(habitacion);
        await _habitacionRepository.SaveAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var habitacion = await _habitacionRepository.GetByIdAsync(id);

        if (habitacion == null)
            return false;

        _habitacionRepository.Delete(habitacion);
        await _habitacionRepository.SaveAsync();

        return true;
    }

    public async Task<bool> CambiarEstadoAsync(int id, EstadoHabitacion nuevoEstado)
    {
        var habitacion = await _habitacionRepository.GetByIdAsync(id);

        if (habitacion == null)
            return false;

        habitacion.CambiarEstado(nuevoEstado);

        _habitacionRepository.Update(habitacion);
        await _habitacionRepository.SaveAsync();

        return true;
    }
}