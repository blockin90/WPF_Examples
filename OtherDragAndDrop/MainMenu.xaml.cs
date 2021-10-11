using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnDragAndDrop_Click(object sender, RoutedEventArgs e)
        {
            DragAndDrop form = new DragAndDrop();
            form.ShowDialog();
        }

        private void btnImgMoving_Click(object sender, RoutedEventArgs e)
        {
            ImageMoving form = new ImageMoving();
            form.ShowDialog();
        }

        private void btnDragDropMoving_Click(object sender, RoutedEventArgs e)
        {
            DragAndDropWithMoving form = new DragAndDropWithMoving();
            form.ShowDialog();
        }

        private void btnImageResizing_Click(object sender, RoutedEventArgs e)
        {
            ImageResizing form = new ImageResizing();
            form.ShowDialog();
        }
    }
}
