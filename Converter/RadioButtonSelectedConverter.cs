using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Converter
{

    /// <summary>
    /// Класс для демонстрации биндинга свойство IsChecked элемента RadioButton к bool свойству объекта класса.
    /// В разметке MainWindow биндинг идет к свойству IsOptionSelected, объявленному в code-behind xaml файла. 
    /// Как IsChecked, так и IsOptionSelected имеют тип bool.
    /// Т.к. bool переменная может иметь 2 состояния, RadioButton'ов должно быть ровно 2 штуки, и мы должны отличать их между собой, 
    /// имея биндинг к одному полю из двух контроллов. 
    /// Для этого вводится св-во ConverterParameter, значение которого у них будет отличаться (у одного true, у другого false). 
    /// Логика такая: если поле класса = true, то активным становится RadioButton с ConverterParameter=true, иначе RadioButton с ConverterParameter=false
    /// Значение ConverterParameter объявлено в разметке, и в конвертер приходит строкой.
    /// </summary>
    public class RadioButtonSelectedConverter : IValueConverter
    {
        /// <summary>
        /// Метод, используемый для конвертации поля объекта привязки к заданному свойству элемента управления
        /// Т.е. из значения свойства IsOptionSelected и значения ConverterParameter в разметке соответствующего RadioButton 
        /// здесь должно быть принято решение - будет ли активным данный элемент (т.е. возвращаем true или false)
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //чтобы не заниматься преобразованием типов, делаем сравнение значений как строк с игнорирование регистра (т.е. "False" и "false" будут равны)
            var strValue = value.ToString();
            var strParam = parameter.ToString();
            return strValue.Equals(strParam, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Метод, используемый для конвертации значения привязанного свойства элемента управления в значение поля объекта привязки
        /// Т.е. из значения RadioButton.IsSelected получаем значение поля IsOptionSelected объекта класса
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //value содержит значения RadioButton.IsSelected элемента, для которого выполняется конвертация
            //если он не выделен, то ничего не делаем
            //если выделен, конвертируем значение привязанного параметра в boolean
            return value.Equals(true) ? System.Convert.ToBoolean(parameter) : Binding.DoNothing;
        }
    }
}
