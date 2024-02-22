﻿using Microsoft.AspNetCore.Components; 
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac
{
    public partial class Farmaco <T> where T : BasePaciente {

        // Parametros
        [Parameter] public T Paciente { get; set; } = null!;

        // Callback para devolver el valor actualizado
        [Parameter] public EventCallback<T> PacienteChanged { get; set; }
    }
}
