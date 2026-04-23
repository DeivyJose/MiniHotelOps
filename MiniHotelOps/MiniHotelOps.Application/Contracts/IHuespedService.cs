using MiniHotelOps.Application.DTOs.Huespedes;

namespace MiniHotelOps.Application.Contracts;

public interface IHuespedService
{
    Task<List<HuespedResponseDto>> GetAllAsync();
    Task<HuespedResponseDto> CreateAsync(HuespedCreateDto dto);
}