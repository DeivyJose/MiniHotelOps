namespace MiniHotelOps.Domain.Entities;

public class ObservacionReserva
{
    public int Id { get; set; }

    public int ReservaId { get; set; }
    public Reserva Reserva { get; set; }

    public string Nota { get; set; }
    public DateTime Fecha { get; set; }

    public ObservacionReserva()
    {
    }

    public ObservacionReserva(string nota)
    {
        Nota = nota;
        Fecha = DateTime.Now;
    }
}