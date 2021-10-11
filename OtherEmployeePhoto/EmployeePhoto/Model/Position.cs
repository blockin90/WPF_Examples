using System.Collections.Generic;

namespace EmployeePhoto.Model
{
    /// <summary>
    /// Должность сотрудника
    /// </summary>
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}