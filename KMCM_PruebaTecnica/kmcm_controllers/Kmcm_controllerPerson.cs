using KMCM_PruebaTecnica.kmcm_accessData;
using KMCM_PruebaTecnica.kmcm_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KMCM_PruebaTecnica.Controllers
{
	[Route("kmcm_api/[controller]")]
	[ApiController]
	public class PersonController : ControllerBase
	{
		private readonly kmcm_repositoryPerson _repository;

		public PersonController(kmcm_repositoryPerson repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// Para obtener todas las personas.
		/// </summary>
		/// <returns>Una lista de personas.</returns>
		[HttpGet]
		[Authorize]
		public async Task<ActionResult<IEnumerable<Kmcm_person>>> GetAllPersons()
		{
			var persons = await _repository.getAllPersonsAsync();
			return Ok(persons);
		}

		/// <summary>
		/// Para obtener una persona por su ID.
		/// </summary>
		/// <param name="id">El ID de la persona.</param>
		/// <returns>La persona correspondiente al ID proporcionado.</returns>
		[HttpGet("{id}")]
		[Authorize]
		public async Task<ActionResult<Kmcm_person>> GetPersonById(int id)
		{
			var person = await _repository.getPersonByIdAsync(id);
			if (person == null)
			{
				return NotFound($"No se encontró una persona con ID {id}.");
			}
			return Ok(person);
		}

		/// <summary>
		/// Para agregar una nueva persona.
		/// </summary>
		/// <param name="person">El objeto persona a agregar.</param>
		/// <returns>La persona agregada.</returns>
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Kmcm_person>> AddPerson(Kmcm_person person)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var addedPerson = await _repository.addPersonAsync(person);
			if (addedPerson == null)
			{
				return BadRequest("No se pudo agregar la persona.");
			}
			return CreatedAtAction(nameof(GetPersonById), new { id = addedPerson.kmcm_id }, addedPerson);
		}

		/// <summary>
		/// Para actualizar los datos de una persona existente.
		/// </summary>
		/// <param name="id">El ID de la persona a actualizar.</param>
		/// <param name="person">Los datos actualizados de la persona.</param>
		/// <returns>Un valor booleano que indica si la actualización fue exitosa.</returns>
		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> UpdatePerson(int id, Kmcm_person person)
		{
			if (id != person.kmcm_id)
			{
				return BadRequest("El ID de la persona no coincide.");
			}

			var success = await _repository.updatePersonAsync(person);
			if (!success)
			{
				return NotFound($"No se encontró una persona con ID {id} para actualizar.");
			}
			return NoContent();
		}

		/// <summary>
		/// Para eliminar una persona por su ID.
		/// </summary>
		/// <param name="id">El ID de la persona a eliminar.</param>
		/// <returns>Un valor booleano que indica si la eliminación fue exitosa.</returns>
		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeletePerson(int id)
		{
			var success = await _repository.deletePersonAsync(id);
			if (!success)
			{
				return NotFound($"No se encontró una persona con ID {id} para eliminar.");
			}
			return NoContent();
		}
	}
}
