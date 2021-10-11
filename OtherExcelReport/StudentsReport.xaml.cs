using OtherExcelReport.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
//указываем, что будем использовать библиотеку Microsoft Excel Object Library
//под псевдонимом Excel:
using Excel = Microsoft.Office.Interop.Excel;


namespace OtherExcelReport
{
    /// <summary>
    /// Логика взаимодействия для StudentsReport.xaml
    /// </summary>
    public partial class StudentsReport : Window
    {
        public StudentsReport()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //первый запуск может выполняться долго из-за генерации базы данных
            //в боевом приложении весь нижележащий код вынести в отдельный поток:
            //await Task.Run(() => { код загрузки данных }); 
            //cbGroups = результат загрузки; cbAges = результат загрузки

            //формируем элементы для выпадающих списков:
            var groups = new[] { new Group { Name = "Все группы", Id = -1 } }
                .Union(App.Database.Groups.OrderBy(g => g.Name).ToArray());
            cbGroups.ItemsSource = groups;
            //выбираем возраст всех обучающихся студентов
            // DbFunctions.DiffMonths(s.BirthDate, DateTime.Now)/12 - очень грубый расчет возраста
            var studentAges = App.Database.Students
                .Select(s => DbFunctions.DiffMonths(s.BirthDate, DateTime.Now)/12)
                .Where(age => age != null)
                .Select(age => age.ToString())
                .OrderBy(age => age);
            cbAge.ItemsSource = new[] { "Любой возраст" }.Union(studentAges);
        }
        int GetAge(Student student)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = DateTime.Now - student.BirthDate.Value;
            
            int years = (zeroTime + span).Year - 1;
            return years;
        }
        private void cb_FilterStudents(object sender, SelectionChangedEventArgs e)
        {
            //берем выбранную группу
            Group selectedGroup = cbGroups.SelectedItem as Group;
            //создаем делегат для фильтрации групп:
            Func<Group, bool> groupFilter;
            if ( selectedGroup == null || selectedGroup.Id == -1) {
                //если группа не выбрана или выбраны все группы, 
                //в качестве условия фильтрации ставим true, т.е. отбираем все группы
                groupFilter = (group) => true;
            } else {
                //если группа выбрана, в качестве условия отбора ставим соответствие
                //кода группы той группе, что была выбрана
                groupFilter = (group) => group.Id == selectedGroup.Id;
            }
            //повторяем ту же процедуру (берем всех, если не выбрано, иначе фильтруем) для отбора студентов по возрасту
            string selectedAge = cbAge.SelectedItem as string;
            Func<Student, bool> ageFilter;
            if (selectedAge == null || selectedAge == "Любой возраст" ) {
                ageFilter = (student) => true;
                
            } else {
                //в качестве условия фильтрации:
                //разница в годах (текущая дата минус дата рождения) == выбранный возраст
                //функция GetAge определена выше
                ageFilter = (student) => GetAge( student ).ToString() == selectedAge;
            }

            var students = App.Database.Groups
                .Where(groupFilter)     //применяем определенный выше фильтр для отбора групп
                .SelectMany(g => g.Students)    //выбираем студентов из выбранных групп
                .Where( ageFilter )  //и применяем к ним фильтр для студентов
                .Select( ( student, num ) => $"{num+1,2} {student.Group.Name,7} {student.LastName} {student.FirstName}")
                .ToArray();
            lbStudents.ItemsSource = students;
        }

        //Плохой код, который в реальной среде нужно разбить на несколько методов
        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            /* формируем отчет вида
             *  ________________________________________
             * |Список студентов XX лет по группам:     |
             * |ПИБ-51                                  |
             * |  1. Такой то такойтович                |
             * |  2. Такой то такойтович №2             |
             * |  ..........                            |
             * |ИБ-51                                   |
             * |  1. Такой то такойтович                |
             * |  ..........                            |
             * |                                        |
             *  ========================================
             */
            //формируем фильтры для выборки (см. пример выше), можно вынести это в отдельные функции
            Group selectedGroup = cbGroups.SelectedItem as Group;
            Func<Group, bool> groupFilter;
            if (selectedGroup == null || selectedGroup.Id == -1) {
                groupFilter = (group) => true;
            } else {
                groupFilter = (group) => group.Id == selectedGroup.Id;
            }
            string selectedAge = cbAge.SelectedItem as string;
            Func<Student, bool> ageFilter;
            if (selectedAge == null || selectedAge == "Любой возраст") {
                ageFilter = (student) => true;
            } else {
                ageFilter = (student) => GetAge(student).ToString() == selectedAge;
            }

            //сами данные будем выбирать по мере формирования отчета
            //далее комментируется только код, относящийся к выгрузке данных, об основах работы с Excel см. MainWindow  
            var groups =  App.Database.Groups.Where(groupFilter);
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workBook = excelApp.Workbooks.Add();
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
            Excel.Range header = workSheet.Range["A1", "I1"];
            header.Merge();
            header.HorizontalAlignment = Excel.Constants.xlCenter;
            header.Font.Bold = true;
            if (selectedAge == null || selectedAge == "Любой возраст") {
                header.Value = "Список студентов по группам";
            } else {
                header.Value = $"Список студентов {selectedAge} лет по группам";
            }
            //задаем небольшую ширину для столбцов A,B для настройки отображения
            Excel.Range columnA = workSheet.Columns[1];
            Excel.Range columnB = workSheet.Columns[2];
            columnA.ColumnWidth = 2.5;
            columnB.ColumnWidth = 2.5;

            int currentRow = 2; //номер текущей строки, на которую будем выводить данные
            //цикл по выбранным группам
            foreach ( Group g in groups) {
                //для каждой группы формируем жирный заголовок, далее перечисляем студентов
                Excel.Range groupHeader = workSheet.Range[$"A{currentRow}", $"C{currentRow}"];
                groupHeader.Merge();
                groupHeader.Font.Bold = true;
                groupHeader.Value = g.Name;
                currentRow++;
                //выбираем студентов и их порядковые номера как объект анонимного класса:
                var groupStudents = g.Students.Select((s, num) => new { Num = num + 1, Student = s });
                foreach( var s in groupStudents) {
                    workSheet.Range[$"B{currentRow}"].Value = s.Num;
                    workSheet.Range[$"C{currentRow}"].Value = s.Student.LastName + " " + s.Student.FirstName;
                    currentRow++;
                }
            }
            //а весь документ возьмем в жирную рамку:
            Excel.Range docRange = workSheet.Range["A1", $"I{currentRow}"];
            docRange.BorderAround2(Weight: Excel.XlBorderWeight.xlMedium);
            excelApp.Visible = true;
            excelApp.UserControl = true;
            var students = App.Database.Groups
                .Where(groupFilter)     //применяем определенный выше фильтр для отбора групп
                .SelectMany(g => g.Students)    //выбираем студентов из выбранных групп
                .Where(ageFilter)  //и применяем к ним фильтр для студентов
                .Select((student, num) => $"{num + 1,2} {student.Group.Name,7} {student.LastName} {student.FirstName}")
                .ToArray();
            lbStudents.ItemsSource = students;
        }
    }
}
