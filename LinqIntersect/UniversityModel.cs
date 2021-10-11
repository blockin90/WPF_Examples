namespace ConsoleApp4
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class UniversityModel : DbContext
    {
        static UniversityModel()
        {
            //задаем инициализатор БД
            Database.SetInitializer(new ModelInitializer());
        }

        public UniversityModel()
             : base("name=Model1")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentExam>().HasKey(se => new { se.StudentId, se.ExamId, se.ExamDate });
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<StudentExam> StudentExams { get; set; }

    }

    public class Student
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public virtual ICollection<StudentExam> StudentExams { get; set; }
        public virtual ICollection<Paper> Papers { get; set; }
        
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public Student()
        {
            StudentExams = new List<StudentExam>();
            Papers = new List<Paper>();
        }
    }

    public class Paper
    {
        public int Id { get; set; }
        //название публикаци:
        public string Name { get; set; }
        //автор публикаци:
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int Year { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public Group()
        {
            Students = new List<Student>();
        }
    }

    public class Exam
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string ExamName { get; set; }
        [Range(1, 12)]
        public int Semester { get; set; }
        public DateTime PlannedExamDate { get; set; }
        public virtual ICollection<StudentExam> StudentExams { get; set; }
        public Exam()
        {
            StudentExams = new List<StudentExam>();
        }
    }
    public class StudentExam
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public DateTime ExamDate { get; set; }
        [Range(2, 5)]
        public int Assessment { get; set; }
    }
}