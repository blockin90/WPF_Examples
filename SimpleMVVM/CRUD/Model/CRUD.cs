using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Model
{
    public class CRUD
    {
        public int UniversityComponentId { get; set; }
        public int UserId { get; set; }
        public virtual UniversityComponent UniversityComponent { get; set; }
        public virtual User User { get; set; }

        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}
