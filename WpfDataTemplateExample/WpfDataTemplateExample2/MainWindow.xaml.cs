using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDataTemplateExample2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //создаем студентов для тестирования
            var students = new[] {
                new Student
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    BirthDate = new DateTime(1992,2,2),
                    Photo = "default.png"
                },
                new Student
                {
                    FirstName = "Сидр",
                    LastName = "Сидоров",
                    BirthDate = new DateTime(1992,3,1),
                    Photo = "Koala.jpg"
                },
                new Student
                {
                    FirstName = "Петр",
                    LastName = "Серегин",
                    BirthDate = new DateTime(1994,3,4),
                    Photo = "Penguins.jpg"
                }
            };
            //задаем источники данных для ListBox'ов
            listBox1.ItemsSource = students;
            listBox2.ItemsSource = students;
        }
    }
}
