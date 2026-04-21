using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel.DataAnnotations; // for [Required] attribute
using System.Security.Claims; // for Claims to log user into cookies
using artefact.Data;

namespace artefact.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public LoginModel(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Check if user exists in the database with the provided email
            var user = _db.Users.FirstOrDefault(u => u.Email == Email);

            // Compare password against the encrypted password in the database
            if (user == null || !BCrypt.Net.BCrypt.Verify(Password, user.PasswordEncrypted))
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return Page();
            }

            // store user info in cookies for authentication
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
                );

            return RedirectToPage("/Index");
        }
    }
}
