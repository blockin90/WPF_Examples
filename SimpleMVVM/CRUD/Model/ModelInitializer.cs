using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Model
{
    class ModelInitializer : DropCreateDatabaseAlways<UniversityModel>
    {
        protected override void Seed(UniversityModel db)
        {
            var Groups = new Group[]
            {
                new Group(){ Name = "Иб-51"},
                new Group(){ Name = "Пиб-51"},
                new Group(){ Name = "Иф-51"}
            };
            var Students = new Student[]
            {
                new Student(){ FirstName = "Сидор", LastName = "Петров", Age = 20, AvgRate = 3.5, Group = Groups[0]},
                new Student(){ FirstName = "Алексей", LastName = "Петриков", Age = 19, AvgRate = 3.25, Group = Groups[0]},
                new Student(){ FirstName = "Петр", LastName = "Петрикян", Age = 21, AvgRate = 4.23, Group = Groups[0]},
                new Student(){ FirstName = "Никитос", LastName = "Сережников", Age = 20, AvgRate = 3.5, Group = Groups[1]},
                new Student(){ FirstName = "Жекос", LastName = "Иванов", Age = 21, AvgRate = 4.15, Group = Groups[1]},
                new Student(){ FirstName = "Сергей", LastName = "Сергеев", Age = 22, AvgRate = 4.85, Group = Groups[1]},
                new Student(){ FirstName = "Петр", LastName = "Сидоров", Age = 18, AvgRate = 3.0, Group = Groups[1]},
                new Student(){ FirstName = "Кирилл", LastName = "Николаевич", Age = 18, AvgRate = 5.0, Group = Groups[2]},
            };
            var components = new UniversityComponent[]
            {
                new UniversityComponent(){ Name = "StudentsList" },
                new UniversityComponent(){ Name = "StudentsNotes" }
            };
            var users = new User[]
            {
                new User{ Login = "user1", Password = "12345"},
                new User{ Login = "vasja", Password = "12345"},
                new User{ Login = "petr", Password = "12345"}
            };
            var cruds = new CRUD[]
            {
                new CRUD(){ User = users[0], UniversityComponent = components[0], CanRead = true, CanCreate = true },
                new CRUD(){ User = users[1], UniversityComponent = components[0], CanCreate = true },
                new CRUD(){ User = users[1], UniversityComponent = components[1], CanCreate = true, CanDelete = true },
                new CRUD(){ User = users[2], UniversityComponent = components[0], CanRead = true, CanCreate = true, CanDelete = true, CanUpdate = true}
            };
            db.Groups.AddRange(Groups);
            db.Students.AddRange(Students);
            db.CRUDs.AddRange(cruds);
            
            db.SaveChanges();
        }

    }
}
