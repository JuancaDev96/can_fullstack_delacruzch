using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Scraping.Contexto;

public partial class ScraperdbContext(DbContextOptions<ScraperdbContext> options) : DbContext(options)
{
    public virtual DbSet<Archivo> Archivos { get; set; }

    public virtual DbSet<DetalleDireccion> DetalleDireccions { get; set; }

    public virtual DbSet<DireccionUrl> DireccionUrls { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Archivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("archivo_pkey");

            entity.ToTable("archivo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Procesado)
                .HasDefaultValue(false)
                .HasColumnName("procesado");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .HasColumnName("usuario_registro");
        });

        modelBuilder.Entity<DetalleDireccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("detalle_direccion_pkey");

            entity.ToTable("detalle_direccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contenido).HasColumnName("contenido");
            entity.Property(e => e.Etiqueta)
                .HasMaxLength(50)
                .HasColumnName("etiqueta");
            entity.Property(e => e.UrlId).HasColumnName("url_id");

            entity.HasOne(d => d.Url).WithMany(p => p.DetalleDireccions)
                .HasForeignKey(d => d.UrlId)
                .HasConstraintName("detalle_direccion_url_id_fkey");
        });

        modelBuilder.Entity<DireccionUrl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("direccion_url_pkey");

            entity.ToTable("direccion_url");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArchivoId).HasColumnName("archivo_id");
            entity.Property(e => e.Procesado)
                .HasDefaultValue(false)
                .HasColumnName("procesado");
            entity.Property(e => e.Url).HasColumnName("url");

            entity.HasOne(d => d.Archivo).WithMany(p => p.DireccionUrls)
                .HasForeignKey(d => d.ArchivoId)
                .HasConstraintName("direccion_url_archivo_id_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clave)
                .HasMaxLength(255)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(255)
                .HasColumnName("usuario");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .HasColumnName("usuario_registro");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
