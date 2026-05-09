using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using artefact.Data;
using artefact.Models;

namespace artefact.Pages.Projects
{
    [Authorize] // Makes sure that this page can only be accessed if the user is logged in
    public class ManageModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ManageModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Project Project { get; set; } = null!;

        // Context Below Holds the List of Project Contents to be Displayed on the Page
        public List<ProjectContent> Contents { get; set; } = new();

        [BindProperty]

        // Context Below Holds the New Content to be Added to the Project
        public string NewContent { get; set; } = string.Empty;

        // Runs when the Page is Accessed with a GET Request, Fetches the Project and its Contents from the Database
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var project = await _db.Projects
                .Include(p => p.Contents)
                .FirstOrDefaultAsync(p => p.Id == id);

            // Checks if the Project Exists and if it belongs to the Logged-in User, if not returns a 404 Not Found Response
            if (project == null || project.UserId != userId)
                return NotFound();
            
            Project = project;

            // Displays Contents of the Project in Descending Order of Creation Date
            Contents = project.Contents
                .OrderByDescending(u => u.CreatedDate)
                .ToList();

            return Page();
        }

        // Runs When the "Save Changes" Button is Pressed
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var existingProject = await _db.Projects.FindAsync(id);

            if (existingProject == null || existingProject.UserId != userId)
                return NotFound();

            existingProject.Name = Project.Name;
            existingProject.Description = Project.Description;

            await _db.SaveChangesAsync();
            return RedirectToPage("/Projects/Manage", new { id });
        }

        // Runs When the "Add Content" Button is Pressed
        public async Task<IActionResult> OnPostAddContentAsync(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var project = await _db.Projects.FindAsync(id);

            if (project == null || project.UserId != userId)
                return NotFound();

            // Only Adds New Content if the NewContent Property is Not Empty or Whitespace
            if (!string.IsNullOrWhiteSpace(NewContent))
            {
                var content = new ProjectContent
                {
                    Content = NewContent,
                    CreatedDate = DateTime.UtcNow,
                    ProjectId = id
                };
                _db.ProjectContents.Add(content);
                await _db.SaveChangesAsync();
            }
            
            return RedirectToPage("/Projects/Manage", new { id });
        }

        // Runs When the "Delete Content" Button is Pressed
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var content = await _db.ProjectContents.FindAsync(id);

            if (content == null || content.Project.UserId != userId)
                return NotFound();

            _db.Projects.Remove(Project);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
