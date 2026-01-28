using Microsoft.EntityFrameworkCore;
using FatwaQA.Models;

namespace FatwaQA.Data
{
    public class FatwaContext : DbContext
    {
        public FatwaContext(DbContextOptions<FatwaContext> options)
            : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Question configuration
            modelBuilder.Entity<Question>()
                .HasOne(q => q.AnsweredBy)
                .WithMany()
                .HasForeignKey(q => q.AnsweredById)
                .OnDelete(DeleteBehavior.SetNull);

            // Create indexes for better query performance
            modelBuilder.Entity<Question>()
                .HasIndex(q => q.Status);

            modelBuilder.Entity<Question>()
                .HasIndex(q => q.IsPublished);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
