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

namespace BindingExample2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //создаем студента для демонстрации биндинга
            Student student = new Student()
            {
                FirstName = "Вася",
                LastName = "Иванов",
                BirthDay = new DateTime(1990, 1, 1),
                Papers = new List<Paper>
                {
                    new Paper{ Id = 3, Title = "Название статьи 1", Magazine = "Вестник науки", Volume = 0.4, PublicDate = new DateTime( 2018,02,14)},
                    new Paper{ Id = 4, Title = "Название статьи 2", Magazine = "Сборник..", Volume = 0.2, PublicDate = new DateTime( 2018,04,20)},
                    new Paper{ Id = 7, Title = "Название статьи 3", Magazine = "Вестник СибУПК", Volume = 0.6, PublicDate = new DateTime( 2018,06,20)}
                 }
            };
            //и устанавливаем его в качестве контекста данных:
            DataContext = student;
        }

        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            //sender - источник события, в данном случае кнопка, получаем на нее ссылку:
            Button btn = sender as Button;
            //контекстом данных является объект, который представляет текущую строку, в данном случае - Paper:
            Paper p = btn.DataContext as Paper;
            //формируем строку с данными по публикации:
            string message = $"Id:\t{p.Id}" + Environment.NewLine +
                             $"Журнал:\t{p.Magazine}" + Environment.NewLine +
                             $"Название:\t{p.Title}" + Environment.NewLine +
                             $"Объем:\t{p.Volume} п.л." + Environment.NewLine +
                             $"Дата:\t{p.PublicDate.ToShortDateString()}";
            //и выводим ее:
            MessageBox.Show(message);
        }
    }
}
