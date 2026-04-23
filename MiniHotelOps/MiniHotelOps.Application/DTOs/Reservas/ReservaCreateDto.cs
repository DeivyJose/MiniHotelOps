namespace MiniHotelOps.Application.DTOs.Reservas;

public class ReservaCreateDto
{
    public int HuespedId { get; set; }
    public int HabitacionId { get; set; }
    public DateTime FechaEntrada { get; set; }
    public DateTime FechaSalida { get; set; }
    public int CantidadPersonas { get; set; }
}