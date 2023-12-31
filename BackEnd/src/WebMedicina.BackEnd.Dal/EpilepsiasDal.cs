﻿using Microsoft.EntityFrameworkCore;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
using WebMedicina.Shared.Dto.Tipos;

namespace WebMedicina.BackEnd.Dal {
    public class EpilepsiasDal {
        private readonly WebmedicinaContext _context;

        public EpilepsiasDal(WebmedicinaContext context) {
            _context = context;
        }

        // Get EPILEPSIAS
        public List<EpilepsiasDto> GetEpilepsias() {
            try {
                List<EpilepsiasDto> listaEpilepsias = _context.Epilepsias.Select(q => q.ToDto()).ToList();
                if (listaEpilepsias.Count > 0) {
                    for (int i = 0; i < listaEpilepsias.Count; i++) {
                        listaEpilepsias[i].Indice = i+1;
                    }
                }
                return listaEpilepsias;
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Create EPILEPSIAS
        public async Task<bool> CrearEpilepsia(string nombre) {
            try {
                EpilepsiaModel nuevaEpilepsia = new() {
                    Nombre = nombre
                };

                await _context.Epilepsias.AddAsync(nuevaEpilepsia);
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        // Delete EPILEPSIAS
        public async Task<bool> DeleteEpilepsia(int idEpilepsia) {
            try {
                EpilepsiaModel? epilepsiaBorrada = await _context.Epilepsias.FindAsync(idEpilepsia);
                if (epilepsiaBorrada != null) {
                    _context.Epilepsias.Remove(epilepsiaBorrada);
                }
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        // Update EPILEPSIAS
        public async Task<(bool validacionEntry, bool filasModif)> UpdateEpilepsia(EpilepsiasDto epilepsia) {
            try {
                EpilepsiaModel? ep = await _context.Epilepsias.FindAsync(epilepsia.IdEpilepsia);
                bool validacionEntry = false, filasModif = false;
                if (ep != null) {

                    // Asignamos nuevo nombre
                    ep.Nombre = epilepsia.Nombre;

                    // Verificamos que el objeto haya sido modicado
                    if (_context.Entry(ep).State == EntityState.Modified) {
                        validacionEntry = true;
                        filasModif = await _context.SaveChangesAsync() > 0;
                    }

                }
                return (validacionEntry, filasModif);
            } catch (Exception) {
                throw;
            }
        }
    }
}
