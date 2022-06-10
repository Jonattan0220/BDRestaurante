using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restaurante.Models
{
    public partial class BDRestauranteContext : DbContext
    {
        public BDRestauranteContext()
        {
        }

        public BDRestauranteContext(DbContextOptions<BDRestauranteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<DetalleXfactura> DetalleXfacturas { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<Mesa> Mesas { get; set; } = null!;
        public virtual DbSet<Mesero> Meseros { get; set; } = null!;
        public virtual DbSet<Supervisor> Supervisors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DetalleXfactura>(entity =>
            {
                entity.HasKey(e => e.IdDetalleXfactura);

                entity.ToTable("DetalleXFactura");

                entity.Property(e => e.IdDetalleXfactura).HasColumnName("IdDetalleXFactura");

                entity.Property(e => e.Plato)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Factura)
                    .WithMany(p => p.DetalleXfacturas)
                    .HasForeignKey(d => d.FacturaId)
                    .HasConstraintName("FK_DetalleXFactura_Factura");

                entity.HasOne(d => d.Supervisor)
                    .WithMany(p => p.DetalleXfacturas)
                    .HasForeignKey(d => d.SupervisorId)
                    .HasConstraintName("FK_DetalleXFactura_Supervisor");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura");

                entity.Property(e => e.FechaHora).HasColumnType("datetime");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK_Factura_Cliente");

                entity.HasOne(d => d.Mesa)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.MesaId)
                    .HasConstraintName("FK_Factura_Mesa");

                entity.HasOne(d => d.Mesero)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.MeseroId)
                    .HasConstraintName("FK_Factura_Mesero1");
            });

            modelBuilder.Entity<Mesa>(entity =>
            {
                entity.ToTable("Mesa");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mesero>(entity =>
            {
                entity.ToTable("Mesero");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Supervisor>(entity =>
            {
                entity.HasKey(e => e.SupervirsorId);

                entity.ToTable("Supervisor");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
