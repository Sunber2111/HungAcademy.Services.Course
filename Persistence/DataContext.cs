using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<CaseStudy> CaseStudies { get; set; }

        public DbSet<UserWatcher> UserWatchers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Course>(entity =>
            {
                // set primary key
                entity.HasKey(x => x.Id);

                // auto gen key
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                // set foreign key
                entity.HasOne(x => x.Category)
                       .WithMany(x => x.Courses)
                       .HasForeignKey(x => x.CategoryId)
                       .OnDelete(DeleteBehavior.SetNull);

            });

            builder.Entity<Category>(entity =>
            {
                // set primary key
                entity.HasKey(x => x.Id);

                // auto gen key
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

            });

            builder.Entity<Lecture>(entity =>
            {
                // set primary key
                entity.HasKey(x => x.Id);

                // auto gen key
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                // set foreign key
                entity.HasOne(x => x.Section)
                       .WithMany(x => x.Lectures)
                       .HasForeignKey(x => x.SectionId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Section>(entity =>
            {
                // set primary key
                entity.HasKey(x => x.Id);

                // auto gen key
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                // set foreign key
                entity.HasOne(x => x.Course)
                       .WithMany(x => x.Sections)
                       .HasForeignKey(x => x.CourseId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<CaseStudy>(entity =>
            {
                // set primary key
                entity.HasKey(x => x.Id);

                // auto gen key
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                // set foreign key
                entity.HasOne(x => x.Course)
                       .WithMany(x => x.CaseStudies)
                       .HasForeignKey(x => x.CourseId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<UserWatcher>(entity =>
            {
                // set primary key
                entity.HasKey(x => new { x.LectureId, x.UserId });

                // set foreign key
                entity.HasOne(x => x.Lecture)
                       .WithMany(x => x.UserWatchers)
                       .HasForeignKey(x => x.LectureId)
                       .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
