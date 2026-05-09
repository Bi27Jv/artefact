namespace artefact.Models
{
    public class ProjectContent
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        // Foreign Key to Project, Connects Content to its Project
        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;
    }
}
