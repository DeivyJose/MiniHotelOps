using MiniHotelOps.Application.Contracts;
using MiniHotelOps.Application.DTOs.Reservas;
using MiniHotelOps.Domain.Entities;
using MiniHotelOps.Domain.Enums;

namespace MiniHotelOps.Application.Services;

public class ReservaService
{
    private readonly IGenericRepository<Reserva> _reservaRepository;
    private readonly IGenericRepository<Habitacion> _habitacionRepository;
    private readonly IGenericRepository<Huesped> _huespedRepository;

    public ReservaService(
        IGenericRepository<Reserva> reservaRepository,
        IGenericRepository<Habitacion> habitacionRepository,
        IGenericRepository<Huesped> huespedRepository)
    {
        _reservaRepository = reservaRepository;
        _habitacionRepository = habitacionRepository;
        _huespedRepository = huespedRepository;
    }

    public async Task<Reserva> CrearReservaAsync(ReservaCreateDto dto)
    {
        if (dto.FechaSalida <= dto.FechaEntrada)
            throw new Exception("La fecha de salida debe ser mayor que la de entrada.");

        var habitacion = await _habitacionRepository.GetByIdAsync(dto.HabitacionId);

        if (habitacion == null)
            throw new Exception("La habitación no existe.");

        var huesped = await _huespedRepository.GetByIdAsync(dto.HuespedId);

        if (huesped == null)
            throw new Exception("El huésped no existe.");

        if (dto.CantidadPersonas > habitacion.Capacidad)
            throw new Exception("La cantidad de personas excede la capacidad.");

        var reservas = await _reservaRepository.GetAllAsync();

        var conflicto = reservas.Any(r =>
            r.HabitacionId == dto.HabitacionId &&
            r.Estado != EstadoReserva.Cancelada &&
            dto.FechaEntrada < r.FechaSalida &&
            dto.FechaSalida > r.FechaEntrada
        );

        if (conflicto)
            throw new Exception("La habitación ya está reservada en esas fechas.");

        var dias = (dto.FechaSalida - dto.FechaEntrada).Days;

        if (dias <= 0)
            throw new Exception("Cantidad de días inválida.");

        var total = dias * habitacion.PrecioPorNoche;

        var reserva = new Reserva(
            dto.HuespedId,
            dto.HabitacionId,
            dto.FechaEntrada,
            dto.FechaSalida,
            dto.CantidadPersonas
        );

        reserva.TotalEstimado = total;
        reserva.Estado = EstadoReserva.Confirmada;

        await _reservaRepository.AddAsync(reserva);
        await _reservaRepository.SaveAsync();

        return reserva;
    }
}