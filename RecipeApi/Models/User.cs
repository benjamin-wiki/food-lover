using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RecipeApi.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Preference = string.Empty;
            Recipes = new List<Recipe>();
        }

        public string? Preference { get; set; }

        public ICollection<Recipe> Recipes { get; set; }  // Changed from List<Recipe> to ICollection<Recipe>

        // A method to set (and hash) the password
        public void SetPassword(string password)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            this.PasswordHash = hasher.HashPassword(this, password);
        }

        public bool CheckPassword(string passwordToCheck)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(this, this.PasswordHash, passwordToCheck);
            return result == PasswordVerificationResult.Success;
        }
    }
}
