using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Model
{
    /// <summary>
    /// Компонент информационной системы для задания прав CRUD
    /// </summary>
    public class UniversityComponent
    {   
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}
