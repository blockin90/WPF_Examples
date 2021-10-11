using System;
using System.Data.Entity;

namespace LinqExamples.Model
{

    public class UniversityModel : DbContext
    {
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<StudentExam> StudentExams { get; set; }

        static UniversityModel()
        {
            Database.SetInitializer(new ModelInitializer());
        }
        public UniversityModel()
            : base("name=UniversityModel")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentExam>().HasKey(se => new { se.StudentId, se.ExamId, se.ExamDate });
            base.OnModelCreating(modelBuilder);
        }


    }
}