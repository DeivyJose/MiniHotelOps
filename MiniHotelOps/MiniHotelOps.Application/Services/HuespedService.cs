using MiniHotelOps.Application.Contracts;
using MiniHotelOps.Application.DTOs.Huespedes;
using MiniHotelOps.Domain.Entities;

namespace MiniHotelOps.Application.Services;

public class HuespedService : IHuespedService
{
    private readonly IGenericRepository<Huesped> _repository;

    public HuespedService(IGenericRepository<Huesped> repository)
    {
        _repository = repository;
    }

    public async Task<List<HuespedResponseDto>> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();

        return data.Select(h => new HuespedResponseDto
        {
            Id = h.Id,
            NombreCompleto = h.ObtenerNombreCompleto(),
            Documento = h.Documento,
            Telefono = h.Telefono,
            Email = h.Email
        }).ToList();
    }

    public async Task<HuespedResponseDto> CreateAsync(HuespedCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre))
            throw new Exception("Nombre requerido");

        if (string.IsNullOrWhiteSpace(dto.Documento))
            throw new Exception("Documento requerido");

        var entity = new Huesped(
            dto.Nombre,
            dto.Apellido,
            dto.Documento,
            dto.Telefono,
            dto.Email,
            dto.Direccion,
            dto.FechaNacimiento
        );

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();

        return new HuespedResponseDto
        {
            Id = entity.Id,
            NombreCompleto = entity.ObtenerNombreCompleto(),
            Documento = entity.Documento,
            Telefono = entity.Telefono,
            Email = entity.Email
        };
    }
}