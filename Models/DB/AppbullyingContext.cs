using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ApiPryBullyng.Models.DB;

namespace ApiPryBullyng.Models.DB;

public partial class AppbullyingContext : DbContext
{
    public AppbullyingContext()
    {
    }

    public AppbullyingContext(DbContextOptions<AppbullyingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Formulario> Formularios { get; set; }

    public virtual DbSet<Institucion> Institucions { get; set; }

    public virtual DbSet<Mensaje> Mensajes { get; set; }

    public virtual DbSet<Preguntum> Pregunta { get; set; }

    public virtual DbSet<Resultado> Resultados { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=MyDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK_ID_Curso");

            entity.ToTable("Curso");

            entity.Property(e => e.IdCurso).HasColumnName("id_Curso");
            entity.Property(e => e.IdInstitucionF).HasColumnName("id_InstitucionF");
            entity.Property(e => e.Nivel).HasColumnName("nivel");
            entity.Property(e => e.Paralelo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("paralelo");

            entity.HasOne(d => d.IdInstitucionFNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.IdInstitucionF)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_Institucion1");
        });

        modelBuilder.Entity<Formulario>(entity =>
        {
            entity.HasKey(e => e.IdFormulario).HasName("PK_ID_Formulario");

            entity.ToTable("Formulario");

            entity.Property(e => e.IdFormulario).HasColumnName("id_Formulario");
            entity.Property(e => e.Detalle)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("detalle");
            entity.Property(e => e.Fecha)
                .IsRequired()
                .HasColumnName("fecha");
            entity.Property(e => e.IdUsuarioF).HasColumnName("id_UsuarioF");
            entity.Property(e => e.TituloCaso)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tituloCaso");

            entity.HasOne(d => d.IdUsuarioFNavigation).WithMany(p => p.Formularios)
                .HasForeignKey(d => d.IdUsuarioF)
                .HasConstraintName("FK_ID_Usuario3");
        });

        modelBuilder.Entity<Institucion>(entity =>
        {
            entity.HasKey(e => e.IdInstitucion).HasName("PK_ID_Institucion");

            entity.ToTable("Institucion");

            entity.Property(e => e.IdInstitucion).HasColumnName("id_Institucion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Info)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("info");
            entity.Property(e => e.Logo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("logo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.ToTable("Mensaje");

            entity.HasKey(e => e.IdMensaje).HasName("PK_Mensaje");

            entity.Property(e => e.IdMensaje).HasColumnName("id_Mensaje");
            entity.Property(e => e.Contenido).IsRequired().IsUnicode().HasColumnName("Contenido");
            entity.Property(e => e.FechaEnvio).IsRequired().HasColumnName("FechaEnvio");
            entity.Property(e => e.Leido).IsRequired().HasColumnName("Leido");
            entity.Property(e => e.GrupoId).IsRequired().IsUnicode(false).HasColumnName("GrupoId");
            entity.Property(e => e.IdUsuarioRemitente).HasColumnName("id_UsuarioRemitente");
            entity.Property(e => e.IdUsuarioDestinatario).HasColumnName("id_UsuarioDestinatario");

            entity.HasOne(d => d.IdUsuarioRemitenteNavigation)
                .WithMany(p => p.MensajesRemitidos)
                .HasForeignKey(d => d.IdUsuarioRemitente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mensaje_UsuarioRemitente");

            entity.HasOne(d => d.IdUsuarioDestinatarioNavigation)
                .WithMany(p => p.MensajesRecibidos)
                .HasForeignKey(d => d.IdUsuarioDestinatario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mensaje_UsuarioDestinatario");
        });


        modelBuilder.Entity<MensajesNoLeidos>(entity =>
        {
            entity.ToTable("MensajesNoLeidos");

            entity.HasKey(e => e.IdMensajeNoLeido).HasName("PK_MensajesNoLeidos");

            entity.Property(e => e.IdMensajeNoLeido).HasColumnName("IdMensajeNoLeido");
            entity.Property(e => e.IdUsuarioDestinatario).HasColumnName("IdUsuarioDestinatario");
            entity.Property(e => e.IdUsuarioRemitente).HasColumnName("IdUsuarioRemitente");
            entity.Property(e => e.CantidadMensajes).HasColumnName("CantidadMensajes");

            entity.HasOne(d => d.IdUsuarioDestinatarioNavigation)
                .WithMany(p => p.MensajesNoLeidosDestinatario)
                .HasForeignKey(d => d.IdUsuarioDestinatario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MensajesNoLeidos_UsuarioDestinatario");

            entity.HasOne(d => d.IdUsuarioRemitenteNavigation)
                .WithMany(p => p.MensajesNoLeidosRemitente)
                .HasForeignKey(d => d.IdUsuarioRemitente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MensajesNoLeidos_UsuarioRemitente");
        });



        modelBuilder.Entity<Preguntum>(entity =>
        {
            entity.HasKey(e => e.IdPregunta).HasName("PK_ID_Pregunta");

            entity.Property(e => e.IdPregunta).HasColumnName("id_Pregunta");
            entity.Property(e => e.IdTestF).HasColumnName("id_TestF");
            entity.Property(e => e.TextoPregunta)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("textoPregunta");

            entity.HasOne(d => d.IdTestFNavigation).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.IdTestF)
                .HasConstraintName("FK_ID_Test");
        });

        modelBuilder.Entity<Resultado>(entity =>
        {
            entity.HasKey(e => e.IdResultado);

            entity.Property(e => e.IdResultado).HasColumnName("id_Resultado");
            entity.Property(e => e.IdTest).HasColumnName("id_Test");
            entity.Property(e => e.IdUsuario).HasColumnName("id_Usuario");
            entity.Property(e => e.PuntajeResultados).HasColumnName("puntajeResultados");

            entity.HasOne(d => d.IdTestNavigation).WithMany(p => p.Resultados)
                .HasForeignKey(d => d.IdTest)
                .HasConstraintName("FK_Resultados_Test");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Resultados)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Resultados_Usuario");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.IdTest).HasName("PK_ID_Test");

            entity.ToTable("Test");

            entity.Property(e => e.IdTest).HasColumnName("id_Test");
            entity.Property(e => e.NombreTest)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreTest");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_ID_Usuario");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.NombreUsuario, "UK_NombreUsuario").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_Usuario");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contrasenia");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Genero).HasColumnName("genero");
            entity.Property(e => e.IdCursoF).HasColumnName("id_CursoF");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Rol).HasColumnName("rol");
            entity.Property(e => e.FechaNacimiento).IsRequired().HasColumnName("fechaNacimiento");

            entity.HasOne(d => d.IdCursoFNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdCursoF)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_Curso");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<ApiPryBullyng.Models.DB.MensajesNoLeidos> MensajesNoLeidos { get; set; } = default!;
}
