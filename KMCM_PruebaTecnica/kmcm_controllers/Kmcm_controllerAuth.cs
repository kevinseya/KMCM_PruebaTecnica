using KMCM_PruebaTecnica.kmcm_accessData; 
using KMCM_PruebaTecnica.kmcm_models;
using KMCM_PruebaTecnica.kmcm_util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KMCM_PruebaTecnica.Controllers
{
	[Route("kmcm_api/[controller]")]
	[ApiController]
	public class Kmcm_controllerAuth : ControllerBase
	{
		private readonly kmcm_repositoryUser _userRepository;
		private readonly kmcm_encript _encript;
		private readonly string _key; 
		private readonly string _issuer; 
		private readonly string _audience;

		public Kmcm_controllerAuth(kmcm_repositoryUser userRepository, kmcm_encript encript,IConfiguration configuration)
		{
			_userRepository = userRepository;
			_encript = encript;
			_key = configuration.GetValue<string>("Jwt:Key");
			_issuer = configuration.GetValue<string>("Jwt:Issuer");
			_audience = configuration.GetValue<string>("Jwt:Audience");
		}

		/// <summary>
		/// Autenticar al usuario y generar un token JWT.
		/// </summary>
		/// <param name="loginModel">Modelo con las credenciales de inicio de sesión.</param>
		/// <returns>Un token JWT si las credenciales son válidas.</returns>
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var users = await _userRepository.getAllUsersAsync();
			var existingUser = users.FirstOrDefault(u => u.kmcm_username == loginModel.Username);

			if (existingUser == null)
			{
				return Unauthorized();
			}

			// Desencriptar la contraseña almacenada
			var decryptedPassword = _encript.Decrypt(existingUser.kmcm_password);

			// Comparar las contraseñas
			if (decryptedPassword != loginModel.Password)
			{
				return Unauthorized();
			}

			// Crear los claims del token
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, existingUser.kmcm_id.ToString()),
				new Claim(ClaimTypes.Name, existingUser.kmcm_username)
			};

			// Crear el token JWT
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_key);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(1),
				Issuer = _issuer,
				Audience = _audience,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var name = existingUser.Kmcm_person?.kmcm_name;
			var lastname = existingUser.Kmcm_person?.kmcm_lastname;

			return Ok(new
			{
				Token = tokenHandler.WriteToken(token),
				Name = name,
				Lastname = lastname
			});
		}
	}

	public class LoginModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}

