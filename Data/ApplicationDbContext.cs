using Microsoft.EntityFrameworkCore;
using artefact.Models;

namespace artefact.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Define DbSet for models below:
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Context below prevents duplicate email adresses in the Users table:
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Context below creates a one-to-many relationship, where one user can have many projects, and a project belongs to one user.
            modelBuilder.Entity<Project>()
                .HasOne(P => P.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Context below creates a one-to-many relationship, where one project can have many contents, and content belongs to one project.
            modelBuilder.Entity<ProjectContent>()
                .HasOne(u => u.Project)
                .WithMany(p => p.Contents)
                .HasForeignKey(U => U.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
