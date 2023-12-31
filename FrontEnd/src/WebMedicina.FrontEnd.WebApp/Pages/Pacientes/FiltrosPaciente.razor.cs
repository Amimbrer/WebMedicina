﻿using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes
{
    public partial class FiltrosPaciente {
        [CascadingParameter(Name = "modoOscuro")] bool IsDarkMode { get; set; } // Modo oscuro
        [Inject] private IPacientesService _pacientesService { get; set; } = null!;
        [Inject] private IComun _comun { get; set; } = null!;


        // Para identificar si el panel debe estar abierto o no
        [Parameter] public bool FiltroAbierto { get; set; }
        [Parameter] public EventCallback<bool> FiltroAbiertoChanged { get; set; }

        // Lista de pacientes bindeada bidireccinalmente
        [Parameter] public List<CrearPacienteDto>? ListaPaciente { get; set; }
        [Parameter] public EventCallback<List<CrearPacienteDto>?> ListaPacienteChanged { get; set; }

        // Listas no bindeadas 
        [CascadingParameter(Name = "ListaEpilepsias")] public IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        [CascadingParameter(Name = "ListaMutaciones")] public IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;

        // Filtros seleccionados
        private FiltroPacienteDto FiltrosPacientes { get; set; } = new();

        // Lista de medicos para filtrar
        private IEnumerable<UserInfoDto>? ListaMedicos { get; set; } = null;


        // Filtrar Pacientes 
        private async Task ObtenerPacientesFiltrados() {
            try {
                // Actualizamos la lista de pacientes
                ListaPaciente = await _pacientesService.FiltrarPacientes(FiltrosPacientes);
                await ListaPacienteChanged.InvokeAsync(ListaPaciente);

                // Cerramos drawe y bindeamos parametros
                await CerrarDrawer();
            } catch (Exception) { 
                throw;
            }
        }

        // Buscador para autocomplete de medicos
        private async Task<IEnumerable<UserInfoDto>> BuscarMedPac(string? busqueda) {
            try {
                // Si la lista es null se obtiene por primera vez de BD
                ListaMedicos ??= await _pacientesService.ObtenerAllMed();

                // Si hay medicos en la lista se realiza la busqueda
                if (!string.IsNullOrWhiteSpace(busqueda) && ListaMedicos != null && ListaMedicos.Any()) {
                    return ListaMedicos.Where(q => ($"{q.UserLogin} {q.Nombre} {q.Apellidos}").Contains(busqueda, StringComparison.OrdinalIgnoreCase));
                } 

                return ListaMedicos ?? Enumerable.Empty<UserInfoDto>();
            } catch (Exception) {
                throw;
            }
        }


        /// <summary>
        /// Resetear filtros y lista de pacientes
        /// </summary>
        /// <returns></returns>
        private async Task ResetearFiltrado() {
            try {
                FiltrosPacientes = new();
                ListaPaciente = await _pacientesService.FiltrarPacientes(null);
                await ListaPacienteChanged.InvokeAsync(ListaPaciente);
            } catch (Exception) {
                throw;
            }
        }

        private Func<EpilepsiasDto, string> ConvertirEpi = tipo => tipo.Nombre;
        private Func<MutacionesDto, string> ConvertirMut = tipo => tipo.Nombre;

        private async Task AbrirCerrarDrawer() {
            try {
                if (FiltroAbierto) {
                    await _comun.BloquearScroll("#app");
                } else {
                    await _comun.DesbloquearScroll("#app");
                }
                await FiltroAbiertoChanged.InvokeAsync(FiltroAbierto);
            } catch (Exception) {
                throw;
            }
        }

        // Cerrar drawer 
        private async Task CerrarDrawer() {
            try {
                FiltroAbierto = false;
                await AbrirCerrarDrawer();
            } catch (Exception) {
                throw;
            }
        }
    }
}
