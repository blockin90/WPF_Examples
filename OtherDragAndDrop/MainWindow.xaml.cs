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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
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

        private void ListBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selected = (sender as ListBox).SelectedItem;

        }
        
        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(e.OriginalSource as DependencyObject, ((Image)e.OriginalSource), DragDropEffects.Move);
            }
        }
        Point lastImageCoord;
        Point deltaCoord;

        private void Image_Move(object sender, MouseEventArgs e)
        {
            
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Canvas.SetLeft(sender as Image, lastImageCoord.X - deltaCoord.X);
                Canvas.SetTop(sender as Image, lastImageCoord.Y - deltaCoord.Y);
                //DragDrop.DoDragDrop(e.OriginalSource as DependencyObject, ((Image)e.OriginalSource), DragDropEffects.Move);
            }
            lastImageCoord = e.GetPosition(canvas as IInputElement);
        }
        private void Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lastImageCoord = e.GetPosition(canvas as IInputElement);
            deltaCoord = lastImageCoord;
            deltaCoord.X -= Canvas.GetLeft(sender as Image);
            deltaCoord.Y -= Canvas.GetTop(sender as Image);
        }
        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            var formats = e.Data.GetFormats();
            var element = e.Data.GetData("System.Windows.Controls.Image") as Image;
            Image img = new Image();
            img.Height = element.Height;
            img.Width = element.Width;
            img.Source = element.Source;
            img.MouseMove += Border_MouseMove; //Image_Move;
            img.MouseLeftButtonDown += Border_MouseDown; //Img_MouseLeftButtonDown;
            if( new Random().Next(100) < 50)
            {
                Button btn = new Button();
                btn.Click += Btn_Click;
                btn.Click += Btn_Click1;
                btn.Content = img;
                
                (sender as Canvas).Children.Add(btn);
                Canvas.SetLeft(btn, e.GetPosition(sender as IInputElement).X);
                Canvas.SetTop(btn, e.GetPosition(sender as IInputElement).Y - 20);
            }
            else
            {
                (sender as Canvas).Children.Add(img);
                Canvas.SetLeft(img, e.GetPosition(sender as IInputElement).X);
                Canvas.SetTop(img, e.GetPosition(sender as IInputElement).Y - 20);


            }

        }

        private void Btn_Click1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello again!");
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("It's new Button and it's working! ");
        }
        Point mouseResizePosition;
        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if( e.LeftButton == MouseButtonState.Pressed)
            {
                Image i = (sender) as Image;
                i.Stretch = Stretch.Fill;
                Point currentMousePosition = e.GetPosition(wrapPanel); ;
                //Image i = b.Child as Image;
                i.Width += currentMousePosition.X - mouseResizePosition.X;
                i.Height += currentMousePosition.Y - mouseResizePosition.Y;

                mouseResizePosition = currentMousePosition;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseResizePosition = e.GetPosition(wrapPanel);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
