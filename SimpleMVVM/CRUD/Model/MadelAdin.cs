namespace MVVM_Students.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
   
    class MyContextInitializer : DropCreateDatabaseAlways<MadelAdin>
    {
        protected override void Seed(MadelAdin db)
        {
            List<Group> Groups;
            List<Student> Students;

            Groups = new List<Group>()
            {
                new Group(){ Name = "Иб-51"},
                new Group(){ Name = "Пиб-51"},
                new Group(){ Name = "Иф-51"}
            };
            Students = new List<Student>()
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
            db.Groups.AddRange(Groups);
            db.Students.AddRange(Students);
            db.SaveChanges();
        }
    }
    public class MadelAdin : DbContext
    {

        static MadelAdin()
        {
            Database.SetInitializer<MadelAdin>(new MyContextInitializer());
        }
        // Контекст настроен для использования строки подключения "MadelAdin" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "MVVM_Students.Model.MadelAdin" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "MadelAdin" 
        // в файле конфигурации приложения.
        public MadelAdin()
            : base("name=MadelAdin")
        {

        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
    }
    //spasite , neponyatno 
    public class Student
    { //kommentariy , poprosili sdelat` , ya sdelal
        public int Id { get; set; }  //tipa nado , ya hz 
        public string FirstName { get; set; }//eto ne kot
        public string LastName { get; set; }
        public int Age { get; set; }
        public double AvgRate { get; set; }


        public int GroupId { get; set; }
        public Group Group { get; set; }


        public Student()
        {

        }
    }

    public class Group
    {
        public int Id { get; set; } //ffu 
        public string Name { get; set; }
        public int Kurs { set; get; }
        public Group()
        {
            Students = new List<Student>();
        }
        public virtual ICollection<Student> Students { get; set; }
    }


    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}