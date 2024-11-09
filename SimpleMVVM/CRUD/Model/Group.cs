using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Model
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary> Год набора </summary>
        public int Year { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
