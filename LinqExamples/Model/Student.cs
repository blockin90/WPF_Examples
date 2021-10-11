using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinqExamples.Model
{
    public class Student
    {
        public int Id { get; set; }
        /// <summary> Средняя оценка студента за время обучения </summary>
        [NotMapped]
        public double AverageRate { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public virtual ICollection<StudentExam> StudentExams { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        [NotMapped]
        public string FIO
        {
            get { return $"{LastName} {FirstName}"; }
        }


        public Student()
        {
            StudentExams = new List<StudentExam>();
        }
    }
}
