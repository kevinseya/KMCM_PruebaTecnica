using Microsoft.EntityFrameworkCore;
using KMCM_PruebaTecnica.kmcm_models;

namespace KMCM_PruebaTecnica.kmcm_model.kmcm_DbContext
{
	public partial class Kmcm_DbContext : DbContext
	{
		public Kmcm_DbContext(DbContextOptions<Kmcm_DbContext> options)
		: base(options)
		{ 
		}

		public virtual DbSet<Kmcm_person> Persons { get; set; }

	}

}
