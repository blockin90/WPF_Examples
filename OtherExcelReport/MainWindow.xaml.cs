using OtherExcelReport.Model;
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
//указываем, что будем использовать библиотеку Microsoft Excel Object Library
//под псевдонимом Excel:
using Excel = Microsoft.Office.Interop.Excel;

namespace OtherExcelReport
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UniversityModel model = new UniversityModel();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MakeReport_Click(object sender, RoutedEventArgs e)
        {
            // Создаём экземпляр Excel
            Excel.Application excelApp = new Excel.Application();
            // В запущенном экземпляре Excel добавляем рабочую книги Excel
            Excel.Workbook workBook = excelApp.Workbooks.Add();
            // Получаем первый лист книги Excel
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
            //далее работа сводится к заданию значений ячеек
            //обратиться к ячейке мы можем двумя способами:
            //1) workSheet.Cells[номер_строки, номер_столбца], 
            //   например, следующий код запишет "Привет" в ячейку B1:
            //   workSheet.Cells[1, 2] = "Привет"; 
            //2) другой способ - через явное задание диапазона: 
            //   workSheet.Range["B1"] = "Привет";
            //   используя Range можно получать не только одну ячейку, но и выделять 
            //   для обработки сразу несколько:  workSheet.Range["B2", "B5"]

            //формируем заголовок, для этого объединяем верхние ячейки:
            Excel.Range header = workSheet.Range["A1", "I1"];
            header.Merge();
            //и записываем туда какой то текст:
            header.Value = "Бесполезный документ";
            //задаем выравнивание по центру и жирный шрифт:
            header.HorizontalAlignment = Excel.Constants.xlCenter;
            header.Font.Bold = true;

            //покажем использование формул, для этого вторую строку заполним
            //случайными числами, а на третьей посчитаем среднее значение
            Random rnd = new Random();
            for (int j = 1; j < 10; j++) {
                workSheet.Cells[2, j] = rnd.Next(1,10);
            }
            Excel.Range avgCell = workSheet.Cells[3, 1];
            avgCell.Formula = "=СРЗНАЧ(A2:I2)";
            avgCell.FormulaHidden = false;
            //оформим тонкую границу вокруг каждого из случайных чисел:
            for (int j = 1; j < 10; j++) {
                Excel.Range cell = workSheet.Cells[2, j];
                cell.BorderAround2();
            }
            //а весь документ возьмем в жирную рамку:
            Excel.Range docRange = workSheet.Range["A1", "I3"];
            docRange.BorderAround2(Weight : Excel.XlBorderWeight.xlThick);

            //построим график типа "столбчатая диаграмма"
            Excel.ChartObjects chartObjs = (Excel.ChartObjects)workSheet.ChartObjects();
            //рассчитаем координаты создаваемого графика
            //график будет располагаться в диапазоне 4-10 строки,1-9 столбцы
            //берем заданный диапазон
            Excel.Range chartRange = workSheet.Range["A4","I10"];
            //и размещаем график по координатам этого диапазона:
            Excel.ChartObject chartObj = chartObjs.Add( chartRange.Left, chartRange.Top, chartRange.Width, chartRange.Height);
            Excel.Chart xlChart = chartObj.Chart;
            // Устанавливаем тип диаграммы
            xlChart.ChartType = Excel.XlChartType.xlColumnClustered;
            //указываем источник данных:
            xlChart.SetSourceData(workSheet.Range["A2", "I2"]);
            // Открываем созданный excel-файл
            excelApp.Visible = true;
            excelApp.UserControl = true;
            //или, например, можно сразу отправить на печать:
            //workSheet.PrintOutEx();            
        }

        private void OpenStudentForm_Click(object sender, RoutedEventArgs e)
        {
            StudentsReport reportForm = new StudentsReport();
            reportForm.ShowDialog();
        }
    }
}
