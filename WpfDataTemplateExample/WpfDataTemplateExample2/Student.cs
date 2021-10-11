using System;

namespace WpfDataTemplateExample2
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// Фотография
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// Полный путь к фотографии
        /// </summary>
        public string PhotoPath
        {
            get
            {
                //берем путь к исполяемому файлу, добавлем к нему имя фотографии
                return Environment.CurrentDirectory + @"\" + Photo;
            }
        }
    }
}
