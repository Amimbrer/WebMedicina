﻿using Microsoft.EntityFrameworkCore;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
using WebMedicina.Shared.Dto.Tipos;

namespace WebMedicina.BackEnd.Dal {
    public class FarmacosDal {
        private readonly WebmedicinaContext _context;

        public FarmacosDal(WebmedicinaContext context) {
            _context = context;
        }
        //  Get FARMACOS
        public List<FarmacosDto> GetFarmacos() {
            try { 
                return _context.Farmacos.Select(q => q.ToDto()).ToList();
            } catch (Exception) { throw; }
        }

        //  Create FARMACOS
        public async Task<bool> CrearFarmaco(string nombre) {
            try {
                FarmacosModel nuevoFarmaco = new() {
                    Nombre = nombre
                };

                await _context.Farmacos.AddAsync(nuevoFarmaco);
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        //  Delete FARMACOS
        public async Task<bool> DeleteFarmaco(int idFarmaco) {
            try {
                FarmacosModel? farmacoBorrado = await _context.Farmacos.FindAsync(idFarmaco);
                if (farmacoBorrado != null) {
                    _context.Farmacos.Remove(farmacoBorrado);
                }
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        //  Update FARMACOS
        public async Task<(bool validacionEntry, bool filasModif)> UpdateFarnaco(FarmacosDto farmaco) {
            try {
                FarmacosModel? far = await _context.Farmacos.FindAsync(farmaco.IdFarmaco);
                bool validacionEntry = false, filasModif = false;
                if (far != null) {

                    // Asignamos nuevo nombre
                    far.Nombre = farmaco.Nombre;

                    // Verificamos que el objeto haya sido modicado
                    if (_context.Entry(far).State == EntityState.Modified) {
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
