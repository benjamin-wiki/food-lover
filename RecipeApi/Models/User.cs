using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RecipeApi.Models
{
	public class User
	{
		public int Id { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		// todo: Hash, never store passwords in plain text
		public string Password { get; set; }

		public string Preference { get; set; }

		public List<Recipe> Recipes { get; set; }
	}
}
