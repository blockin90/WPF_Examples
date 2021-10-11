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
    /// Логика взаимодействия для DragAndDrop.xaml
    /// </summary>
    public partial class DragAndDrop : Window
    {
        public DragAndDrop()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Возникает при движении мышки над картинкой в панели картинок
        /// Если во время движения зажата левая кнопка мыши, начинаем процедуру Drag and drop
        /// Для завршения дрэг энд дрова в контейнере приемнике должно быть установлено свойство AllowDrop, а также
        /// обработано событие Drop
        /// </summary>
        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //первый параметр - объект, инициировавший событие, второй - данные для перетаскивания (полезная нагрузка)
                DragDrop.DoDragDrop(sender as DependencyObject, ((Image)e.OriginalSource), DragDropEffects.Move);
            }
        }
        /// <summary>
        /// Обработка завершения операции DaD, генерация нового элемента на основе переданных данных
        /// </summary>
        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            //контейнер может принимать разные элементы управления -> разные форматы кодирования этих данных
            //допустимые форматы хранятся в e.Data.GetFormats()
            //зная допустимый формат, с помощью e.Data.GetData( имя_формата ) можно получить ссылку на объект 
            //но мы, например, не помним, как кодируется картинка..поэтому перебираем все форматы, и пробуем получить
            //изображение из каждого из них, как только удалось - profit!
            foreach(var format in e.Data.GetFormats()) { 

                var element = e.Data.GetData( format ) as Image;
                if (element != null)
                {
                    Image img = new Image();
                    img.Height = element.Height;
                    img.Width = element.Width;
                    img.Source = element.Source;        //копируем в новую картинку исходник старой
                    //добавляем на канвас и позиционируем нашу картинку:
                    (sender as Canvas).Children.Add(img);       
                    Canvas.SetLeft(img, e.GetPosition(sender as IInputElement).X);
                    Canvas.SetTop(img, e.GetPosition(sender as IInputElement).Y);
                    break;  //не забываем прервать цикл, т.к. элемент уже добавлен!
                }
            }
        }
    }
}
