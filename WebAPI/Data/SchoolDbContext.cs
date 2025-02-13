using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Class> Class {  get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<LookupGender> LookupGender { get; set; }



        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Class Table Configuration
            modelBuilder.Entity<Class>()
            .HasKey(c => c.ClassId);
            modelBuilder.Entity<Class>()
            .HasMany(c => c.Students)
            .WithOne(s => s.Class)
            .HasForeignKey(s => s.ClassId);
            modelBuilder.Entity<Class>()
            .HasIndex(c => c.ClassName)
            .IsUnique();

            //Subject Table Configuration
            modelBuilder.Entity<Subject>()
            .HasKey(su => su.SubjectId);
            modelBuilder.Entity<Subject>()
            .HasIndex(su => su.SubjectName)
            .IsUnique();

            modelBuilder.Entity<LookupGender>()
            .HasMany(l => l.GenderStudents)
            .WithOne(s => s.Gender)
            .HasForeignKey(s => s.GenderId);
            modelBuilder.Entity<LookupGender>()
            .HasKey(g => g.GenderId);
            modelBuilder.Entity<LookupGender>()
            .HasIndex(g => g.GenderName)
            .IsUnique();

            //Student Table Configuration
            modelBuilder.Entity<Student>()
            .HasKey(s => s.StudentId);
            modelBuilder.Entity<Student>()
            .HasIndex(s => s.Email)
            .IsUnique();
            modelBuilder.Entity<Student>()
            .HasIndex(s => s.Phone)
            .IsUnique();

            //Subject Table Configuration
            modelBuilder.Entity<Subject>()
            .HasMany(su => su.Teachers)
            .WithOne(t => t.Subject)
            .HasForeignKey(t => t.SubjectId);

            //Teacher Table Configuration
            modelBuilder.Entity<Teacher>()
            .HasKey(t => t.TeacherId);
            modelBuilder.Entity<Teacher>()
            .HasIndex(t => t.Email)
            .IsUnique();
            modelBuilder.Entity<Teacher>()
            .HasIndex(t => t.Phone)
            .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
