﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebMedicina.BackEnd.Model;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    [DbContext(typeof(WebmedicinaContext))]
    [Migration("20240118235235_IdMedicoForeignKey4")]
    partial class IdMedicoForeignKey4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Aspnetuserrole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");

                    b.ToTable("aspnetuserroles", (string)null);
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.AspnetroleModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "NormalizedName" }, "RoleNameIndex")
                        .IsUnique();

                    b.ToTable("aspnetroles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            ConcurrencyStamp = "3a60d352-fdb2-47d0-9ffd-60d85066df1d",
                            Name = "superAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = "2",
                            ConcurrencyStamp = "7693e3f5-7d41-465b-9043-f9b3f74b468a",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "3",
                            ConcurrencyStamp = "fbf51c11-2da4-4383-85ca-0d45c706f9c0",
                            Name = "medico",
                            NormalizedName = "MEDICO"
                        });
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.AspnetroleclaimModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "RoleId" }, "IX_AspNetRoleClaims_RoleId");

                    b.ToTable("aspnetroleclaims", (string)null);
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.AspnetuserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int(11)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LockoutEnd")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "NormalizedEmail" }, "EmailIndex");

                    b.HasIndex(new[] { "NormalizedUserName" }, "UserNameIndex")
                        .IsUnique();

                    b.ToTable("aspnetusers", (string)null);
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.Aspnetuserclaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "UserId" }, "IX_AspNetUserClaims_UserId");

                    b.ToTable("aspnetuserclaims", (string)null);
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.Aspnetuserlogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "UserId" }, "IX_AspNetUserLogins_UserId");

                    b.ToTable("aspnetuserlogins", (string)null);
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.AspnetusertokenModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                    b.ToTable("aspnetusertokens", (string)null);
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.EpilepsiaModel", b =>
                {
                    b.Property<int>("IdEpilepsia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idEpilepsia");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre")
                        .HasDefaultValueSql("''");

                    b.HasKey("IdEpilepsia")
                        .HasName("PRIMARY");

                    b.ToTable("Epilepsias");

                    b.HasData(
                        new
                        {
                            IdEpilepsia = 1,
                            FechaCreac = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            Nombre = "Epilepsia1"
                        },
                        new
                        {
                            IdEpilepsia = 2,
                            FechaCreac = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            Nombre = "Epilepsia2"
                        });
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.EtapaLTModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("IdMedicoCreador")
                        .HasColumnType("int(11)");

                    b.Property<int?>("IdMedicoUltModif")
                        .IsConcurrencyToken()
                        .HasColumnType("int(11)");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IdMedicoCreador");

                    b.HasIndex("IdMedicoUltModif");

                    b.ToTable("EtapaLT");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "",
                            FechaCreac = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            Label = "¿Ha dado su consentimiento el paciente?",
                            Titulo = "Consentimiento Informado"
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "Descripcion",
                            FechaCreac = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Label = "¿Ha dado su consentimiento el paciente?",
                            Titulo = "Paciente Acude a Extracción Analítica"
                        },
                        new
                        {
                            Id = 3,
                            Descripcion = "",
                            FechaCreac = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            Label = "¿Ha dado su consentimiento el paciente?",
                            Titulo = "Muestra de Genética"
                        });
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.EvolucionLTModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Confirmado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdEtapa")
                        .HasColumnType("int");

                    b.Property<int>("IdMedicoUltModif")
                        .HasColumnType("int(11)");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("IdEtapa");

                    b.HasIndex("IdMedicoUltModif");

                    b.HasIndex("IdPaciente");

                    b.ToTable("EvolucionLT");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.FarmacosModel", b =>
                {
                    b.Property<int>("IdFarmaco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idFarmaco");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre")
                        .HasDefaultValueSql("''");

                    b.HasKey("IdFarmaco")
                        .HasName("PRIMARY");

                    b.ToTable("Farmacos");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.MedicosModel", b =>
                {
                    b.Property<int>("IdMedico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idMedico");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("apellidos");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaNac")
                        .HasColumnType("datetime")
                        .HasColumnName("fechaNac");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NetuserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("netuserId")
                        .UseCollation("utf8mb4_general_ci");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)")
                        .HasColumnName("sexo")
                        .HasDefaultValueSql("''");

                    b.Property<string>("UserLogin")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("userLogin");

                    b.HasKey("IdMedico")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "UserLogin" }, "userLogin")
                        .IsUnique();

                    b.HasIndex(new[] { "NetuserId" }, "Índice 2");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.MedicospacienteModel", b =>
                {
                    b.Property<int>("IdMedPac")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idMedPac");

                    b.Property<int>("IdMedico")
                        .HasColumnType("int(11)")
                        .HasColumnName("idMedico");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int(11)")
                        .HasColumnName("idPaciente");

                    b.HasKey("IdMedPac")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdPaciente" }, "FK_medicospacientes_pacientes");

                    b.HasIndex(new[] { "IdMedico", "IdPaciente" }, "idUsuario_idPaciente");

                    b.ToTable("medicospacientes", null, t =>
                        {
                            t.HasComment("Relacion de que medicos pueden editar que pacientes");
                        });
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.MutacionesModel", b =>
                {
                    b.Property<int>("IdMutacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idMutacion");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre")
                        .HasDefaultValueSql("''");

                    b.HasKey("IdMutacion")
                        .HasName("PRIMARY");

                    b.ToTable("Mutaciones");

                    b.HasData(
                        new
                        {
                            IdMutacion = 1,
                            FechaCreac = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            Nombre = "Mutacion1"
                        },
                        new
                        {
                            IdMutacion = 2,
                            FechaCreac = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            Nombre = "Mutacion2"
                        });
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.PacientesModel", b =>
                {
                    b.Property<int>("IdPaciente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idPaciente");

                    b.Property<string>("DescripEnferRaras")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("descripEnferRaras")
                        .HasDefaultValueSql("''");

                    b.Property<string>("EnfermRaras")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)")
                        .HasColumnName("enfermRaras")
                        .HasDefaultValueSql("''");

                    b.Property<string>("Farmaco")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)")
                        .HasColumnName("farmaco");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaDiagnostico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaDiagnostico")
                        .HasDefaultValueSql("curdate()");

                    b.Property<DateTime>("FechaFractalidad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaFractalidad")
                        .HasDefaultValueSql("curdate()");

                    b.Property<DateTime>("FechaNac")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaNac")
                        .HasDefaultValueSql("curdate()");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("IdEpilepsia")
                        .HasColumnType("int(11)")
                        .HasColumnName("idEpilepsia");

                    b.Property<int?>("IdMutacion")
                        .HasColumnType("int(11)")
                        .HasColumnName("idMutacion");

                    b.Property<int?>("IdUltimaEtapa")
                        .HasColumnType("int");

                    b.Property<int>("MedicoCreador")
                        .HasColumnType("int(11)")
                        .HasColumnName("medicoCreador");

                    b.Property<int>("MedicoUltMod")
                        .HasColumnType("int(11)")
                        .HasColumnName("medicoUltMod");

                    b.Property<string>("NumHistoria")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("enum('H','M')")
                        .HasColumnName("sexo")
                        .HasDefaultValueSql("'H'");

                    b.Property<int>("Talla")
                        .HasPrecision(20, 6)
                        .HasColumnType("int")
                        .HasColumnName("talla");

                    b.HasKey("IdPaciente")
                        .HasName("PRIMARY");

                    b.HasIndex("IdUltimaEtapa");

                    b.HasIndex(new[] { "IdMutacion" }, "idMutacion");

                    b.HasIndex(new[] { "IdEpilepsia" }, "idTipoEpilepsia");

                    b.HasIndex(new[] { "MedicoCreador" }, "medicoCreador");

                    b.HasIndex(new[] { "MedicoUltMod" }, "medicoUltMod");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Aspnetuserrole", b =>
                {
                    b.HasOne("WebMedicina.BackEnd.Model.AspnetroleModel", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId");

                    b.HasOne("WebMedicina.BackEnd.Model.AspnetuserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.AspnetroleclaimModel", b =>
                {
                    b.HasOne("WebMedicina.BackEnd.Model.AspnetroleModel", "Role")
                        .WithMany("Aspnetroleclaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.Aspnetuserclaim", b =>
                {
                    b.HasOne("WebMedicina.BackEnd.Model.AspnetuserModel", "User")
                        .WithMany("Aspnetuserclaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.Aspnetuserlogin", b =>
                {
                    b.HasOne("WebMedicina.BackEnd.Model.AspnetuserModel", "User")
                        .WithMany("Aspnetuserlogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.AspnetusertokenModel", b =>
                {
                    b.HasOne("WebMedicina.BackEnd.Model.AspnetuserModel", "User")
                        .WithMany("Aspnetusertokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.EtapaLTModel", b =>
                {
                    b.HasOne("WebMedicina.BackEnd.Model.MedicosModel", "MedicoCreador")
                        .WithMany()
                        .HasForeignKey("IdMedicoCreador");

                    b.HasOne("WebMedicina.BackEnd.Model.MedicosModel", "MedicoUltModif")
                        .WithMany()
                        .HasForeignKey("IdMedicoUltModif");

                    b.Navigation("MedicoCreador");

                    b.Navigation("MedicoUltModif");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.EvolucionLTModel", b =>
                {
                    b.HasOne("WebMedicina.BackEnd.Model.EtapaLTModel", "Etapa")
                        .WithMany("EvolucionEtapa")
                        .HasForeignKey("IdEtapa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMedicina.BackEnd.Model.MedicosModel", "MedicoUltModif")
                        .WithMany("EvolucionMedicoUltModif")
                        .HasForeignKey("IdMedicoUltModif")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMedicina.BackEnd.Model.PacientesModel", "Paciente")
                        .WithMany("Evoluciones")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etapa");

                    b.Navigation("MedicoUltModif");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.MedicosModel", b =>
                {
                    b.HasOne("WebMedicina.BackEnd.Model.AspnetuserModel", "Netuser")
                        .WithMany("Medicos")
                        .HasForeignKey("NetuserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_medicos_aspnetusers");

                    b.Navigation("Netuser");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.MedicospacienteModel", b =>
                {
                    b.HasOne("WebMedicina.BackEnd.Model.MedicosModel", "IdMedicoNavigation")
                        .WithMany("Medicospacientes")
                        .HasForeignKey("IdMedico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_medicospacientes_medicos");

                    b.HasOne("WebMedicina.BackEnd.Model.PacientesModel", "IdPacienteNavigation")
                        .WithMany("Medicospacientes")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_medicospacientes_pacientes");

                    b.Navigation("IdMedicoNavigation");

                    b.Navigation("IdPacienteNavigation");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.PacientesModel", b =>
                {
                    b.HasOne("WebMedicina.BackEnd.Model.EpilepsiaModel", "IdEpilepsiaNavigation")
                        .WithMany("Pacientes")
                        .HasForeignKey("IdEpilepsia")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_pacientes_epilepsias");

                    b.HasOne("WebMedicina.BackEnd.Model.MutacionesModel", "IdMutacionNavigation")
                        .WithMany("Pacientes")
                        .HasForeignKey("IdMutacion")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_pacientes_mutaciones");

                    b.HasOne("WebMedicina.BackEnd.Model.EtapaLTModel", "UltimaEtapa")
                        .WithMany("PacienteUltimaEtapa")
                        .HasForeignKey("IdUltimaEtapa");

                    b.HasOne("WebMedicina.BackEnd.Model.MedicosModel", "MedicoCreadorNavigation")
                        .WithMany("PacienteMedicoCreadorNavigations")
                        .HasForeignKey("MedicoCreador")
                        .IsRequired()
                        .HasConstraintName("FK_pacientes_medicos_2");

                    b.HasOne("WebMedicina.BackEnd.Model.MedicosModel", "MedicoUltModNavigation")
                        .WithMany("PacienteMedicoUltModNavigations")
                        .HasForeignKey("MedicoUltMod")
                        .IsRequired()
                        .HasConstraintName("FK_pacientes_medicos");

                    b.Navigation("IdEpilepsiaNavigation");

                    b.Navigation("IdMutacionNavigation");

                    b.Navigation("MedicoCreadorNavigation");

                    b.Navigation("MedicoUltModNavigation");

                    b.Navigation("UltimaEtapa");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.AspnetroleModel", b =>
                {
                    b.Navigation("Aspnetroleclaims");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.AspnetuserModel", b =>
                {
                    b.Navigation("Aspnetuserclaims");

                    b.Navigation("Aspnetuserlogins");

                    b.Navigation("Aspnetusertokens");

                    b.Navigation("Medicos");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.EpilepsiaModel", b =>
                {
                    b.Navigation("Pacientes");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.EtapaLTModel", b =>
                {
                    b.Navigation("EvolucionEtapa");

                    b.Navigation("PacienteUltimaEtapa");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.MedicosModel", b =>
                {
                    b.Navigation("EvolucionMedicoUltModif");

                    b.Navigation("Medicospacientes");

                    b.Navigation("PacienteMedicoCreadorNavigations");

                    b.Navigation("PacienteMedicoUltModNavigations");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.MutacionesModel", b =>
                {
                    b.Navigation("Pacientes");
                });

            modelBuilder.Entity("WebMedicina.BackEnd.Model.PacientesModel", b =>
                {
                    b.Navigation("Evoluciones");

                    b.Navigation("Medicospacientes");
                });
#pragma warning restore 612, 618
        }
    }
}
