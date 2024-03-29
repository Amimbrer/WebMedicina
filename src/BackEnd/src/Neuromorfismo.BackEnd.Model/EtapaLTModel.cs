﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neuromorfismo.BackEnd.Model {

    [Table("Etapas")]
    public class EtapaLTModel : BaseModel {
        [Key]
        public short Id { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string Titulo { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        public string Label { get; set; } = default!;

        [MaxLength(250)]
        public string? Descripcion { get; set; }

        public int? IdMedicoCreador { get; set; } = null;
        [ForeignKey("IdMedicoCreador")]
        public MedicosModel? MedicoCreador { get; set; } = null;

        public int? IdMedicoUltModif { get; set; } = null;

        [ForeignKey("IdMedicoUltModif")]
        public MedicosModel? MedicoUltModif { get; set; } = null;

        public ICollection<EvolucionLTModel>? EvolucionesEtapa { get; set;} = null;
    }
}
