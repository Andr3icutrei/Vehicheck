using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Entities
{
    public class User : BaseEntity
    {
        [MinLength(5), MaxLength(30)]
        public string FirstName { get; set; }

        [MinLength(5), MaxLength(30)]
        public string LastName { get; set; }

        public bool IsAdmin { get; set; }

        [MinLength(5), MaxLength(40)]
        public string Email { get; set; }

        [MinLength(5), MaxLength(20)]
        public string Phone { get; set; }

        // Remove the plain Password property and replace with:
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(255)]
        public string Salt { get; set; }

        // Navigation properties
        public ICollection<Car> Cars { get; set; } = new List<Car>();

        // Helper methods for password handling
        public void SetPassword(string password)
        {
            Salt = GenerateSalt();
            PasswordHash = HashPassword(password, Salt);
        }

        public bool VerifyPassword(string password)
        {
            var hash = HashPassword(password, Salt);
            return hash == PasswordHash;
        }

        private static string GenerateSalt()
        {
            var saltBytes = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            using (var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(
                password,
                Convert.FromBase64String(salt),
                100000, // iterations (adjust based on security needs)
                System.Security.Cryptography.HashAlgorithmName.SHA256))
            {
                var hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
