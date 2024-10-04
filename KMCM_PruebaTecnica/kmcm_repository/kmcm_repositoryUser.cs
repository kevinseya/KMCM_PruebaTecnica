using KMCM_PruebaTecnica.kmcm_models; 
using KMCM_PruebaTecnica.kmcm_models.kmcm_DbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KMCM_PruebaTecnica.kmcm_accessData
{
	public class kmcm_repositoryUser
	{
		private readonly Kmcm_DbContext _context; 

		public kmcm_repositoryUser(Kmcm_DbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Obtener todos los usuarios.
		/// </summary>
		/// <returns>Lista de usuarios.</returns>
		public async Task<List<kmcm_user>> getAllUsersAsync()
		{
			return await _context.Users.Include(u => u.Kmcm_person).ToListAsync();
		}

		/// <summary>
		/// Obtener un usuario por su ID.
		/// </summary>
		/// <param name="id">El ID del usuario.</param>
		/// <returns>El usuario correspondiente.</returns>
		public async Task<kmcm_user> getUserByIdAsync(int id)
		{
			return await _context.Users.FindAsync(id); 
		}

		/// <summary>
		/// Obtener un usuario por su ID de persona FK.
		/// </summary>
		/// <param name="personId">El ID de la persona del usuario.</param>
		/// <returns>El usuario correspondiente.</returns>
		public async Task<kmcm_user> getUserByPersonIdAsync(int personId)
		{
			return await _context.Users
				.FirstOrDefaultAsync(user => user.kmcm_person_id == personId);
		}

		/// <summary>
		/// Agregar un nuevo usuario.
		/// </summary>
		/// <param name="user">El objeto usuario a agregar.</param>
		/// <returns>El usuario agregado.</returns>
		public async Task<kmcm_user> addUserAsync(kmcm_user user)
		{
			_context.Users.Add(user); 
			await _context.SaveChangesAsync();
			return user; 
		}

		/// <summary>
		/// Actualizar un usuario existente.
		/// </summary>
		/// <param name="user">El usuario actualizado.</param>
		/// <returns>Un valor booleano que indica si la actualización fue exitosa.</returns>
		public async Task<bool> updateUserAsync(kmcm_user user)
		{
			_context.Entry(user).State = EntityState.Modified; 
			var result = await _context.SaveChangesAsync();
			return result > 0; 
		}

		/// <summary>
		/// Eliminar un usuario por su ID.
		/// </summary>
		/// <param name="id">El ID del usuario a eliminar.</param>
		/// <returns>Un valor booleano que indica si la eliminación fue exitosa.</returns>
		public async Task<bool> deleteUserAsync(int id)
		{
			var user = await getUserByIdAsync(id);
			if (user == null)
			{
				return false; 
			}

			_context.Users.Remove(user); 
			var result = await _context.SaveChangesAsync();
			return result > 0; 
		}
	}
}
