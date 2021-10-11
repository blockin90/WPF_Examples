using OtherExcelReport.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OtherExcelReport
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //создаем объект контекста как глобальную переменную в классе App
        public static UniversityModel Database { get; } = new UniversityModel();
        static App()
        {
            //создаем параметр DataDirectory для использования в строке подключения 
            //в файле App.Config:  attachDbFileName=|DataDirectory|UniversityModel.mdf
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory);
        }

    }
}
