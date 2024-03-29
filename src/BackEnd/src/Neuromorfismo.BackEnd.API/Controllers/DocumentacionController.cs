﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Neuromorfismo.BackEnd.ServicesDependencies;
using Neuromorfismo.BackEnd.ServicesDependencies.Mappers;
using Neuromorfismo.Shared.Dto.LineaTemporal;
using Neuromorfismo.Shared.Dto.Pacientes;

namespace Neuromorfismo.BackEnd.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentacionController : ControllerBase {
        private readonly IDocumentacionService _documentacionService;
        private readonly IEmailService _emailService;

        public DocumentacionController(IDocumentacionService documentacionService, IEmailService emailService) {
            _documentacionService = documentacionService;
            _emailService = emailService;
        }


        [HttpPost("cargarexcel")]
        public ActionResult CargarExcel([FromBody] ExcelPacientesDto excelPacientes) {
            try {

                // Validamos que la lista contenga elementos
                if (excelPacientes.Pacientes.IsEmpty) {   
                    return BadRequest();
                } else {
                    List<ValidationResult> validationResults = new();
                    bool validacionOK = true;
                    List<PacienteExcelDto> pacientesExcel = new();
 
                    // Mapeamos lista de pacientes y validamos que cumplan validaciones
                    foreach (var pac in excelPacientes.Pacientes) {

                        validacionOK = Validator.TryValidateObject(pac, new(pac), validationResults);
                        if (validacionOK) {
                            pacientesExcel.Add(new PacienteExcelDto(pac));
                        } else {
                            // Si el modelo no es valido devolvemos el resultado de la validacion
                            return BadRequest(validationResults);
                        }
                    }

                    // Obtenemos el nombre de la pagina
                    if (string.IsNullOrWhiteSpace(excelPacientes.NombrePaginaExcel)) {
                        excelPacientes.NombrePaginaExcel = "Pacientes";
                    }

                    // Generamos excel con la lista de pacientes
                    MemoryStream memoryStream = _documentacionService.GenerarExcelPacientes(pacientesExcel, excelPacientes.NombrePaginaExcel);

                    // Creamos el excel
                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Pacientes-{DateTime.Today.ToFileTime()}.xlsx");
                }
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPost("enviaremail")]
        public ActionResult EnviarEmail([FromBody] EmailEditarEvoDto datosEmail) {
            // Validamos que el base64 sea de una imagen
            string[] headerYBase64 = datosEmail.ImgBase64.Split(",");

            if (headerYBase64.Any() && headerYBase64[0].Contains("image/png", StringComparison.OrdinalIgnoreCase)) {
                // Obtenemos el asunto y el cuerpo del correo 
                var(asunto, cuerpo) = _documentacionService.GenerarCorreo(datosEmail, User.ToUserInfoDto());

                // Realizamos envio de email sin esperar una respuesta
                if(!string.IsNullOrWhiteSpace(asunto) && !string.IsNullOrWhiteSpace(cuerpo)) {
                    _emailService.Send(asunto, cuerpo, new MemoryStream(Convert.FromBase64String(headerYBase64[1])));
                }
            }

            return NoContent();
        }
    }
}
