using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiJwt.Custom;
using WebApiJwt.Models;
using WebApiJwt.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebApiJwt.Controllers {
	[Route("api/[controller]")]
	[AllowAnonymous]
	[ApiController]
	public class AccesoController : ControllerBase {

		private readonly DbJwtContext _dbJwtContext;
		private readonly Utilidades _utilidades;
		public AccesoController(DbJwtContext dbJwtContext, Utilidades utilidades) {
			_dbJwtContext = dbJwtContext;
			_utilidades = utilidades;
		}
		/*******************************************************************/
		[HttpPost]
		[Route("Registrarse")]
		public async Task<IActionResult> Register(UsuarioDTO userDto) {
			var user_model = new Usuario {
				Nombre = userDto.Nombre,
				Correo = userDto.Correo,
				Clave = _utilidades.encryptSha256(userDto.Clave)
			};

			await _dbJwtContext.Usuarios.AddAsync(user_model);
			await _dbJwtContext.SaveChangesAsync();

			if (user_model.IdUsuario != 0)
				return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
			else
				return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
		}
		/*******************************************************************/
		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login(LoginDTO loginDto) {
			var userFound = await _dbJwtContext.Usuarios
				.Where(u =>
					u.Correo == loginDto.Correo &&
					u.Clave == _utilidades.encryptSha256(loginDto.Clave)
				).FirstOrDefaultAsync();

			if (userFound == null)
				return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
			else 
				return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generateJwt(userFound) });
		}
		/*******************************************************************/
	}
}
