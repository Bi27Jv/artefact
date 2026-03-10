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

        // Context below prevents duplicate email adresses in the Users table:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
