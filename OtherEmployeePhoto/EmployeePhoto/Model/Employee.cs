
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePhoto.Model
{
    /// <summary>
    /// Сотрудник к которого в БД хранится только путь к изображению
    /// </summary>
    /// <remarks>
    /// Реализация INotifyPropertyChanged нужная для автоматического обновления фотографий, после их 
    /// изменения. Без реализации этого интерфейса, изменения отобразятся лишь при следующем запуске 
    /// программы.
    /// </remarks>
    public class Employee : INotifyPropertyChanged
    {
        public int Id { get; set; }
        /// <summary>Полное имя </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>Id должности </summary>
        /// Знак вопроса означает, что поле может принимать значения null
        public int ? PositionId { get; set; }
        public virtual Position Position { get; set; }

        /// <summary>Имя файла, содержащего изображение</summary>
        string _photo;
        /// <summary>Свойство, предоставляющее доступ к имени файла, содержащего изображение</summary>
        public string Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                //т.к. биндинг у нас идет к свойству PhotoPath, уведомляем всех подписчиков 
                //об изменении именно этого свойства: 
                OnPropertyChanged("PhotoPath");
            }
        }

        /// <summary>
        /// Полный путь к изображению
        /// </summary>
        /// <remarks>
        /// Для того, чтобы использовать Binding для изображений, нужно указывать полный путь,
        /// поэтому свойства Photo, объявленного выше, недостаточно.
        /// Это свойство реализует данную логику - на основе имени файла (Photo) составляется 
        /// полное местоположение картинки. В данном примере предполагается, что все фотографии
        /// загружаются в папку Photos, которая находится в той же директории, что и исполняемый файл
        /// </remarks>
        [NotMapped]
        public string PhotoPath
        {
            get
            {
                //Environment.CurrentDirectory - свойство, содержащее путь к папке исполняемого файла
                return Environment.CurrentDirectory + @"\Photos\" + Photo;
            }
        }
        #region Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Возбуждает событие, уведомляет об изменении значения свойства
        /// </summary>
        /// <param name="prop">имя свойства, которое изменилось</param>
        /// <remarks>
        /// CallerMemberName позволяет не передавать вручную имя свойства, вместо этого 
        /// значение подставляется автоматически
        /// </remarks>
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
