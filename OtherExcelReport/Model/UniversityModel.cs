namespace OtherExcelReport.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UniversityModel : DbContext
    {
        static UniversityModel()
        {
            //задаем инициализатор базы данных
            Database.SetInitializer(new DbInitializer());
        }
        public UniversityModel()
            : base("name=UniversityModel")
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}