using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinqExamples.Model
{
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
}
