﻿using System.ComponentModel;

namespace Neuromorfismo.Shared.Dto.Tipos {
    public class EpilepsiasDto : BaseTipoDto, ICloneable, IEquatable<EpilepsiasDto>{
        public int IdEpilepsia { get; init; }

        public object Clone() {
            return this.MemberwiseClone();
        }

        // Comparar clases        
        public bool Equals(EpilepsiasDto? other) {
            return other != null && (ReferenceEquals(other, this) || (other.Nombre == this.Nombre && other.FechaUltMod == this.FechaUltMod && other.FechaCreac == this.FechaCreac && other.IdEpilepsia == this.IdEpilepsia));
        }

        public override int GetHashCode() {
            return IdEpilepsia.GetHashCode() ^ Nombre.GetHashCode() ^ FechaUltMod.GetHashCode() ^ FechaCreac.GetHashCode();
        }

        public override bool Equals(object? obj) {
            return Equals(obj as EpilepsiasDto);
        }
    }
}
