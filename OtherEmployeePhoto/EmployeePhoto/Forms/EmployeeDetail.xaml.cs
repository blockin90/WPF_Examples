using EmployeePhoto.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace EmployeePhoto.Forms
{
    /// <summary>
    /// Логика взаимодействия для EmployeeDetail1.xaml
    /// </summary>
    public partial class EmployeeDetail : Window
    {
        Employee _employee;
        public EmployeeDetail(Employee employee )
        {
            InitializeComponent();
            //загружаем список должностей в выпадающий список:
            cbPosition.ItemsSource = App.Database.Positions.ToArray();
            //устанавливаем переданного сотрудника как контекст данных
            DataContext = employee;
            //и сохраняем ссылку в _employee
            _employee = employee;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            //сообщаем вызывающей стороне, что результат диалогового окна успешен:
            DialogResult = true;
            //и закрываем окно
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            //создаем диалоговое окно для выбора файлов
            OpenFileDialog fileDialog = new OpenFileDialog();
            //настраиваем доступные типы файлов
            fileDialog.Filter = "Изображения|*.bmp;*.jpg;*.png|Все файлы|*.*";
            //открываем диалоговое окно и сразу смотрим результат, если true - пользователь выбрал файл
            if (fileDialog.ShowDialog() == true) {
                //выбранный файл нужно скопировать к исполняемой программе, в папку photos
                //а если папки нет? нужно создать
                Directory.CreateDirectory("photos");
                //копируем выбранный файл
                File.Copy(fileDialog.FileName, Directory.GetCurrentDirectory() + "\\photos\\" + fileDialog.SafeFileName, true);
                //когда файл скопирован, указать это в объект класса "сотрудник":
                _employee.Photo = fileDialog.SafeFileName;
            }
        }
    }
}
