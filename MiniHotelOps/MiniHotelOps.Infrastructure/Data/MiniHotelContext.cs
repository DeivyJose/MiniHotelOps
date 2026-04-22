using Microsoft.EntityFrameworkCore;
using MiniHotelOps.Domain.Entities;

namespace MiniHotelOps.Infrastructure.Data;

public class MiniHotelContext : DbContext
{
    public MiniHotelContext(DbContextOptions<MiniHotelContext> options) : base(options)
    {
    }

    public DbSet<Habitacion> Habitaciones { get; set; }
    public DbSet<Huesped> Huespedes { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<ReservaServicio> ReservaServicios { get; set; }
    public DbSet<ObservacionReserva> ObservacionesReserva { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Numero)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Descripcion)
                .HasMaxLength(250);

            entity.Property(e => e.PrecioPorNoche)
                .HasColumnType("decimal(10,2)");

            entity.HasIndex(e => e.Numero)
                .IsUnique();
        });

        modelBuilder.Entity<Huesped>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Documento)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.Telefono)
                .HasMaxLength(20);

            entity.Property(e => e.Email)
                .HasMaxLength(120);

            entity.Property(e => e.Direccion)
                .HasMaxLength(200);

            entity.HasIndex(e => e.Documento)
                .IsUnique();
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.TotalEstimado)
                .HasColumnType("decimal(10,2)");

            entity.Property(e => e.ObservacionesGenerales)
                .HasMaxLength(300);

            entity.HasOne(e => e.Huesped)
                .WithMany(h => h.Reservas)
                .HasForeignKey(e => e.HuespedId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Habitacion)
                .WithMany(h => h.Reservas)
                .HasForeignKey(e => e.HabitacionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200);

            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<ReservaServicio>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10,2)");

            entity.HasOne(e => e.Reserva)
                .WithMany(r => r.Servicios)
                .HasForeignKey(e => e.ReservaId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Servicio)
                .WithMany(s => s.Reservas)
                .HasForeignKey(e => e.ServicioId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ObservacionReserva>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nota)
                .IsRequired()
                .HasMaxLength(300);

            entity.HasOne(e => e.Reserva)
                .WithMany(r => r.Observaciones)
                .HasForeignKey(e => e.ReservaId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}