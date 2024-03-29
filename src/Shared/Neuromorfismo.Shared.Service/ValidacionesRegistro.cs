﻿using System.ComponentModel.DataAnnotations;

namespace Neuromorfismo.Shared.Service {
    public static class ValidacionesRegistro {
        public const string PatronNombres = @"^[A-ZÁÉÍÓÚÜÑ][a-záéíóúüñ]+( [A-ZÁÉÍÓÚÜÑ][a-záéíóúüñ]+){0,2}$";
        public const string PatronApellidos = @"^[A-ZÁÉÍÓÚÜÑ][a-záéíóúüñ]+( [A-ZÁÉÍÓÚÜÑ][a-záéíóúüñ]+){1,2}$";
        public const string PatronPassword = @"^(?=.*\d)(?=.*[!@#$%^&*()_+])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$";


         // Valida que la persona tenga al menos 18 años
        public static ValidationResult? ValidateFechaNacimiento(DateTime fechaNacimiento) {
            try {
                if (fechaNacimiento > DateTime.Now.AddYears(-18)) {
                    return new ValidationResult("El usuario debe tener al menos 18 años de edad.");
                }

                return ValidationResult.Success;
            } catch (Exception) {
                throw;
            }
        }  
        
        public static ValidationResult? ValidateFechaNacPaciente(DateTime? fechaNacimientoNull) {
            try {

                if(fechaNacimientoNull is not null) {
                    DateTime fechaNacimiento = fechaNacimientoNull ?? DateTime.MinValue;
                    if (DateTime.Compare(fechaNacimiento, DateTime.Now) > 0) {
                        return new ValidationResult("Fecha de nacimiento no válida.");
                    }
                }

                return ValidationResult.Success;
            } catch (Exception) {
                throw;
            }
        }

        public static DateTime ObtenerFechaMaxNacimiento () {
            return DateTime.Now.AddYears(-18);
        }
    }
}
