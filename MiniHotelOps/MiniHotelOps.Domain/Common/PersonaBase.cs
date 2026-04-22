namespace MiniHotelOps.Domain.Common;

public abstract class PersonaBase
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Documento { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }

    public PersonaBase()
    {
    }

    public PersonaBase(string nombre, string apellido, string documento, string telefono, string email)
    {
        Nombre = nombre;
        Apellido = apellido;
        Documento = documento;
        Telefono = telefono;
        Email = email;
    }

    public abstract string ObtenerNombreCompleto();
}
