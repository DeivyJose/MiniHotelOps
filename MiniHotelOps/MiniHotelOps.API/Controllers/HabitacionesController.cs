using Microsoft.AspNetCore.Mvc;
using MiniHotelOps.Application.Contracts;
using MiniHotelOps.Application.DTOs.Habitaciones;
using MiniHotelOps.Domain.Enums;

namespace MiniHotelOps.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitacionesController : ControllerBase
{
    private readonly IHabitacionService _service;

    public HabitacionesController(IHabitacionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound("Habitación no encontrada.");

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(HabitacionCreateDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, HabitacionUpdateDto dto)
    {
        try
        {
            var result = await _service.UpdateAsync(id, dto);

            if (!result)
                return NotFound("Habitación no encontrada.");

            return Ok("Habitación actualizada correctamente.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromQuery] EstadoHabitacion estado)
    {
        var result = await _service.CambiarEstadoAsync(id, estado);

        if (!result)
            return NotFound("Habitación no encontrada.");

        return Ok("Estado actualizado correctamente.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound("Habitación no encontrada.");

        return Ok("Habitación eliminada correctamente.");
    }
}