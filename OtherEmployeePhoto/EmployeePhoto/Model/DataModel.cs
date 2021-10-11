namespace EmployeePhoto.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataModel : DbContext
    {
        static DataModel()
        {
            //задаем инициализатор, чтобы заполнить БД начальными данными
            Database.SetInitializer(new DbInitializer());
        }
        public DataModel()
            : base("name=Model1")
        {
        }
        /// <summary>Должности</summary>
        public virtual DbSet<Position> Positions { get; set; }
        /// <summary>Список сотрудников, у которых в БД хранится путь к изображению (без INotifyPropertyChanged)</summary>
        public virtual DbSet<Employee> Employees { get; set; }
    }

}