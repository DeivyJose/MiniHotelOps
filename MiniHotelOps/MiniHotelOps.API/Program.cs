using Microsoft.EntityFrameworkCore;
using MiniHotelOps.Application.Contracts;
using MiniHotelOps.Application.Services;
using MiniHotelOps.Infrastructure.Data;
using MiniHotelOps.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MiniHotelContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IHabitacionRepository, HabitacionRepository>();

builder.Services.AddScoped<IHabitacionService, HabitacionService>();
builder.Services.AddScoped<IHuespedService, HuespedService>();
builder.Services.AddScoped<ServicioService>();
builder.Services.AddScoped<ReservaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();