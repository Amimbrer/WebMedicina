﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Neuromorfismo.BackEnd.Model.Seeds {

    public class EtapasLTSeed : IEntityTypeConfiguration<EtapaLTModel> {

        public void Configure(EntityTypeBuilder<EtapaLTModel> builder) {
                builder.HasData(
                    new EtapaLTModel { Id = 1, Label = "¿Ha dado su consentimiento el paciente?", Descripcion = "", Titulo = "Consentimiento Informado", FechaCreac = DateTime.Today, FechaUltMod = DateTime.Today},
                    new EtapaLTModel { Id = 2, Label = "¿Ha dado su consentimiento el paciente?", Descripcion = "Descripcion", Titulo = "Paciente Acude a Extracción Analítica", FechaCreac = DateTime.Today },
                    new EtapaLTModel { Id = 3, Label = "¿Ha dado su consentimiento el paciente?", Descripcion = "", Titulo = "Muestra de Genética", FechaCreac = DateTime.Today, FechaUltMod = DateTime.Today},
                    new EtapaLTModel { Id = 999, Label = string.Empty, Descripcion = "", Titulo = "Fin de la evolución del paciente", FechaCreac = DateTime.Today, FechaUltMod = DateTime.Today}
                );
        }
    }
}
