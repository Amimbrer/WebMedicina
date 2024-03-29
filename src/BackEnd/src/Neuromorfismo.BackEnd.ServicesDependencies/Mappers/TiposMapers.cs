﻿using Neuromorfismo.BackEnd.Model;
using Neuromorfismo.Shared.Dto.Tipos;

namespace Neuromorfismo.BackEnd.ServicesDependencies.Mappers {
    public static class TiposMapers {
        // MutacionesModel to MutacionesDto
        public static MutacionesDto ToDto(this MutacionesModel modelo) =>
            new() {
                FechaCreac = modelo.FechaCreac,
                FechaUltMod = modelo.FechaUltMod,
                IdMutacion = modelo.IdMutacion,
                Nombre = modelo.Nombre
            };


        // EpilepsiasModel to EpilepsiasDto
        public static EpilepsiasDto ToDto(this EpilepsiaModel modelo) =>
           new() {
               FechaCreac = modelo.FechaCreac,
               FechaUltMod = modelo.FechaUltMod,
               IdEpilepsia = modelo.IdEpilepsia,
               Nombre = modelo.Nombre
           };


        // FarmacoModel to FarmacosDto
        public static FarmacosDto ToDto(this FarmacosModel modelo) =>
        new() {
            FechaCreac = modelo.FechaCreac,
            FechaUltMod = modelo.FechaUltMod,
            IdFarmaco = modelo.IdFarmaco,
            Nombre = modelo.Nombre
        };
    }
}
