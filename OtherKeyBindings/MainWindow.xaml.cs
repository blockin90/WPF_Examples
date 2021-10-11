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

namespace WpfKeyBindings
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

        private void NewFileCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if( MessageBox.Show("Создать новый файл?","......",MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                //действия, если была нажата кнопка "Да"............
            }
        }

        private void ExitExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Демонстрация привязки горячих клавиш к элементам управления.");
        }

        private void HelpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Здесь могла быть ваша справка по программе","Справка",MessageBoxButton.OK,MessageBoxImage.Question);
        }
    }
}
