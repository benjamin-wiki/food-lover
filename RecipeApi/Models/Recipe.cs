using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApi.Models
{
	public class Recipe
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Ingredients { get; set; }

		[Required]
		public string Instructions { get; set; }

		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }
	}
}
