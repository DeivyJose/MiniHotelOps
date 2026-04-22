using MiniHotelOps.Domain.Common;

namespace MiniHotelOps.Domain.Entities;

public class Huesped : PersonaBase
{
    public string Direccion { get; set; }
    public DateTime FechaNacimiento { get; set; }

    public List<Reserva> Reservas { get; set; } = new();

    public Huesped()
    {
    }

    public Huesped(string nombre, string apellido, string documento, string telefono, string email, string direccion, DateTime fechaNacimiento)
        : base(nombre, apellido, documento, telefono, email)
    {
        Direccion = direccion;
        FechaNacimiento = fechaNacimiento;
    }

    public override string ObtenerNombreCompleto()
    {
        return $"{Nombre} {Apellido}";
    }
}