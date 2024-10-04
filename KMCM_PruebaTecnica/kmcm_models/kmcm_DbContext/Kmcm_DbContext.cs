using Microsoft.EntityFrameworkCore;
using KMCM_PruebaTecnica.kmcm_models;

namespace KMCM_PruebaTecnica.kmcm_models.kmcm_DbContext
{
	public partial class Kmcm_DbContext : DbContext
	{
		public Kmcm_DbContext(DbContextOptions<Kmcm_DbContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Kmcm_person> Persons { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Kmcm_person>(entity =>
			{
				entity.HasKey(e => e.kmcm_id).HasName("PK__Persons__ID_PERSON");

				entity.ToTable("Persons");

				entity.Property(e => e.kmcm_id).HasColumnName("ID_PERSON");
				entity.Property(e => e.kmcm_name)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("NAME");
				entity.Property(e => e.kmcm_lastname)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("LASTNAME");
				entity.Property(e => e.kmcm_address)
					.HasMaxLength(200)
					.IsUnicode(false)
					.HasColumnName("ADDRESS");
				entity.Property(e => e.kmcm_phone)
					.HasMaxLength(15)
					.IsUnicode(false)
					.HasColumnName("PHONE");
				entity.Property(e => e.kmcm_birthdate)
					.HasColumnType("date")
					.HasColumnName("BIRTHDATE");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
