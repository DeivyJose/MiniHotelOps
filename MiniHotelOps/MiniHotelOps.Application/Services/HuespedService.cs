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
            throw new Exception("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.Apellido))
            throw new Exception("El apellido es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.Documento))
            throw new Exception("El documento es obligatorio.");

        var fecha = dto.FechaNacimiento ?? DateTime.Now;

        var entity = new Huesped(
            dto.Nombre,
            dto.Apellido,
            dto.Documento,
            dto.Telefono,
            dto.Email,
            dto.Direccion,
            fecha
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

    public async Task<bool> DeleteAsync(int id)
    {
        var huesped = await _repository.GetByIdAsync(id);

        if (huesped == null)
            return false;

        _repository.Delete(huesped);
        await _repository.SaveAsync();

        return true;
    }
}