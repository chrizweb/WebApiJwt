using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiJwt.Models;

namespace WebApiJwt.Custom {
	public class Utilidades {
		private readonly IConfiguration config;
		public Utilidades(IConfiguration configuration) {
			config = configuration;
		}
		/******************************************************************/
		public string encryptSha256(string texto) {

			using (SHA256 sha256Hash = SHA256.Create()) {

				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++) {
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}
		/******************************************************************/
		public string generateJwt(Usuario user) {
			/*Create information user*/
			var userClaims = new[] {
				new Claim(ClaimTypes.NameIdentifier,user.IdUsuario.ToString()),
				new Claim(ClaimTypes.Email,user.Correo!),
			};

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:KEY"]!));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

			/*Create details token*/
			var jwtConfig = new JwtSecurityToken(
				claims:userClaims,
				expires:DateTime.UtcNow.AddMinutes(10),
				signingCredentials:credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
		}

	}
}
