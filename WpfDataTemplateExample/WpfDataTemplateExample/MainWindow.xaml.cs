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

namespace WpfDataTemplateExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //создаем массив студентов
            var students = new[] {
                new Student {FirstName= "Иван", LastName = "Иванов", BirthDate = new DateTime(1992,2,2)},
                new Student {FirstName= "Сидр", LastName = "Сидоров", BirthDate = new DateTime(1992,3,1)},
                new Student {FirstName= "Петр", LastName = "Иванов", BirthDate = new DateTime(1994,3,4)},
            };
            //и выводим его во всех элементы управления (листбокс, комбобокс, датагрид):
            cbStudents.ItemsSource = students;
            lbStudents.ItemsSource = students;
            dgStudents.ItemsSource = students;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Редактирование студента", "Заглушка");
        }
    }
}
