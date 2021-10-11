using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;

namespace LinqExamples.Pages
{
    /// <summary>
    /// Логика взаимодействия для Select.xaml
    /// </summary>
    public partial class Select : Window
    {
        public Select()
        {
            InitializeComponent();
        }

        private void select1_Click(object sender, RoutedEventArgs e)
        {
            var result = App.DataModel.Students.Select(s =>
                   new { Фамилия = s.LastName, Имя = s.FirstName, СредняяОценка = s.StudentExams.Average(se => se.Assessment) }
                ).ToArray();
            dgSelect1.ItemsSource = result;
            tbRecordsCount.Text = result.Length.ToString();

        }

        private void select1ToFile_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == true) {
                var result = App.DataModel.Students.Select(s =>
                       new { Фамилия = s.LastName, Имя = s.FirstName, СредняяОценка = s.StudentExams.Average(se => se.Assessment) }
                    ).ToArray().Select( s => $"{s.Фамилия};{s.Имя};{s.СредняяОценка}").ToArray();
                if( new FileInfo(dlg.FileName).Extension == "") {
                    dlg.FileName += ".txt";
                }
                File.WriteAllLines(dlg.FileName, result);
                MessageBox.Show("Сохранено!");
            }

        }

        private void selectMany_Click(object sender, RoutedEventArgs e)
        {
            var result = App.DataModel.Students.SelectMany(s => s.StudentExams ).
                Select( se => 
                    new { ФИО = se.Student.LastName + " " + se.Student.FirstName,
                          Группа = se.Student.Group.Name,
                          Экзамен = se.Exam.ExamName,
                          Дата = se.ExamDate,
                          Оценка = se.Assessment
                         }
                ).ToArray();
            dgSelect1.ItemsSource = result;
            tbRecordsCount.Text = result.Length.ToString();
        }
    }
}
