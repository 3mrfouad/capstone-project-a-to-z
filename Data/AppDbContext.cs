using AZLearn.Models;
using Microsoft.EntityFrameworkCore;

namespace AZLearn.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Cohort> Cohorts { get; set; }
        public virtual DbSet<CohortCourse> CohortCourses { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Homework> Homeworks { get; set; }
        public virtual DbSet<Rubric> Rubrics { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Timesheet> Timesheets { get; set; }
        public virtual DbSet<ShoutOut> ShoutOuts { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connection =
                    "server=localhost;" +
                    "port = 3306;" +
                    "user = root;" +
                    "database = AZLearnDb;";

                string version = "10.4.14-MariaDB";

                optionsBuilder.UseMySql(connection, x => x.ServerVersion(version));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Cohort Table

            modelBuilder.Entity<Cohort>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ModeOfTeaching)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.City)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            #endregion

            #region Course Table

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Description)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            #endregion

            #region CohortCourse Table

            modelBuilder.Entity<CohortCourse>(entity =>
            {
                /* Creating Composite Key with CohortId, CourseId  */
                entity.HasKey(e => new { e.CohortId, e.CourseId });

                entity.Property(e => e.ResourcesLink)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.InstructorId)
                    .HasName("FK_CohortCourse_Instructor");

                entity.HasOne(thisEntity => thisEntity.Instructor)
                    .WithMany(parent => parent.CohortCourses)
                    .HasForeignKey(thisEntity => thisEntity.InstructorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CohortCourse_Instructor");
            });

            #endregion

            #region Homework Table

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.Property(e => e.Title)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DocumentLink)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.GitHubClassRoomLink)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CourseId)
                    .HasName("FK_Homework_Course");

                entity.HasIndex(e => e.InstructorId)
                    .HasName("FK_Homework_Instructor");

                entity.HasIndex(e => e.CohortId)
                    .HasName("FK_Homework_Cohort");

                entity.HasOne(thisEntity => thisEntity.Course)
                    .WithMany(parent => parent.Homeworks)
                    .HasForeignKey(thisEntity => thisEntity.CourseId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Homework_Course");

                entity.HasOne(thisEntity => thisEntity.Instructor)
                    .WithMany(parent => parent.Homeworks)
                    .HasForeignKey(thisEntity => thisEntity.InstructorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Homework_Instructor");

                entity.HasOne(thisEntity => thisEntity.Cohort)
                    .WithMany(parent => parent.Homeworks)
                    .HasForeignKey(thisEntity => thisEntity.CohortId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Homework_Cohort");
            });

            #endregion

            #region Rubric Table

            modelBuilder.Entity<Rubric>(entity =>
            {
                entity.Property(e => e.Criteria)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.HomeworkId)
                    .HasName("FK_Rubric_Homework");

                entity.HasOne(thisEntity => thisEntity.Homework)
                    .WithMany(parent => parent.Rubrics)
                    .HasForeignKey(thisEntity => thisEntity.HomeworkId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Rubric_Homework");
            });

            #endregion

            #region Grade Table

            modelBuilder.Entity<Grade>(entity =>
            {
                /* Creating Composite key with RubricId, StudentId */
                entity.HasKey(e => new { e.RubricId, e.StudentId });

                entity.Property(e => e.InstructorComment)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.StudentComment)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            #endregion

            #region User Table

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PasswordHash)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Email)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CohortId)
                    .HasName("FK_User_Cohort"); /* User here can be either Student or Instructor, CohortId is 0 for Instructor*/

                entity.HasOne(thisEntity => thisEntity.Cohort)
                    .WithMany(parent => parent.Users)
                    .HasForeignKey(thisEntity => thisEntity.CohortId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_User_Cohort");
            });

            #endregion

            #region Timesheet Table

            modelBuilder.Entity<Timesheet>(entity =>
            {
                entity.HasIndex(e => e.HomeworkId)
                    .HasName("FK_Timesheet_Homework");

                entity.HasIndex(e => e.StudentId)
                    .HasName("FK_Timesheet_Student");

                entity.HasOne(thisEntity => thisEntity.Homework)
                    .WithMany(parent => parent.Timesheets)
                    .HasForeignKey(thisEntity => thisEntity.HomeworkId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Timesheet_Homework");

                entity.HasOne(thisEntity => thisEntity.Student)
                    .WithMany(parent => parent.Timesheets)
                    .HasForeignKey(thisEntity => thisEntity.StudentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Timesheet_Student");
            });

            #endregion

            #region Shoutout Table

            modelBuilder.Entity<ShoutOut>(entity =>
            {
                entity.Property(e => e.Topic)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Comment)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.StudentId)
                    .HasName("FK_ShoutOut_Student");

                entity.HasIndex(e => e.PeerId)
                    .HasName("FK_ShoutOut_Peer");

                entity.HasIndex(e => e.HomeworkId)
                    .HasName("FK_ShoutOut_Homework");

                entity.HasOne(thisEntity => thisEntity.Student)
                    .WithMany(parent => parent.ShoutOutsStudent)
                    .HasForeignKey(thisEntity => thisEntity.StudentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ShoutOut_Student");

                entity.HasOne(thisEntity => thisEntity.Peer)
                    .WithMany(parent => parent.ShoutOutsPeer)
                    .HasForeignKey(thisEntity => thisEntity.PeerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ShoutOut_Peer");

                entity.HasOne(thisEntity => thisEntity.Homework)
                    .WithMany(parent => parent.ShoutOuts)
                    .HasForeignKey(thisEntity => thisEntity.HomeworkId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ShoutOut_Homework");
            });

            #endregion

            #region Notification Table

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.StudentId)
                    .HasName("FK_Notification_Student");

                entity.HasOne(thisEntity => thisEntity.Student)
                    .WithMany(parent => parent.Notifications)
                    .HasForeignKey(thisEntity => thisEntity.StudentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Notification_Student");
            });

            #endregion
        }
    }
}