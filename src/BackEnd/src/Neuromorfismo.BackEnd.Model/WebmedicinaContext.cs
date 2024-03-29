﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Neuromorfismo.BackEnd.Model.Seeds;

namespace Neuromorfismo.BackEnd.Model;

public class NeuromorfismoContext : IdentityDbContext<UserModel, RoleModel, string> {
    public NeuromorfismoContext() {
    }

    public NeuromorfismoContext(DbContextOptions<NeuromorfismoContext> options)
        : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    //    string connectionString = "Server=127.0.0.1;Port=3306;DataBase=webmedicina;User=userWebMedicina;Password=Neuromorfismo;SSL Mode=Required;"+
    //                            "CertificateFile=F:\\Program Files\\MariaDB 11.3\\data\\ssl\\client.pfx;CertificatePassword=12345;AllowUserVariables=True;";
    //    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    //}

    public virtual DbSet<EpilepsiaModel> Epilepsias { get; set; }

    public virtual DbSet<FarmacosModel> Farmacos { get; set; }

    public virtual DbSet<MedicosModel> Medicos { get; set; }

    public virtual DbSet<MedicospacienteModel> Medicospacientes { get; set; }

    public virtual DbSet<MutacionesModel> Mutaciones { get; set; }

    public virtual DbSet<PacientesModel> Pacientes { get; set; }

    public virtual DbSet<EvolucionLTModel> EvolucionLT { get; set; }

    public virtual DbSet<EtapaLTModel> EtapaLT { get; set; }

    public virtual DbSet<UserRefreshTokens> UserRefreshToken { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RolesSeed());

        #if DEBUG
            // Seeds para pruebas
            modelBuilder.ApplyConfiguration(new EpilepsiasSeed());
            modelBuilder.ApplyConfiguration(new MutacionSeed());
            modelBuilder.ApplyConfiguration(new EtapasLTSeed());
        #endif


        modelBuilder.Entity<EpilepsiaModel>(entity =>
        {
            entity.HasKey(e => e.IdEpilepsia).HasName("PRIMARY");

            entity.Property(e => e.IdEpilepsia)
                .HasColumnType("int(11)")
                .HasColumnName("idEpilepsia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<FarmacosModel>(entity =>
        {
            entity.HasKey(e => e.IdFarmaco).HasName("PRIMARY");

            entity.Property(e => e.IdFarmaco)
                .HasColumnType("int(11)")
                .HasColumnName("idFarmaco");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<MedicosModel>(entity =>
        {
            entity.HasKey(e => e.IdMedico).HasName("PRIMARY");

            entity.HasIndex(e => e.UserLogin, "userLogin").IsUnique();

            entity.HasIndex(e => e.NetuserId, "Índice 2");

            entity.Property(e => e.IdMedico)
                .HasColumnType("int(11)")
                .HasColumnName("idMedico");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .HasColumnName("apellidos");
            entity.Property(e => e.FechaNac)
                .HasColumnType("datetime")
                .HasColumnName("fechaNac");
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

            entity.HasOne(d => d.Netuser).WithOne(p => p.Medico)
                .HasConstraintName("FK_medicos_aspnetusers");
        });

        modelBuilder.Entity<MedicospacienteModel>(entity =>
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

        modelBuilder.Entity<MutacionesModel>(entity =>
        {
            entity.HasKey(e => e.IdMutacion).HasName("PRIMARY");


            entity.Property(e => e.IdMutacion)
                .HasColumnType("int(11)")
                .HasColumnName("idMutacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<PacientesModel>(entity => {
            entity.HasKey(e => e.IdPaciente).HasName("PRIMARY");

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
            entity.Property(e => e.Farmaco)
                .HasMaxLength(250)
                .HasColumnName("farmaco");
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
            entity.Property(e => e.IdEpilepsia)
                .HasColumnType("int(11)")
                .HasColumnName("idEpilepsia");
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
        base.OnModelCreating(modelBuilder);
    }
}
