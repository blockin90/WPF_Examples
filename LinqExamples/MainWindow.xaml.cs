using LinqExamples.Model;
using LinqExamples.Pages;
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

namespace LinqExamples
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainMenu.IsEnabled = false;
            var students = await Task.Run(() => App.DataModel.Students.OrderBy( s=>s.Group.Name).ThenBy(s=>s.LastName).ToArray());
            mainMenu.IsEnabled = true;
            dgStudents.ItemsSource = students;
            tbStudentsCount.Text = students.Count().ToString();
        }

        private void Where_Click(object sender, RoutedEventArgs e)
        {
            (new Where()).ShowDialog();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            (new Select()).ShowDialog();
        }
    }
}
