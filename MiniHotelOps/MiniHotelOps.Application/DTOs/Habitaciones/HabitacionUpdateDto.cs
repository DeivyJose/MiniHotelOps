namespace MiniHotelOps.Application.DTOs.Habitaciones;

public class HabitacionUpdateDto
{
    public string Numero { get; set; }
    public string Tipo { get; set; }
    public int Capacidad { get; set; }
    public decimal PrecioPorNoche { get; set; }
    public string Descripcion { get; set; }
}