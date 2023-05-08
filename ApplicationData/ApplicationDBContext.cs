using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataClass;

namespace WebAPI.ApplicationData
{
	public class ApplicationDBContext : IdentityDbContext<IdentityUser>
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-WebAPI;Trusted_Connection=True;MultipleActiveResultSets=true");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Recipes>().
				HasMany(x => x.Ingredienten).
				WithOne(x => x.Recipes).
				OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Categories>().HasData(
				new Categories { Id = 6, Name = "Soepen" },
				new Categories { Id = 2, Name = "Vegetarisch" },
				new Categories { Id = 3, Name = "Voorgerecht" },
				new Categories { Id = 4, Name = "Hoofdgerecht" },
				new Categories { Id = 5, Name = "Dessert" }
				);

			base.OnModelCreating(modelBuilder);
		}


		public DbSet<Categories> Categories { get; set; }
		public DbSet<Recipes> Recipes { get; set; }
		public DbSet<Ingredienten> Ingredientens { get; set; }


		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
			: base(options)
		{
		}
		public ApplicationDBContext()
			: base()
		{
		}

	}
}
