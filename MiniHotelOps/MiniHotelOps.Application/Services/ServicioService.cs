using MiniHotelOps.Application.Contracts;
using MiniHotelOps.Application.DTOs.Servicios;
using MiniHotelOps.Domain.Entities;

namespace MiniHotelOps.Application.Services;

public class ServicioService
{
    private readonly IGenericRepository<Servicio> _repository;

    public ServicioService(IGenericRepository<Servicio> repository)
    {
        _repository = repository;
    }

    public async Task<List<Servicio>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Servicio> CreateAsync(ServicioCreateDto dto)
    {
        if (dto.Precio <= 0)
            throw new Exception("Precio inválido");

        var entity = new Servicio(dto.Nombre, dto.Descripcion, dto.Precio);

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();

        return entity;
    }
}