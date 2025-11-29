using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TechNova.Models
{
    public partial class TechNovaDbContext : IdentityDbContext
    {
        public TechNovaDbContext()
        {
        }

        public TechNovaDbContext(DbContextOptions<TechNovaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<VentaDetalle> VentaDetalle { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=TechNovaDB;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primero Identity
            base.OnModelCreating(modelBuilder);

            // --- CLIENTE ---
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ClienteId);

                entity.HasIndex(e => e.Correo, "IDX_Clientes_Correo");
                entity.HasIndex(e => e.NombreCompleto, "IDX_Clientes_Nombre");

                entity.Property(e => e.Correo).HasMaxLength(100);
                entity.Property(e => e.Direccion).HasMaxLength(200);
                entity.Property(e => e.NombreCompleto).HasMaxLength(150);
                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            // --- PRODUCTO ---
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.ProductoId);

                entity.HasIndex(e => e.Codigo, "IDX_Productos_Codigo");
                entity.HasIndex(e => e.Nombre, "IDX_Productos_Nombre");

                entity.HasIndex(e => e.Codigo, "UQ_Productos_Codigo").IsUnique();

                entity.Property(e => e.Codigo).HasMaxLength(50);
                entity.Property(e => e.Descripcion).HasMaxLength(300);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            });

            // --- VENTA ---
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.VentaId);

                entity.HasIndex(e => e.ClienteId, "IDX_Ventas_Cliente");
                entity.HasIndex(e => e.Fecha, "IDX_Ventas_Fecha");

                entity.Property(e => e.Fecha)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // --- VENTA DETALLE ---
            modelBuilder.Entity<VentaDetalle>(entity =>
            {
                entity.HasKey(e => e.VentaDetalleId);

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.VentaDetalles)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Venta)
                    .WithMany(p => p.VentaDetalles)
                    .HasForeignKey(d => d.VentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
