namespace artefact.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordEncrypted { get; set; }

        // Navigation property to the user's related projects
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
