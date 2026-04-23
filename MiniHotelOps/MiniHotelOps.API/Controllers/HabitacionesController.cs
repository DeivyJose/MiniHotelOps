using Microsoft.AspNetCore.Mvc;
using MiniHotelOps.Application.Contracts;
using MiniHotelOps.Application.DTOs.Habitaciones;
using MiniHotelOps.Domain.Enums;

namespace MiniHotelOps.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitacionesController : ControllerBase
{
    private readonly IHabitacionService _habitacionService;

    public HabitacionesController(IHabitacionService habitacionService)
    {
        _habitacionService = habitacionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _habitacionService.GetAllAsync();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _habitacionService.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(HabitacionCreateDto dto)
    {
        try
        {
            var result = await _habitacionService.CreateAsync(dto);
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
            var result = await _habitacionService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok("Habitación actualizada correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _habitacionService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok("Habitación eliminada correctamente");
    }

    [HttpPatch("{id}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromQuery] EstadoHabitacion estado)
    {
        var result = await _habitacionService.CambiarEstadoAsync(id, estado);

        if (!result)
            return NotFound();

        return Ok("Estado actualizado correctamente");
    }
}