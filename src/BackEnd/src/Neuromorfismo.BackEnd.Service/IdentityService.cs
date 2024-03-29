﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Neuromorfismo.BackEnd.Dal;
using Neuromorfismo.BackEnd.Model;
using Neuromorfismo.BackEnd.ServicesDependencies;
using Neuromorfismo.BackEnd.ServicesDependencies.ExtensionMethods;
using Neuromorfismo.Shared.Dto.UserAccount;
using Neuromorfismo.Shared.Dto.Usuarios;

namespace Neuromorfismo.BackEnd.Service
{
    public class IdentityService : IIdentityService {
        private readonly MedicoDal _medicoDal;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly AdminDal _adminDal;

        public IdentityService(MedicoDal medicoDal, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, AdminDal adminDal) {
            _medicoDal = medicoDal;
            _userManager = userManager;
            _signInManager = signInManager;
            _adminDal = adminDal;
        }

        /// <summary>
        /// Obtener info del usuario en el login
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<MedicosModel?> ObtenerUsuarioYRolLogin(string userName) {
            MedicosModel? medicosModel = _medicoDal.ObtenerInfoUserLogin(userName);

            // Obtenemos el rol si se ha obtenido correctamente la info del usuario
            if (medicosModel != null && !string.IsNullOrEmpty(medicosModel.UserLogin)) {
                var rol = await ObtenerRol(medicosModel.UserLogin);

                if (string.IsNullOrEmpty(rol)) {
                    medicosModel.Rol = "medico";
                } else {
                    medicosModel.Rol = rol;
                }
            }

            return medicosModel;
        }

        /// <summary>
        /// Crear nuevo usuario solo administradores
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> CrearUser(UserModel user, UserRegistroDto model) {
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) {
                // Añadimos el usuario a su rol
                result = await _userManager.AddToRoleAsync(user, model.Rol);
                return result.Succeeded;
            }
            return false;
        }

