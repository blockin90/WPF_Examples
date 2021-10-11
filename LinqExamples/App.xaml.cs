using LinqExamples.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LinqExamples
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory);
        }
        public static UniversityModel DataModel { get; } = new UniversityModel();
    }
}
