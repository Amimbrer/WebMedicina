﻿
using System.Text.Json;

namespace WebMedicina.Shared.Service {
    public static class Clonar {

        // Crear una copia de ub objeto con propiedades con referencia
        public static T ClonarManual<T>(this T copia) {
            try {
                // Convertimos a json 
                string json = JsonSerializer.Serialize(copia);

                // desconvertimos y creamos el nuevo objeto
                return JsonSerializer.Deserialize<T>(json) ?? throw new ArgumentNullException("Error al clonar el objeto.");
            } catch (Exception) {
                throw;
            }
        }
    }
}
