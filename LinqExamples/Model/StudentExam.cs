using System;
using System.ComponentModel.DataAnnotations;

namespace LinqExamples.Model
{
    public class StudentExam
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public DateTime ExamDate { get; set; }
        [Range(2, 5)]
        public int Assessment { get; set; }
        public virtual Student Student { get; set; }
        public virtual Exam Exam { get; set; }
    }
}