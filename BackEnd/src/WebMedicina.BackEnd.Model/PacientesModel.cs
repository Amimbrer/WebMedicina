﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedicina.BackEnd.Model;

public partial class PacientesModel : BaseModel, IEquatable<PacientesModel>
{
    public int IdPaciente { get; set; }

    public string NumHistoria { get; set; } = null!;

    public DateTime FechaNac { get; set; }

    public string Sexo { get; set; } = null!;

    public int Talla { get; set; }

    public DateTime FechaDiagnostico { get; set; }

    public DateTime FechaFractalidad { get; set; }

    public string Farmaco { get; set; } = null!;

    public int? IdEpilepsia { get; set; }

    public int? IdMutacion { get; set; }

    public string EnfermRaras { get; set; } = null!;

    public string DescripEnferRaras { get; set; } = null!;

    public int MedicoUltMod { get; set; }

    public int MedicoCreador { get; set; }

    public virtual EpilepsiaModel? IdEpilepsiaNavigation { get; set; }

    public virtual MutacionesModel? IdMutacionNavigation { get; set; }

    public virtual MedicosModel MedicoCreadorNavigation { get; set; } = null!;

    [ConcurrencyCheck]
    public virtual MedicosModel MedicoUltModNavigation { get; set; } = null!;

    public virtual ICollection<MedicospacienteModel> Medicospacientes { get; set; } = new List<MedicospacienteModel>();

    public virtual EtapaLTModel? UltimaEtapa { get; set; } = null;

    // Comparar propiedades
    public bool Equals(PacientesModel? obj) {
        return obj != null && (ReferenceEquals(obj, this) || (obj.IdPaciente == this.IdPaciente && obj.NumHistoria == this.NumHistoria && obj.FechaNac == this.FechaNac && obj.Sexo == this.Sexo && obj.Talla == this.Talla && obj.FechaFractalidad == this.FechaFractalidad && obj.FechaDiagnostico == this.FechaDiagnostico && 
            obj.IdEpilepsia == this.IdEpilepsia && obj.Farmaco == this.Farmaco && obj.IdMutacion == this.IdMutacion && obj.EnfermRaras == this.EnfermRaras && obj.FechaCreac == this.FechaCreac && obj.FechaUltMod == this.FechaUltMod && obj.MedicoUltMod == this.MedicoUltMod && obj.MedicoCreador == this.MedicoCreador &&
            obj.DescripEnferRaras == this.DescripEnferRaras && obj.EnfermRaras == this.EnfermRaras));
    }
}
