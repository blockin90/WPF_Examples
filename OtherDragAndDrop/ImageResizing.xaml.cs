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
    /// Логика взаимодействия для ImageResizing.xaml
    /// </summary>
    public partial class ImageResizing : Window
    {
        enum ResizeDirection { None, HorizontalToLeft, HorizontalToRight, Vertical }
        ResizeDirection direction;
        public ImageResizing()
        {
            InitializeComponent();
        }
        Point lastCoord;
        private void RightGridSplitter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            direction = ResizeDirection.HorizontalToRight;
            lastCoord = e.GetPosition(canvas);
            e.Handled = true;
        }
        private void LeftGridSplitter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            direction = ResizeDirection.HorizontalToLeft;
            lastCoord = e.GetPosition(canvas);
            e.Handled = true;
        }
        private void GridSplitter_MouseUp(object sender, MouseButtonEventArgs e)
        {
            direction = ResizeDirection.None;
            e.Handled = true;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var img = grid.Children.OfType<Image>().FirstOrDefault();
                if (img != null)
                {
                    switch (direction)
                    {
                        case ResizeDirection.HorizontalToRight:
                            img.Width += e.GetPosition(canvas).X - lastCoord.X;
                            break;
                        case ResizeDirection.HorizontalToLeft:
                            double leftCoord = Canvas.GetLeft(grid);
                            Canvas.SetLeft(grid, Canvas.GetLeft(grid) + e.GetPosition(canvas).X - lastCoord.X);
                            img.Width -= e.GetPosition(canvas).X - lastCoord.X;
                            break;
                    }

                }
            }
            lastCoord = e.GetPosition(canvas);
        }
    }
}
