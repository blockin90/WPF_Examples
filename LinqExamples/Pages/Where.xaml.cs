using LinqExamples.Model;
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
using System.Windows.Shapes;

namespace LinqExamples.Pages
{
    /// <summary>
    /// Логика взаимодействия для Where.xaml
    /// </summary>
    public partial class Where : Window
    {
        public Where()
        {
            InitializeComponent();
            List<Group> groups = new List<Group>() { new Group() { Name = "Все группы", Id = -1 } };
            groups.AddRange(App.DataModel.Groups.ToArray());
            cbGroups.ItemsSource = groups;
            cbGroups2.ItemsSource = groups;
            cbGroups.SelectedItem = cbGroups2.SelectedItem = groups[0];
            cbGroups2.SelectedItem = groups[0];

            cbAssesmentFilter.ItemsSource = new[] { "Все", "Троечники", "Хорошисты", "Отличники" };
        }
        
        private void whereFilter1(object sender, RoutedEventArgs e)
        {
            var selectedGroup = cbGroups.SelectedItem as Group;
            var lastNameFilter = tbFilter.Text.Trim();
            //если выбрана группа с id == -1, то это элемент "Все группы", поэтому возвращаем true, т.е. отбираем всех студентов
            //иначе фильтруем по совпадению id группы у студента с выбранным элементом
            var result = App.DataModel.Students.Where( 
                s =>  (selectedGroup.Id == -1 ? true : s.GroupId == selectedGroup.Id) &&
                       s.LastName.StartsWith(lastNameFilter)
                ).OrderBy( s=> s.LastName ).ToArray();
            lstStudents.ItemsSource = result;
        }



        bool FilterByAssesment(Student student)
        {
            int FilterAvgRate = cbAssesmentFilter.SelectedIndex;
            double avgRate = student.StudentExams.Count > 0? student.StudentExams.Average(e => e.Assessment) : 0;  //высчитываем среднюю оценку по студенту
            if (FilterAvgRate == 1)
                return avgRate < 3.5;
            else if (FilterAvgRate == 2)
                return avgRate >= 3.5 && avgRate < 4.5;
            else if( FilterAvgRate == 3 )
                return avgRate >= 4.5;
            return true;        //если ничего не выбрано или выбраны "все", то не фильтруем
            
        }

        private void whereFilter2(object sender, RoutedEventArgs e)
        {
            var selectedGroup = cbGroups2.SelectedItem as Group;
            var lastNameFilter = tbFilter2.Text.Trim();
            var result = App.DataModel.Students.Where(
                s => (selectedGroup.Id == -1 ? true : s.GroupId == selectedGroup.Id) &&
                       s.LastName.StartsWith(lastNameFilter)
                ).Where(FilterByAssesment).OrderBy(s => s.LastName).ToArray();
            lstStudents2.ItemsSource = result;
        }

        private void cbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
