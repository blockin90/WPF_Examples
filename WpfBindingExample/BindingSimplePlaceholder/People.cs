using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingSimplePlaceholder
{
    class People
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
