using Microsoft.EntityFrameworkCore;
using RecipeApi.models;

namespace RecipeApi.Data
{
	public class RecipeContext : DbContext
	{
		public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Recipe> Recipes { get; set; }
	}
}
