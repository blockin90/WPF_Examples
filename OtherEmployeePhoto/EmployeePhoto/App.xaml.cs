using EmployeePhoto.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeePhoto
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            //задаем параметр для строки подключения, для использования параметра AttachDbFileName вместо initial catalog
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory);
           
        }
        public static DataModel Database = new DataModel();

    }
}
