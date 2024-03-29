﻿using Microsoft.AspNetCore.Components;
using Neuromorfismo.Shared.Dto.Pacientes;

namespace Neuromorfismo.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac
{
    public partial class Talla <T> where T : BasePaciente {
        [Parameter] public T Paciente { get; set; } = null!; // Parametros
        [Parameter] public EventCallback<T> PacienteChanged { get; set; } // Callback para devolver el valor actualizado
        [Parameter] public string Label { get; set; } = "Talla*";
    }
}
