﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebMedicina.BackEnd.Model;

public partial class WebmedicinaContext : DbContext
{
    public WebmedicinaContext()
    {
    }

    public WebmedicinaContext(DbContextOptions<WebmedicinaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aspnetrole> Aspnetroles { get; set; }

    public virtual DbSet<Aspnetroleclaim> Aspnetroleclaims { get; set; }

    public virtual DbSet<Aspnetuser> Aspnetusers { get; set; }

    public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; }

    public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; }

    public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Epilepsia> Epilepsias { get; set; }

    public virtual DbSet<Farmaco> Farmacos { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Medicospaciente> Medicospacientes { get; set; }

    public virtual DbSet<Mutacione> Mutaciones { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=webmedicina;user=userWebMedicina;password=WebMedicina", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Aspnetrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroles");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<Aspnetroleclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroleclaims");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetroleclaims)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<Aspnetuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetusers");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.AccessFailedCount).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEnd).HasMaxLength(6);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Aspnetuserrole",
                    r => r.HasOne<Aspnetrole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                    l => l.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("aspnetuserroles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Aspnetuserclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetuserclaims");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserclaims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetuserlogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("aspnetuserlogins");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserlogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetusertoken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("aspnetusertokens");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetusertokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Epilepsia>(entity =>
        {
            entity.HasKey(e => e.IdEpilepsia).HasName("PRIMARY");

            entity
                .ToTable("epilepsias")
                .UseCollation("utf8mb4_spanish2_ci");

            entity.Property(e => e.IdEpilepsia)
                .HasColumnType("int(11)")
                .HasColumnName("idEpilepsia");
            entity.Property(e => e.FechaCreac)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaCreac");
            entity.Property(e => e.FechaUltMod)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaUltMod");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Farmaco>(entity =>
        {
            entity.HasKey(e => e.IdFarmaco).HasName("PRIMARY");

            entity
                .ToTable("farmacos")
                .UseCollation("utf8mb4_spanish2_ci");

            entity.Property(e => e.IdFarmaco)
                .HasColumnType("int(11)")
                .HasColumnName("idFarmaco");
            entity.Property(e => e.FechaCreac)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaCreac");
            entity.Property(e => e.FechaUltMod)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaUltMod");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.IdMedico).HasName("PRIMARY");

            entity
                .ToTable("medicos")
                .UseCollation("utf8mb4_spanish2_ci");

            entity.HasIndex(e => e.UserLogin, "userLogin").IsUnique();

            entity.HasIndex(e => e.NetuserId, "Índice 2");

            entity.Property(e => e.IdMedico)
                .HasColumnType("int(11)")
                .HasColumnName("idMedico");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .HasColumnName("apellidos");
            entity.Property(e => e.FechaCreac)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaCreac");
            entity.Property(e => e.FechaNac)
                .HasDefaultValueSql("curdate()")
                .HasColumnType("datetime")
                .HasColumnName("fechaNac");
            entity.Property(e => e.FechaUltMod)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaUltMod");
            entity.Property(e => e.NetuserId)
                .HasColumnName("netuserId")
                .UseCollation("utf8mb4_general_ci");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .HasDefaultValueSql("''")
                .HasColumnName("sexo");
            entity.Property(e => e.UserLogin)
                .HasMaxLength(50)
                .HasColumnName("userLogin");

            entity.HasOne(d => d.Netuser).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.NetuserId)
                .HasConstraintName("FK_medicos_aspnetusers");
        });

        modelBuilder.Entity<Medicospaciente>(entity =>
        {
            entity.HasKey(e => e.IdMedPac).HasName("PRIMARY");

            entity.ToTable("medicospacientes", tb => tb.HasComment("Relacion de que medicos pueden editar que pacientes"));

            entity.HasIndex(e => e.IdPaciente, "FK_medicospacientes_pacientes");

            entity.HasIndex(e => new { e.IdMedico, e.IdPaciente }, "idUsuario_idPaciente");

            entity.Property(e => e.IdMedPac)
                .HasColumnType("int(11)")
                .HasColumnName("idMedPac");
            entity.Property(e => e.IdMedico)
                .HasColumnType("int(11)")
                .HasColumnName("idMedico");
            entity.Property(e => e.IdPaciente)
                .HasColumnType("int(11)")
                .HasColumnName("idPaciente");

            entity.HasOne(d => d.IdMedicoNavigation).WithMany(p => p.Medicospacientes)
                .HasForeignKey(d => d.IdMedico)
                .HasConstraintName("FK_medicospacientes_medicos");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Medicospacientes)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("FK_medicospacientes_pacientes");
        });

        modelBuilder.Entity<Mutacione>(entity =>
        {
            entity.HasKey(e => e.IdMutacion).HasName("PRIMARY");

            entity
                .ToTable("mutaciones")
                .UseCollation("utf8mb4_spanish2_ci");

            entity.Property(e => e.IdMutacion)
                .HasColumnType("int(11)")
                .HasColumnName("idMutacion");
            entity.Property(e => e.FechaCreac)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaCreac");
            entity.Property(e => e.FechaUltMod)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaUltMod");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PRIMARY");

            entity
                .ToTable("pacientes")
                .UseCollation("utf8mb4_spanish2_ci");

            entity.HasIndex(e => e.IdFarmaco, "idFarmaco");

            entity.HasIndex(e => e.IdMutacion, "idMutacion");

            entity.HasIndex(e => e.IdEpilepsia, "idTipoEpilepsia");

            entity.HasIndex(e => e.MedicoCreador, "medicoCreador");

            entity.HasIndex(e => e.MedicoUltMod, "medicoUltMod");

            entity.Property(e => e.IdPaciente)
                .HasColumnType("int(11)")
                .HasColumnName("idPaciente");
            entity.Property(e => e.DescripEnferRaras)
                .HasDefaultValueSql("''")
                .HasColumnType("text")
                .HasColumnName("descripEnferRaras");
            entity.Property(e => e.EnfermRaras)
                .HasMaxLength(1)
                .HasDefaultValueSql("''")
                .HasColumnName("enfermRaras");
            entity.Property(e => e.FechaCreac)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaCreac");
            entity.Property(e => e.FechaDiagnostico)
                .HasDefaultValueSql("curdate()")
                .HasColumnType("datetime")
                .HasColumnName("fechaDiagnostico");
            entity.Property(e => e.FechaFractalidad)
                .HasDefaultValueSql("curdate()")
                .HasColumnType("datetime")
                .HasColumnName("fechaFractalidad");
            entity.Property(e => e.FechaNac)
                .HasDefaultValueSql("curdate()")
                .HasColumnType("datetime")
                .HasColumnName("fechaNac");
            entity.Property(e => e.FechaUltMod)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaUltMod");
            entity.Property(e => e.IdEpilepsia)
                .HasColumnType("int(11)")
                .HasColumnName("idEpilepsia");
            entity.Property(e => e.IdFarmaco)
                .HasColumnType("int(11)")
                .HasColumnName("idFarmaco");
            entity.Property(e => e.IdMutacion)
                .HasColumnType("int(11)")
                .HasColumnName("idMutacion");
            entity.Property(e => e.MedicoCreador)
                .HasColumnType("int(11)")
                .HasColumnName("medicoCreador");
            entity.Property(e => e.MedicoUltMod)
                .HasColumnType("int(11)")
                .HasColumnName("medicoUltMod");
            entity.Property(e => e.NumHistoria).HasMaxLength(50);
            entity.Property(e => e.Sexo)
                .HasDefaultValueSql("'H'")
                .HasColumnType("enum('H','M')")
                .HasColumnName("sexo");
            entity.Property(e => e.Talla)
                .HasPrecision(20, 6)
                .HasColumnName("talla");

            entity.HasOne(d => d.IdEpilepsiaNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdEpilepsia)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_pacientes_epilepsias");

            entity.HasOne(d => d.IdFarmacoNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdFarmaco)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_pacientes_farmacos");

            entity.HasOne(d => d.IdMutacionNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdMutacion)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_pacientes_mutaciones");

            entity.HasOne(d => d.MedicoCreadorNavigation).WithMany(p => p.PacienteMedicoCreadorNavigations)
                .HasForeignKey(d => d.MedicoCreador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pacientes_medicos_2");

            entity.HasOne(d => d.MedicoUltModNavigation).WithMany(p => p.PacienteMedicoUltModNavigations)
                .HasForeignKey(d => d.MedicoUltMod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pacientes_medicos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
