﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Immutable;
using System.Net.Http.Json;
using Neuromorfismo.FrontEnd.ServiceDependencies;
using Neuromorfismo.Shared.Dto.Pacientes;
using Neuromorfismo.Shared.Dto.Tipos;
using Neuromorfismo.Shared.Service;

namespace Neuromorfismo.FrontEnd.WebApp.Pages.Pacientes
{
    public partial class TablaPacientes {
        [Inject] private IPacientesService _pacientesService { get; set; } = null!;
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] private IDialogService DialogService { get; set; } = null!;


        // Parametros
        [Parameter] public ImmutableList<CrearPacienteDto>? ListaPacientes { get; set; }
        [Parameter] public EventCallback<int> EliminarPacienteList { get; set; } // Evento callback para eliminar paciente
        [CascadingParameter(Name = "ListaEpilepsias")] public IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        [CascadingParameter(Name = "ListaMutaciones")] public IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;
        [Parameter] public EventCallback<CrearPacienteDto> MostrarLineaTemp { get; set; } // Evento callback para mostrar la linea temporal de un paciente

        // Configuracion de dialogo de edicion
        private DialogOptions OpcionesDialogo { get; set; } = new DialogOptions { FullWidth = true, CloseButton = true, MaxWidth= MaxWidth.Small, 
            DisableBackdropClick = true, Position = DialogPosition.Center};

        // Realizar busqueda por search
        private string _searchString { get; set; } = string.Empty;




        /// <summary>
        /// Confimrar la eliminacion de un paciente
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        private async Task ConfirmarEliminacion(CrearPacienteDto paciente) {
            // Mostramos mensaje de confirmacion
            bool? result = await DialogService.ShowMessageBox(
                "Eliminar Paciente",
                (MarkupString)$"¿Está seguro que desea eliminar al paciente <b>{paciente.NumHistoria}</b>?",
                yesText: "Confirmar", noText: "Cancelar");

            if(result is not null && result == true) {
                await EliminarPaciente(paciente);
            }
        }
        

        /// <summary>
        ///  Evento para eliminar paciente
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        private async Task EliminarPaciente(CrearPacienteDto paciente) { 
            HttpResponseMessage respuesta = await _pacientesService.EliminarPaciente(paciente.IdPaciente);

            // Mensaje para mostrar el usuario
            Severity tipoSnacbar = Severity.Success;
            string mensaje = "Paciente eliminado exitosamente.";

            if (respuesta.IsSuccessStatusCode) {

                // Validamos si el paciente ha podido ser creado
                if (await respuesta.Content.ReadFromJsonAsync<bool>() == false) {
                    mensaje = "El paciente no ha podido ser eliminado. Inténtelo de nuevo o conteacte con un administrador.";
                    tipoSnacbar = Severity.Error;
                } else {
                    // Actualizamos la lista de pacientes eliminando al paciente 
                    await EliminarPacienteList.InvokeAsync(paciente.IdPaciente);
                }
            } else {
                mensaje = await respuesta.Content.ReadAsStringAsync();
                tipoSnacbar = Severity.Error;
            }

            _snackbar.Add(mensaje, tipoSnacbar); 
        }

        /// <summary>
        /// Filtrar por search
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        private bool Search(CrearPacienteDto paciente) { 
            bool valido = false;
                
            // Filtramos la busqueda por el campo search
            if (string.IsNullOrWhiteSpace(_searchString) || paciente is null) {
                valido = true;
            } else if (paciente.NumHistoria.Contains(_searchString, StringComparison.OrdinalIgnoreCase)) {
                valido = true;
            } else if (paciente.FechaNac?.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true) {
                valido = true;
            } else if (paciente.FechaDiagnostico?.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true) {
                valido = true;
            } else if (paciente.FechaFractalidad?.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true) {
                valido = true;
            } else if (paciente.Farmaco.Contains(_searchString, StringComparison.OrdinalIgnoreCase)) {
                valido = true;
            } else if (paciente.Epilepsia is not null && paciente.Epilepsia.Nombre.Contains(_searchString, StringComparison.OrdinalIgnoreCase)) {
                valido = true;
            } else if (paciente.Mutacion is not null && paciente.Mutacion.Nombre.Contains(_searchString, StringComparison.OrdinalIgnoreCase)) {
                valido = true;
            } else if (paciente.Talla.ToString().Equals(_searchString, StringComparison.OrdinalIgnoreCase)) {
                valido = true;
            }

            return valido; 
        }

        /// <summary>
        /// Mostrar dialogo para editar paciente
        /// </summary>
        /// <param name="nuevoPaciente"></param>
        /// <returns></returns>
        private async Task MostrarEditarPac(CrearPacienteDto nuevoPaciente) { 
            // Creamos copia de seguridad del paciente
            CrearPacienteDto copiaPaciente = nuevoPaciente.ClonarManual();

            // Generamos parametros para modal crear paciente
            DialogParameters parametros = new() {
                { "ListaEpilepsias", ListaEpilepsias },
                { "ListaMutaciones", ListaMutaciones },
                { "nuevoPaciente", nuevoPaciente },
            };

            var dialog = DialogService.Show<EditarPaciente>("Nuevo Paciente", parametros, OpcionesDialogo);
            var respuesta = await dialog.Result;
            if(!respuesta.Canceled && respuesta.Data != null && respuesta.Data is CrearPacienteDto pacienteEditado) {
                nuevoPaciente = pacienteEditado;
            } else {
                _pacientesService.ReiniciarCopiaPaciente(ref nuevoPaciente, copiaPaciente);
            } 
        }
    }
}
