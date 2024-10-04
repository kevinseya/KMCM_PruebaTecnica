using KMCM_PruebaTecnica.kmcm_accessData; // Asegúrate de que la ruta es correcta
using KMCM_PruebaTecnica.kmcm_models;
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
		private readonly string _key; 
		private readonly string _issuer; 
		private readonly string _audience;

		public Kmcm_controllerAuth(kmcm_repositoryUser userRepository, IConfiguration configuration)
		{
			_userRepository = userRepository;
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

			
			var user = await _userRepository.getAllUsersAsync(); 
			var existingUser = user.FirstOrDefault(u => u.kmcm_username == loginModel.Username);

			if (existingUser == null || existingUser.kmcm_password != loginModel.Password) 
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
			return Ok(new { Token = tokenHandler.WriteToken(token) });
		}
	}

	public class LoginModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}

