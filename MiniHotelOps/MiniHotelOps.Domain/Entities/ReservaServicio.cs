namespace MiniHotelOps.Domain.Entities;

public class ReservaServicio
{
    public int Id { get; set; }

    public int ReservaId { get; set; }
    public Reserva Reserva { get; set; }

    public int ServicioId { get; set; }
    public Servicio Servicio { get; set; }

    public int Cantidad { get; set; }
    public decimal Subtotal { get; set; }
}