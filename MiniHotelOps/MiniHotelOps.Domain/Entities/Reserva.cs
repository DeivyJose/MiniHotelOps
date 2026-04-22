using MiniHotelOps.Domain.Enums;

namespace MiniHotelOps.Domain.Entities;

public class Reserva
{
    public int Id { get; set; }

    public int HuespedId { get; set; }
    public Huesped Huesped { get; set; }

    public int HabitacionId { get; set; }
    public Habitacion Habitacion { get; set; }

    public DateTime FechaEntrada { get; set; }
    public DateTime FechaSalida { get; set; }
    public int CantidadPersonas { get; set; }
    public decimal TotalEstimado { get; set; }
    public EstadoReserva Estado { get; set; }
    public string ObservacionesGenerales { get; set; }

    public List<ReservaServicio> Servicios { get; set; } = new();
    public List<ObservacionReserva> Observaciones { get; set; } = new();

    public Reserva()
    {
    }

    public Reserva(int huespedId, int habitacionId, DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas)
    {
        HuespedId = huespedId;
        HabitacionId = habitacionId;
        FechaEntrada = fechaEntrada;
        FechaSalida = fechaSalida;
        CantidadPersonas = cantidadPersonas;
        Estado = EstadoReserva.Pendiente;
    }

    public void CambiarEstado(EstadoReserva nuevoEstado)
    {
        Estado = nuevoEstado;
    }
}