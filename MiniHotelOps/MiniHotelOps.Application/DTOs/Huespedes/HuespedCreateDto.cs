namespace MiniHotelOps.Application.DTOs.Huespedes;

public class HuespedCreateDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;

    public DateTime? FechaNacimiento { get; set; }
}