using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMCM_PruebaTecnica.kmcm_models
{
	public class kmcm_user
	{
		[Key]
		public int kmcm_id { get; set; }

		[Required(ErrorMessage = "El usuario es obligatorio.")]
		[StringLength(50, ErrorMessage = "El usuario no puede exceder los 50 caracteres.")]
		public string kmcm_username { get; set; }

		[Required(ErrorMessage = "La contraseña es obligatoria.")]
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.", MinimumLength = 8)]
		[RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*()_+<>?])(?=.*[A-Z]).+$",
			ErrorMessage = "La contraseña debe tener al menos un número, un símbolo y una mayúscula.")]
		public string kmcm_password { get; set; }

		// Clave foránea que se relaciona con el modelo Kmcm_person
		[ForeignKey("Kmcm_person")]
		public int kmcm_person_id { get; set; }
		public virtual Kmcm_person Kmcm_person { get; set; }
	}
}
