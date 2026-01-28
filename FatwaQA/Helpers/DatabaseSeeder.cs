using FatwaQA.Data;
using FatwaQA.Models;
using System.Security.Cryptography;
using System.Text;

namespace FatwaQA.Helpers
{
    public static class DatabaseSeeder
    {
        public static void Seed(FatwaContext context)
        {
            // Only seed if no users exist
            if (context.Users.Any())
                return;

            var adminUser = new User
            {
                Username = "admin",
                Email = "admin@fatwa.local",
                FullName = "Admin User",
                Role = "Admin",
                PasswordHash = HashPassword("Admin@123"),
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            context.Users.Add(adminUser);
            context.SaveChanges();
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
