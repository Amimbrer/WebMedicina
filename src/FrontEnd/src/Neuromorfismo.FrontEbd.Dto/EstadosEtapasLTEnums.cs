﻿using System.ComponentModel;

namespace Neuromorfismo.FrontEnd.Dto {

    public static class EstadosEtapasLT {

        // Estados de una etapa
        public enum EstadoEtapa {
            [Description("La etapa ya ha sido confirmada por un médico.")]
            Pasada,

            [Description("La etapa es la siguiente a confirmar.")]
            Presente,

            [Description("La etapa es futura y aún hay etapas por pasar.")]
            Futura,

            [Description("Fin de la evolución del paciente.")]
            FinEtapas
        }
    }
}
