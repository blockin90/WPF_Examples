using CRUD.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CRUD
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
        static UniversityModel _dataModel;
        public static UniversityModel DataModel
        {
            get
            {
                if( _dataModel == null) {
                    _dataModel = new UniversityModel();
                }
                return _dataModel;
            }
        }
        public static User CurrentUser { get; set; }
    }
}
