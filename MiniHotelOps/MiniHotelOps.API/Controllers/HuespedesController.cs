using Microsoft.AspNetCore.Mvc;
using MiniHotelOps.Application.Contracts;
using MiniHotelOps.Application.DTOs.Huespedes;

namespace MiniHotelOps.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HuespedesController : ControllerBase
{
    private readonly IHuespedService _service;

    public HuespedesController(IHuespedService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(HuespedCreateDto dto)
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
}