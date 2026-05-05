namespace artefact.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        // Foreign key to User, connects a project to its creator
        public int UserId { get; set; }
         public User User { get; set; }
    }
}
