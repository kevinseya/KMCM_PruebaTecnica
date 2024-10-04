using KMCM_PruebaTecnica.kmcm_models;
using KMCM_PruebaTecnica.kmcm_models.kmcm_DbContext;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;

namespace KMCM_PruebaTecnica.kmcm_accessData
{
	public class kmcm_repositoryPerson
	{
		private readonly Kmcm_DbContext _context;
		private readonly Serilog.ILogger _logger;

		public kmcm_repositoryPerson(Kmcm_DbContext context)
		{
			_context = context;
			_logger = Log.ForContext<kmcm_repositoryPerson>(); 
		}

		/// <summary>
		/// Obtiene todas las personas de la base de datos.
		/// </summary>
		/// <returns>Una lista de objetos <see cref="Kmcm_person"/>.</returns>
		public async Task<IEnumerable<Kmcm_person>> getAllPersonsAsync()
		{
			try
			{
				return await _context.Persons.ToListAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al obtener todas las personas: {ex.Message}");
				return Enumerable.Empty<Kmcm_person>(); 
			}
		}

		/// <summary>
		/// Obtiene una persona específica por su ID.
		/// </summary>
		/// <param name="id">El ID de la persona a buscar.</param>
		/// <returns>El objeto <see cref="Kmcm_person"/> correspondiente o null si no se encuentra.</returns>
		public async Task<Kmcm_person> getPersonByIdAsync(int id)
		{
			try
			{
				return await _context.Persons.FirstOrDefaultAsync(p => p.kmcm_id == id);
			}
			catch (Exception ex)
			{
				_logger.Error($"Error al obtener la persona con ID {id}: {ex.Message}");
				return null; 
			}
		}

		/// <summary>
		/// Agrega una nueva persona a la base de datos.
		/// </summary>
		/// <param name="person">El objeto <see cref="Kmcm_person"/> que representa a la persona a agregar.</param>
		/// <returns>El objeto <see cref="Kmcm_person"/> agregado.</returns>
		public async Task<Kmcm_person> addPersonAsync(Kmcm_person person)
		{
			try
			{
				await _context.Persons.AddAsync(person);
				await _context.SaveChangesAsync();
				return person;
			}
			catch (Exception ex)
			{
				_logger.Error($"Error al agregar la persona: {ex.Message}");
				return null; 
			}
		}

		/// <summary>
		/// Actualiza los datos de una persona existente en la base de datos.
		/// </summary>
		/// <param name="person">El objeto <see cref="Kmcm_person"/> con los datos actualizados.</param>
		/// <returns>Un valor booleano indicando si la actualización fue exitosa.</returns>
		public async Task<bool> updatePersonAsync(Kmcm_person person)
		{
			try
			{
				var existingPerson = await _context.Persons.FindAsync(person.kmcm_id);

				if (existingPerson == null)
				{
					return false;
				}

				// Actualizar propiedades
				existingPerson.kmcm_name = person.kmcm_name;
				existingPerson.kmcm_lastname = person.kmcm_lastname;
				existingPerson.kmcm_address = person.kmcm_address;
				existingPerson.kmcm_phone = person.kmcm_phone;
				existingPerson.kmcm_birthdate = person.kmcm_birthdate;

				await _context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Error al actualizar la persona con ID {person.kmcm_id}: {ex.Message}");
				return false; 
			}
		}

		/// <summary>
		/// Elimina una persona de la base de datos a través de su ID.
		/// </summary>
		/// <param name="id">El ID de la persona a eliminar.</param>
		/// <returns>Un valor booleano que indica si la eliminación fue exitosa.</returns>
		public async Task<bool> deletePersonAsync(int id)
		{
			try
			{
				var person = await _context.Persons.FindAsync(id);

				if (person == null)
				{
					return false;
				}

				_context.Persons.Remove(person);
				await _context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Error al eliminar la persona con ID {id}: {ex.Message}");
				return false; 
			}
		}
	}
}
