using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionViewsExample.Model
{
    class ModelInitializer : DropCreateDatabaseIfModelChanges<UniversityModel>
    {
        Random rnd = new Random();
        /// <summary>
        /// Случайная генерация расписания экзаменов на сессии
        /// </summary>
        /// <param name="year">Год, на который генерируется сессия</param>
        /// <returns>коллекция экзаменов</returns> 
        IEnumerable<Exam> GenerateExamsPlan(int year, int semester)
        {
            var disciplines = new[] { "Физика", "Математика", "Программирование", "Русский язык", "БЖД", "Физкультура", "Операционные системы", "Вычислительные системы", "Организация ЭВМ", "Практика" };
            int count = rnd.Next(disciplines.Length / 2, disciplines.Length);   //чтобы внести разнообразие, берем не все дисциплины, но большую часть
            for (int i = 0; i < count; ++i) {
                //если остаток от деления № семестра = 1, семестр зимний, начинается в январе (1 месяц), иначе в июне (6)
                DateTime examDate = new DateTime(year, semester % 2 == 1 ? 1 : 6, 1);
                examDate = examDate.AddDays(rnd.Next(30));  //определяем случайным образом дату
                yield return new Exam()
                {
                    ExamName = disciplines[rnd.Next(disciplines.Length)],
                    Semester = semester,
                    PlannedExamDate = examDate
                };
            }
        }
        IEnumerable<Student> GenerateStudents(Group group, int preferredYear)
        {
            var firstNames = new[] { "Сидор", "Алексей", "Петр", "Никитос", "Евгений", "Сергей", "Петр", "Кирилл" };
            var lastNames = new[] { "Петров", "Петриков", "Петрикян", "Серегов", "Иванов", "Сергеев", "Сидоров", "Николаев" };
            int count = rnd.Next(10, 50);
            for (int i = 0; i < count; i++) {
                var date = new DateTime(preferredYear - 2 + rnd.Next(5), 1, 1);
                date.AddDays(rnd.Next(366));
                yield return new Student()
                {
                    BirthDate = date,
                    FirstName = firstNames[rnd.Next(firstNames.Length)],
                    LastName = lastNames[rnd.Next(lastNames.Length)],
                    Group = group
                };
            }
        }
        IEnumerable<StudentExam> GenerateStudentExams(IEnumerable<Student> students, IEnumerable<Exam> exams)
        {
            foreach(var s in students) {
                foreach( var e in exams) {
                    //добавляем экзамены только в 9/10 случаев
                    if (rnd.NextDouble() < 0.9) {
                        //вероятности получения оценок: 3 = 50%, 4 = 30%, 5 = 20%
                        int assesment = 0;
                        double r = rnd.NextDouble();
                        if (r < .5) {
                            assesment = 3;
                        } else if (r < .8) {
                            assesment = 4;
                        } else {
                            assesment = 5;
                        }
                        DateTime examDate;
                        //вероятность сдачи экзамена с первой попытки 80%:
                        if(rnd.NextDouble() < 0.8) {
                            examDate = e.PlannedExamDate;
                        } else {
                            examDate = e.PlannedExamDate.AddDays(rnd.Next(30));
                        }
                        yield return new StudentExam() { Assessment = assesment, ExamDate = examDate, Exam = e, Student = s };
                    }
                }
            }
        }

        protected override void Seed(UniversityModel db)
        {
            var Groups = new Group[]
            {
                new Group(){ Name = "Иб-51", Year = 2015},
                new Group(){ Name = "Пиб-51", Year = 2015},
                new Group(){ Name = "Иф-51", Year = 2015}
            };
            db.Groups.AddRange(Groups);
            foreach( var g in Groups) {
                db.Students.AddRange(GenerateStudents(g, 1998));
            }
            for( int i =1; i <= 4; ++i) {
                db.Exams.AddRange(GenerateExamsPlan(2016, i));
            }
            db.SaveChanges();

            db.StudentExams.AddRange(GenerateStudentExams(db.Students.ToArray(),db.Exams.ToArray()));
            db.SaveChanges();
        }
    }
}
