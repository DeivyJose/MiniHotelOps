using Microsoft.AspNetCore.Mvc;
using MiniHotelOps.Application.DTOs.Reservas;
using MiniHotelOps.Application.Services;

namespace MiniHotelOps.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly ReservaService _service;

    public ReservasController(ReservaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CrearReserva(ReservaCreateDto dto)
    {
        try
        {
            var result = await _service.CrearReservaAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
