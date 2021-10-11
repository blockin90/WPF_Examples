using System;

namespace BindingExample2
{
    public class Paper
    {
        public int Id { get; set; }
        /// <summary> Название статьи </summary>
        public string Title { get; set; }
        /// <summary> Название журнала, в котором была опубликована статья </summary>
        public string Magazine { get; set; }
        /// <summary> Объем статьи в печатных листах</summary>
        public double Volume { get; set; }
        /// <summary> Дата публикации </summary>
        public DateTime PublicDate { get; set; }
    }
}