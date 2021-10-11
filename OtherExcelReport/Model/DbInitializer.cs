using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtherExcelReport.Model
{
    /// <summary>
    /// Класс инициализатор для UniversityModel
    /// </summary>
    public class DbInitializer : CreateDatabaseIfNotExists<UniversityModel>
    {
        Random rnd = new Random();

        protected override void Seed(UniversityModel context)
        {
            var Groups = new List<Group>()
            {
                new Group(){ Name = "Иб-51"},
                new Group(){ Name = "Пиб-51"},
                new Group(){ Name = "Иф-51"}
            };
            var Students = new List<Student>()
            {
                new Student(){ FirstName = "Сидор", LastName = "Петров", BirthDate = GetRandomBirthDate(),  Group = Groups[0]},
                new Student(){ FirstName = "Алексей", LastName = "Петриков", BirthDate = GetRandomBirthDate(), Group = Groups[0]},
                new Student(){ FirstName = "Петр", LastName = "Петрикян", BirthDate = GetRandomBirthDate(), Group = Groups[0]},
                new Student(){ FirstName = "Никитос", LastName = "Сережников", BirthDate = GetRandomBirthDate(), Group = Groups[1]},
                new Student(){ FirstName = "Жекос", LastName = "Иванов", BirthDate = GetRandomBirthDate(), Group = Groups[1]},
                new Student(){ FirstName = "Сергей", LastName = "Сергеев", BirthDate = GetRandomBirthDate(), Group = Groups[1]},
                new Student(){ FirstName = "Петр", LastName = "Сидоров", BirthDate = GetRandomBirthDate(), Group = Groups[1]},
                new Student(){ FirstName = "Кирилл", LastName = "Николаевич", BirthDate = GetRandomBirthDate(), Group = Groups[2]}
            };
            context.Students.AddRange(Students);
            base.Seed(context);
        }
        /// <summary>
        /// Генерация случайной даты в диапазоне 01.01.1999 - 31.12.2001
        /// </summary>
        DateTime GetRandomBirthDate()
        {
            //631453536000000000 - число тиков, соответствующее дате 31.12.2001
            //630507456000000000 - число тиков, соответствующее дате 01.01.1999
            //получаем случайную дату из обозначенного выше диапазона:
            var ticks = (long)(630507456000000000 + rnd.NextDouble() * (631453536000000000 - 630507456000000000));
            return new DateTime(ticks);
        }

    }
}
