﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.API.Controllers {
    [Route("/api/cuentas")]
    [ApiController]
    [Authorize(Roles = "superAdmin, admin")]
    public class CuentasController : ControllerBase {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAdminsService _adminService;
        private readonly IIdentityService _identityService;
        IdentityContext _identityContext;
        WebmedicinaContext _context;

        // Contructor con inyeccion de dependencias
        public CuentasController(IConfiguration configuration, IMapper mapper, IAdminsService adminService, IIdentityService identityService,
            IdentityContext identityContext, WebmedicinaContext context) {
            _configuration = configuration;
            _mapper = mapper;
            _adminService = adminService;
            _identityService = identityService;
            _identityContext = identityContext;
            _context = context;
        }

        [HttpPost("crear")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateUser([FromBody] UserRegistroDto model) {
            using (var transactionIdentity = _identityContext.Database.BeginTransaction()) {
                try {
                    if (ModelState.IsValid && model != null) {
                        var user = new IdentityUser {
                            UserName = model.UserLogin
                        };

                        // Creamos user con identity
                        if (await _identityService.CrearUser(user, model)) {

                            // Insertamos el nuevo medico a la tabla y generamos token si todo esta OK
                            if (_adminService.CrearMedico(model, user.Id)) {
                                await transactionIdentity.CommitAsync();
                                _context.SaveChanges();

                                // Devolvemos que la respuesta ha sido correcta
                                return Ok($"Nuevo {model.Rol} creado correctamente");
                            }
                            // Revertimos toda la transacción si el usuario no se ha creado correctamente
                            await transactionIdentity.RollbackAsync();
                            return BadRequest($"Ha surgido un error al crear el nuevo {model.Rol}");
                        } else {
                            return BadRequest($"Ya existe un usuario con el username: {model.UserLogin}, o el username no es válido.");
                        }

                    } else {
                        return BadRequest("Alguno de los campos no es valido");
                    }
                } catch (Exception) {
                    await transactionIdentity.RollbackAsync();
                    return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
                }
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserLoginDto userLogin) {
            try {
                if (ModelState.IsValid) {
                    if (await _identityService.ComprobarContraseña(userLogin)) {

                        // Obtenemos los datos del medico y su rol
                        MedicosModel? medico = await _identityService.ObtenerUsuarioYRolLogin(userLogin.UserName);

                        // Generamos la info del usuario si se ha obtenido correctamente
                        if(medico is not null) {
                            UserInfoDto userInfo = _mapper.Map<UserInfoDto>(medico);
                            return Ok(BuildToken(userInfo));
                        }
                        return BadRequest("Error al obtener información del usuario");
                    } else {
                        return BadRequest("Credenciales incorrectas");
                    }
                } else {
                    return BadRequest("Usuario o contraseña no válidos");
                }
            } catch (Exception ex) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        // Generamos y comprobamos si el userName está disponible 
        [HttpPost("generarUserName")]
        public async Task<IActionResult> GenerarUserName([FromBody] string[] nomYApell) {
            try { 
                var respuesta = await _identityService.GenerarUserName(nomYApell);
                if (respuesta.userNameInvalido == false) {
                    return Ok(respuesta.userNameGenerado);
                } else {
                    return BadRequest();
                }
               } catch (Exception ex) {
               return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }


        private UserToken BuildToken(UserInfoDto userInfo) {
            try { 
                var claims = new [] {
                    new Claim(ClaimTypes.NameIdentifier, userInfo.IdMedico.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //IDENTIFICADOR
                    new Claim("UserLogin", userInfo.UserLogin),
                    new Claim(ClaimTypes.Name, userInfo.Nombre),
                    new Claim(ClaimTypes.Surname, userInfo.Apellidos),
                    new Claim(ClaimTypes.Role, userInfo.Rol),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora.
                var expiration = DateTime.UtcNow.AddDays(7);

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: creds);

                return new UserToken() {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration
                };
            } catch (Exception) {
                throw;
            }
        }
    }
    }
