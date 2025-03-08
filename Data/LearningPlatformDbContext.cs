using System;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;

namespace LearningPlatform.Data
{
    
    
    public class LearningPlatformDbContext : DbContext
    {
        public LearningPlatformDbContext(DbContextOptions<LearningPlatformDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // composite primary key for the UserCourse table
            modelBuilder.Entity<UserCourse>()
                .HasKey(uc => new { uc.UserId, uc.CourseId });

            // many-to-many relationships
            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCourses)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.Course)
                .WithMany(c => c.UserCourses)
                .HasForeignKey(uc => uc.CourseId);
        }
    }
}