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
    /// Логика взаимодействия для ImageMoving.xaml
    /// </summary>
    public partial class ImageMoving : Window
    {
        /// <summary>
        /// Последние координаты изображения (левый верхний угол - Left, Top)
        /// </summary>
        Point lastImageCoord;
        /// <summary>
        /// Разница между позицией мышки и координатам левого верхнего угла
        /// Нужна, т.к. для перемещения картинки мы меняем Left и Top нашего элементы, а в распоряжении 
        /// имеем только координаты мыши, нужно вносить поправку
        /// Можно обойтись и без нее, в MouseMove первая строка без дельты, вторая с дельтой, эффект один
        /// </summary>
        Point deltaCoord;

        public ImageMoving()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При движении мышки при зажатой левой кнопке изменяем координаты элементы согласно координатам перемещения 
        /// </summary>
        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Canvas.SetLeft(sender as Image, Canvas.GetLeft(sender as Image) + e.GetPosition(canvas).X - lastImageCoord.X);
                Canvas.SetTop(sender as Image, lastImageCoord.Y - deltaCoord.Y);
            }
            lastImageCoord = e.GetPosition(canvas as IInputElement);
        }
        
        /// <summary>
        /// При первом клике,до начала перемещения, сохраняем разницу между координатами мышки и левого верхнего угла
        /// </summary>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            deltaCoord = e.GetPosition(sender as IInputElement);
        }
    }
}
