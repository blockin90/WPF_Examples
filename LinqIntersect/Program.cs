using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    /* 
     * Требуется  отобрать студентов, имеющих "особые" достижения в обучении 
     * для начисления стипендии имени YYYYY. 
     * "Особыми" достижениями будем считать средний балл выше 4.7 + наличие научных публикаций. 
     * Модель данных определена в UniversityModel.cs
	*/
    class Program
    {
        static void Main(string[] args)
        {
            //задаем параметр для строки подключения, для использования параметра AttachDbFileName вместо initial catalog
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory);

            var Database = new UniversityModel();

            //выбираем всех студентов с оценкой выше 4.7 (Average возвращает среднее значение):
            var goodStudents = Database.Students.Where(s => s.StudentExams.Average(se => se.Assessment) >= 4.5);
            //выводим:
            Console.WriteLine("Список студентов со средней оценкой выше 4.5:");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"{"№",4}| {"ФИО", 20}| {"Оценка", 6}");
            foreach ( var student in goodStudents) {
                Console.WriteLine($"{student.Id,4}| {student.LastName + " " + student.FirstName,20}| { Math.Round(student.StudentExams.Average( se=>se.Assessment),2),6}");
            }
            //выбираем студентов, имеющих публикации:
            var studentsWithPapers = Database.Students.Where(s => s.Papers.Count != 0);
            //выводим
            Console.WriteLine();
            Console.WriteLine("Список студентов, имеющих публикации:");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"{"№",4}| {"ФИО",20}| {"Кол-во статей"}");
            foreach (var student in studentsWithPapers) {
                Console.WriteLine($"{student.Id,4}| {student.LastName + " " + student.FirstName,20}| { student.Papers.Count, 6}");
            }
            //выбираем студентов, присутствующих в обоих списков
            var superStudents = goodStudents.Intersect(studentsWithPapers);
            //выводим:
            Console.WriteLine();
            Console.WriteLine("Список студентов, присутствующих в обоих списках:");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"{"№",4}| {"ФИО",20}");
            foreach (var student in superStudents) {
                Console.WriteLine($"{student.Id,4}| {student.LastName + " " + student.FirstName,20}");
            }
            //уничтожаем объект контекста БД:
            Database.Dispose();
        }
    }
}
