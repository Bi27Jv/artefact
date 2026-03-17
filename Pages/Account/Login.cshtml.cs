using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel.DataAnnotations; // for [Required] attribute
using System.Security.Claims; // for Claims to log user into cookies
using artefact.Data;

namespace artefact.Pages.Account
{
    public class loginModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
