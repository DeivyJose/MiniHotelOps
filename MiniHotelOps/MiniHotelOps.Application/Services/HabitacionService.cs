using MiniHotelOps.Application.Contracts;
using MiniHotelOps.Application.DTOs.Habitaciones;
using MiniHotelOps.Domain.Entities;
using MiniHotelOps.Domain.Enums;

namespace MiniHotelOps.Application.Services;

public class HabitacionService : IHabitacionService
{
    private readonly IHabitacionRepository _repository;

    public HabitacionService(IHabitacionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<HabitacionResponseDto>> GetAllAsync()
    {
        var habitaciones = await _repository.GetAllAsync();

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

    public async Task<HabitacionResponseDto?> GetByIdAsync(int id)
    {
        var h = await _repository.GetByIdAsync(id);

        if (h == null)
            return null;

        return new HabitacionResponseDto
        {
            Id = h.Id,
            Numero = h.Numero,
            Tipo = h.Tipo,
            Capacidad = h.Capacidad,
            PrecioPorNoche = h.PrecioPorNoche,
            Estado = h.Estado.ToString(),
            Descripcion = h.Descripcion
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

        var existe = await _repository.GetByNumeroAsync(dto.Numero);

        if (existe != null)
            throw new Exception("Ya existe una habitación con ese número.");

        var habitacion = new Habitacion(dto.Numero, dto.Tipo, dto.Capacidad, dto.PrecioPorNoche, dto.Descripcion);

        await _repository.AddAsync(habitacion);
        await _repository.SaveAsync();

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
        var habitacion = await _repository.GetByIdAsync(id);

        if (habitacion == null)
            return false;

        habitacion.Numero = dto.Numero;
        habitacion.Tipo = dto.Tipo;
        habitacion.Capacidad = dto.Capacidad;
        habitacion.PrecioPorNoche = dto.PrecioPorNoche;
        habitacion.Descripcion = dto.Descripcion;

        _repository.Update(habitacion);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var habitacion = await _repository.GetByIdAsync(id);

        if (habitacion == null)
            return false;

        _repository.Delete(habitacion);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<bool> CambiarEstadoAsync(int id, EstadoHabitacion nuevoEstado)
    {
        var habitacion = await _repository.GetByIdAsync(id);

        if (habitacion == null)
            return false;

        habitacion.CambiarEstado(nuevoEstado);

        _repository.Update(habitacion);
        await _repository.SaveAsync();

        return true;
    }
}