using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiJwt.Custom;
using WebApiJwt.Models;
using WebApiJwt.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApiJwt.Controllers {
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiController]
	public class ProductoController : ControllerBase {

		private readonly DbJwtContext _dbJwtContext;
		public ProductoController(DbJwtContext dbJwtContext) {
			_dbJwtContext = dbJwtContext;
		}

		[HttpGet]
		[Route("Lista")]
		public async Task<IActionResult> List() {
			var list = await _dbJwtContext.Productos.ToListAsync();
			return StatusCode(StatusCodes.Status200OK, new { value = list });
		}
	}
}
