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
    /// Логика взаимодействия для DragAndDropWithMoving.xaml
    /// </summary>
    public partial class DragAndDropWithMoving : Window
    {
        Point lastImageCoord;
        Point deltaCoord;

        public DragAndDropWithMoving()
        {
            InitializeComponent();
        }
        /// <summary>
        /// см. комменты из Drag and Drop'а
        /// </summary>
        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(sender as DependencyObject, ((Image)e.OriginalSource), DragDropEffects.Move);
            }
        }
        /// <summary>
        /// см. комменты из Drag and Drop'а
        /// </summary>
        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            foreach (var format in e.Data.GetFormats())
            {

                var element = e.Data.GetData(format) as Image;
                if (element != null)
                {
                    Image img = new Image();
                    img.Height = element.Height;
                    img.Width = element.Width;
                    img.Source = element.Source;
                    //--------------------------------------------------------
                    //Указываем обработчики нажатий мышки и перемещения мышки 
                    //Для картинки, добавляемой на канвас
                    //--------------------------------------------------------
                    img.MouseDown += CanvasImage_MouseDown;
                    img.MouseMove += CanvasImage_MouseMove;

                    (sender as Canvas).Children.Add(img);
                    Canvas.SetLeft(img, e.GetPosition(sender as IInputElement).X);
                    Canvas.SetTop(img, e.GetPosition(sender as IInputElement).Y);
                    break;  
                }
            }
        }

        /// <summary>
        /// см. комменты из ImageMoving 
        /// </summary>
        private void CanvasImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Canvas.SetLeft(sender as Image, lastImageCoord.X - deltaCoord.X);
                Canvas.SetTop(sender as Image, lastImageCoord.Y - deltaCoord.Y);
            }
            lastImageCoord = e.GetPosition(canvas as IInputElement);
        }

        /// <summary>
        /// см. комменты из ImageMoving 
        /// </summary>
        private void CanvasImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            deltaCoord = e.GetPosition(sender as IInputElement);
        }

    }
}
