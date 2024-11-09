using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double AvgRate { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public string FIO
        {
            get { return LastName + " " + FirstName; }
        }
    }
}
