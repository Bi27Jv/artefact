using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations; // for [Required] attribute
using artefact.Data;
using artefact.Models;

namespace artefact.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty, Required]
        public string Username { get; set; } = string.Empty;

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Check if email address is already registered in the database
            if (_db.Users.Any(u => u.Email == Email))
            {
                ModelState.AddModelError(string.Empty, "Email is already registered.");
                return Page();
            }

            // add new user to the database
            var user = new User
            {
                Name = Username,
                Email = Email,
                PasswordEncrypted = BCrypt.Net.BCrypt.HashPassword(Password)
            };

           _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Account/Login", new { message = "registered" });
        }
    }
}