using CollectionViewsExample.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleMultiFilter
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UniversityModel DatabaseContext { get; } = new UniversityModel();

        static App()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory);
        }
    }
}
