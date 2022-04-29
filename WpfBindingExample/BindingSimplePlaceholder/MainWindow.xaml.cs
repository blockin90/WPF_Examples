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

namespace BindingSimplePlaceholder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new People();
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            var bindExpresion = textBox.GetBindingExpression(TextBox.TextProperty);
            var source = bindExpresion.ResolvedSource;
            var propValue = source.GetType().GetProperty(bindExpresion.ResolvedSourcePropertyName).GetValue(source);
            if(propValue == null) {
                textBox.Text = String.Empty;
            }
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (String.IsNullOrEmpty(textBox.Text)) {
                textBox.Text = null;
            }
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(DataContext.ToString());
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new People();
        }
    }
}
