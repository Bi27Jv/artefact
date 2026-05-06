namespace artefact.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Foreign key to User, connects a project to its creator
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
