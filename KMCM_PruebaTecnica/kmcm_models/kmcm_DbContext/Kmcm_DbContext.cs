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
		public virtual DbSet<kmcm_user> Users { get; set; }  

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Kmcm_person>(entity =>
			{
				entity.HasKey(e => e.kmcm_id).HasName("PK__Persons__ID_PERSON");

				entity.ToTable("KMCM_Persons");

				entity.Property(e => e.kmcm_id).HasColumnName("KMCM_ID_PERSON");
				entity.Property(e => e.kmcm_name)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("KMCM_NAME");
				entity.Property(e => e.kmcm_lastname)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("KMCM_LASTNAME");
				entity.Property(e => e.kmcm_address)
					.HasMaxLength(200)
					.IsUnicode(false)
					.HasColumnName("KMCM_ADDRESS");
				entity.Property(e => e.kmcm_phone)
					.HasMaxLength(15)
					.IsUnicode(false)
					.HasColumnName("KMCM_PHONE");
				entity.Property(e => e.kmcm_birthdate)
					.HasColumnType("date")
					.HasColumnName("KMCM_BIRTHDATE");
			});

			modelBuilder.Entity<kmcm_user>(entity =>
			{
				entity.HasKey(e => e.kmcm_id).HasName("PK__Users__ID_USER"); 

				entity.ToTable("KMCM_Users"); 

				entity.Property(e => e.kmcm_id).HasColumnName("KMCM_ID_USER");
				entity.Property(e => e.kmcm_username)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("KMCM_USERNAME");
				entity.Property(e => e.kmcm_password)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("KMCM_PASSWORD");

				// Configuración de la clave foránea
				entity.HasOne(d => d.Kmcm_person)
					.WithMany() 
					.HasForeignKey(d => d.kmcm_person_id)
					.OnDelete(DeleteBehavior.Cascade) 
					.HasConstraintName("FK_User_Person");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
