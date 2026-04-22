using MiniHotelOps.Domain.Enums;

namespace MiniHotelOps.Domain.Entities;

public class Habitacion
{
    public int Id { get; set; }
    public string Numero { get; set; }
    public string Tipo { get; set; }
    public int Capacidad { get; set; }
    public decimal PrecioPorNoche { get; set; }
    public EstadoHabitacion Estado { get; set; }
    public string Descripcion { get; set; }

    public List<Reserva> Reservas { get; set; } = new();

    public Habitacion()
    {
    }

    public Habitacion(string numero, string tipo, int capacidad, decimal precioPorNoche, string descripcion)
    {
        Numero = numero;
        Tipo = tipo;
        Capacidad = capacidad;
        PrecioPorNoche = precioPorNoche;
        Descripcion = descripcion;
        Estado = EstadoHabitacion.Disponible;
    }

    public void CambiarEstado(EstadoHabitacion nuevoEstado)
    {
        Estado = nuevoEstado;
    }
}