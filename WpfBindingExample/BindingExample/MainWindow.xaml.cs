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

namespace BindingExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Student s = new Student()
        {
            FirstName = "Вася",
            LastName = "Иванов",
            BirthDay = new DateTime(1990, 1, 1),
            Papers = new List<string> {"Статья 1","Статья 2", "Статья 3"}
        };
        public MainWindow()
        {
            InitializeComponent();
            gridUserInfo.DataContext = s;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(String.Format("Имя: {0}, Фамилия: {1}, Дата рождения: {2}", s.FirstName, s.LastName, s.BirthDay));
        }
    }
}