        /// <summary>
        /// Autenticar contraseña login usuario
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public async Task<bool> ComprobarContraseña(UserLoginDto userLogin) {
            if (string.IsNullOrWhiteSpace(userLogin.UserName) || string.IsNullOrWhiteSpace(userLogin.Password)) {
                return false;
            }

            var respuesta = await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, isPersistent: false, lockoutOnFailure: true);
            return respuesta.Succeeded;
        }

        /// <summary>
        /// Obtener Rol del usuario
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private async Task<string?> ObtenerRol(string userName) {
            return await _adminDal.ObtenerRolUser(await _adminDal.ObtenerUsuarioIdentity(userName));
        }

        /// <summary>
        /// LLamamos a BBDD y devolvemos si existe o no el userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> ComprobarUserName(string userName) {
            return await _userManager.FindByNameAsync(userName) is not null;
        }

        /// <summary>
        /// Genereamos un username valido para un nuevo medico
        /// </summary>
        /// <param name="nomYApell"></param>
        /// <returns>UserName Invalido y UserName generado</returns>
        public async Task<(bool userNameInvalido, string userNameGenerado)> GenerarUserName(string[] nomYApell) {
            bool userNameInValido = true;
            string userNameGenerado = string.Empty;

            // Comprobamos que nombre y apellidos sean validos
            if (nomYApell.Length > 1 && ValidarNomYApellUser(nomYApell[0], nomYApell[1])) {
                int indice = 0;
                int indiceApellido2 = 0;
                int indiceApellido3 = 0; // Apellido 3 en caso de que tuviera
                string nombre = nomYApell[0].ToLower().Replace(" ", "").SinTildes();
                string[] apellidos = nomYApell[1].ToLower().SinTildes().Split(" ");

                // Creamos cronometro para contar tiempo maximo
                Stopwatch stopwatch = new();
                stopwatch.Start();

                // Recorremos hasta conseguir un userName valido para el usuario, al minuto salta excepcion
                do {
                    userNameGenerado = string.Empty;

                    // Obtenemos primera letra del nombre
                    userNameGenerado += nombre[0];

                    // Obtenemos minimo 7 letras del primer apellido
                    userNameGenerado += apellidos[0][..(indice + 7 <= apellidos[0].Length ? indice + 7 : apellidos[0].Length)];

                    // Si se superan los caracteres del primer apellido comenzamos a coger del segundo
                    if (apellidos.Length > 0 && indice > apellidos[0].Length) {
                        userNameGenerado += apellidos[1][..(indiceApellido2 <= apellidos[1].Length ? indiceApellido2 : apellidos[1].Length)] ;

                        // Si se ha superado el segundo apellido y tiene 3 mapeamos tambien el tercero
                        if (apellidos.Length > 1 && indice > apellidos[1].Length) {
                            userNameGenerado += apellidos[2][..(indiceApellido3 <= apellidos[2].Length ? indiceApellido3 : apellidos[2].Length)];
                        }
                    }

                    // Validamos el username generado
                    userNameInValido = await ComprobarUserName(userNameGenerado);
                    indice++;
                } while (userNameInValido || stopwatch.Elapsed.TotalSeconds > 30) ;

                // Paramos el cronometro
                stopwatch.Stop();
            }

            return (userNameInValido, userNameGenerado);
        }

        /// <summary>
        /// Actualizamos rol del usuario
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="nuevoRol"></param>
        /// <returns></returns>
        public async Task<bool> ActualizarRol(string userLogin, string nuevoRol) {
            // Obtenemos el usuario y sus roles
            UserModel? usuario = await _adminDal.ObtenerUsuarioIdentity(userLogin);
            IdentityResult rolActualizado = new();

            if (usuario is not null) {
                string? rol = await _adminDal.ObtenerRolUser(usuario);
                // Eliminamos y volvemos a añadir el nuevo rol
                if(!string.IsNullOrWhiteSpace(rol)) {
                    Task<IdentityResult[]> tareasCambiarRol = Task.WhenAll(new Task<IdentityResult>[]{
                         _userManager.RemoveFromRoleAsync(usuario, rol),
                         _userManager.AddToRoleAsync(usuario, nuevoRol)
                    });

                    // Validamos si ambas tareas han devuelto un success result
                    IdentityResult[] rolesActualizados = await tareasCambiarRol;
                    rolActualizado = (rolesActualizados.All(q => q.Succeeded) ? IdentityResult.Success : IdentityResult.Failed());
                }
            }

            return rolActualizado.Succeeded;
        }

        /// <summary>
        /// Validamos si el nombre y apellidos del nuevo usuario son validos 
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellidos"></param>
        /// <returns></returns>
        public bool ValidarNomYApellUser(string nombre, string apellidos) {
            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellidos)) {
                UserRegistroDto usuarioRegistro = new() {
                    Nombre = nombre,
                    Apellidos = apellidos
                };

                // Validamos que el campo del Numero Historia cumpla las validaciones del dto
                var validationErrors = new List<ValidationResult>();
                bool nombreValido = Validator.TryValidateProperty(nombre, new ValidationContext(usuarioRegistro) { MemberName = nameof(usuarioRegistro.Nombre) }, validationErrors);
                bool apellidosValidos = Validator.TryValidateProperty(apellidos, new ValidationContext(usuarioRegistro) { MemberName = nameof(usuarioRegistro.Apellidos) }, validationErrors);

                return nombreValido && apellidosValidos;
            }

            return false;
        }


        /// <summary>
        /// Realiza el reestablecimiento de la contraseña por parte de un administrador
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> RestablecerPassword(RestartPasswordDto resetPass) {
            bool resetSuccess = false;

            // Validamos los datos del user
            if (resetPass.Medico is not null && !string.IsNullOrWhiteSpace(resetPass.Medico.UserLogin)) {

                // Obtenemos el usuario
                UserModel? user = await _userManager.FindByNameAsync(resetPass.Medico.UserLogin);

                if(user is not null) {

                    // Generamos token para restablecer la contraseña
                    string token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Reseteamos la contraseña
                    IdentityResult result = await _userManager.ResetPasswordAsync(user, token, resetPass.Password);
                    resetSuccess = result.Succeeded;
                }
            }

            return resetSuccess;
        }
    }
}
