using System;
using System.ComponentModel.DataAnnotations;

namespace KMCM_PruebaTecnica.kmcm_models
{
	public class Kmcm_person
	{
		[Key]
		public int kmcm_id { get; set; }

		[Required(ErrorMessage = "El nombre es obligatorio.")]
		[StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
		public string kmcm_name { get; set; }

		[Required(ErrorMessage = "El apellido es obligatorio.")]
		[StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
		public string kmcm_lastname { get; set; }

		[StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
		public string kmcm_address { get; set; }

		[Required(ErrorMessage = "El número de teléfono es obligatorio.")]
		[Phone(ErrorMessage = "El número de teléfono no es válido.")]
		[StringLength(15, ErrorMessage = "El teléfono no puede exceder los 10 caracteres.")]
		public string kmcm_phone { get; set; }

		[Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
		[DataType(DataType.Date)]
		public DateTime kmcm_birthdate { get; set; }
	}
}
