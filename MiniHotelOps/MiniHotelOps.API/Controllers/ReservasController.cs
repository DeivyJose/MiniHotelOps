using Microsoft.AspNetCore.Mvc;
using MiniHotelOps.Application.Contracts;
using MiniHotelOps.Application.DTOs.Reservas;
using MiniHotelOps.Application.Services;
using MiniHotelOps.Domain.Entities;

namespace MiniHotelOps.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly ReservaService _service;
    private readonly IGenericRepository<Reserva> _repository;

    public ReservasController(ReservaService service, IGenericRepository<Reserva> repository)
    {
        _service = service;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ReservaCreateDto dto)
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