using KMCM_PruebaTecnica.kmcm_accessData;
using KMCM_PruebaTecnica.kmcm_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KMCM_PruebaTecnica.Controllers
{
	[Route("kmcm_api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly kmcm_repositoryUser _repository;

		public UserController(kmcm_repositoryUser repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// Obtener todos los usuarios.
		/// </summary>
		/// <returns>Una lista de usuarios.</returns>
		[HttpGet]
		[Authorize]
		public async Task<ActionResult<IEnumerable<kmcm_user>>> GetAllUsers()
		{
			var users = await _repository.getAllUsersAsync();
			return Ok(users);
		}

		/// <summary>
		/// Obtener un usuario por su ID.
		/// </summary>
		/// <param name="id">El ID del usuario.</param>
		/// <returns>El usuario correspondiente.</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<kmcm_user>> GetUserById(int id)
		{
			var user = await _repository.getUserByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		/// <summary>
		/// Agregar un nuevo usuario.
		/// </summary>
		/// <param name="user">El objeto usuario a agregar.</param>
		/// <returns>El usuario agregado.</returns>
		[HttpPost]
		public async Task<ActionResult<kmcm_user>> AddUser(kmcm_user user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var addedUser = await _repository.addUserAsync(user);
			return CreatedAtAction(nameof(GetUserById), new { id = addedUser.kmcm_id }, addedUser);
		}

		/// <summary>
		/// Actualizar un usuario existente.
		/// </summary>
		/// <param name="id">El ID del usuario a actualizar.</param>
		/// <param name="user">Los datos actualizados del usuario.</param>
		/// <returns>Un valor booleano que indica si la actualización fue exitosa.</returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, kmcm_user user)
		{
			if (id != user.kmcm_id)
			{
				return BadRequest("El ID del usuario no coincide.");
			}

			var success = await _repository.updateUserAsync(user);
			if (!success)
			{
				return NotFound($"No se encontró un usuario con ID {id} para actualizar.");
			}
			return NoContent();
		}

		/// <summary>
		/// Eliminar un usuario por su ID.
		/// </summary>
		/// <param name="id">El ID del usuario a eliminar.</param>
		/// <returns>Un valor booleano que indica si la eliminación fue exitosa.</returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var success = await _repository.deleteUserAsync(id);
			if (!success)
			{
				return NotFound($"No se encontró un usuario con ID {id} para eliminar.");
			}
			return NoContent();
		}
	}
}
