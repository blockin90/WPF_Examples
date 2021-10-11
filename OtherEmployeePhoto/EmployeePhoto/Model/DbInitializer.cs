using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePhoto.Model
{
    /// <summary>
    /// Инциализатор модели данных
    /// </summary>
    public class DbInitializer : DropCreateDatabaseIfModelChanges<DataModel>
    {
        protected override void Seed(DataModel context)
        {
            //Создаем список должностей, которые будут доступны с первого же запуска программы
            context.Positions.AddRange(
                new[] {
                    new Position { Name = "Начальник" },
                    new Position { Name = "Самый главный начальник"},
                    new Position { Name = "Рабочий"}
                }
            );
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
