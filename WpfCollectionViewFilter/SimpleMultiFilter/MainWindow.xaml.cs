using CollectionViewsExample.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace SimpleMultiFilter
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
            var students = await App.DatabaseContext.Students.ToArrayAsync();
            MessageBox.Show("студенты загружены!");
            var studentsView = CollectionViewSource.GetDefaultView(students);
            studentsView.Filter = FilterFunction;

            studentDataGrid.ItemsSource = studentsView;
        }

        bool FilterFunction(object item)
        {
            //item является объектом Student, преобразуем:
            var s = item as Student;
            if( FilterByLastName(s) && FilterByGroup(s)) {
                return true;
            }
            return false;
        }
        bool FilterByLastName(Student s)
        {
            if (s.LastName == "Петров") {
                return true;
            }
            return false;
        }
        bool FilterByGroup(Student s)
        {
            if (s.GroupId == 1) {
                return true;
            }
            return false;
        }

    }
}
