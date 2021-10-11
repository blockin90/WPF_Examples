using EmployeePhoto.Model;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace EmployeePhoto.Forms
{
    /// <summary>
    /// Окно для демонстрации отображения списка сотрудников (класс Employee)
    /// </summary>
    public partial class EmployeesList : Window
    {

        public EmployeesList()
        {
            InitializeComponent();
        }
        /* добавляем к определению обработчика события Loaded, которое возникает, когда форма загружена
         * модификатор async, чтобы сделать возможным выполнение асинхронной загрузки данных 
         * с использованием оператора await*/
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ставим сообщение о начале загрузки:
            tbStatusMessage.Text = "Загрузка сотрудников...";
            /* т.к. загрузка картинок может оказаться длительной затратное, а при первом 
             * подключении, это еще и инициализация БД, выносим это в отдельный поток.
             * Для этого используем Task.Run, где указываем производимые действия (загрузка данных)
             * и оператор await перед вызовом. */
            dgEmpl.ItemsSource = await Task.Run(() =>
            {
               App.Database.Employees.Load();
               return App.Database.Employees.Local;
            });
            //когда данные загружены, убираем сообщение о загрузке:
            tbStatusMessage.Text = "";
            //делаем доступными кнопки:
            btnCreate.IsEnabled = true;
            btnEdit.IsEnabled = true;
        }
        /// <summary>
        /// обработчик нажатия кнопки "Создать" - добавление нового сотрудника
        /// </summary>
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            //создаем пустую запись для нового сотрудника
            Employee empl = new Employee();
            //и передаем ее на создаваемую форму (в классе формы определен конструктор, принимающий объект Employee):
            EmployeeDetail form = new EmployeeDetail(empl);
            //если вызов диалога завершен успешно...
            if (form.ShowDialog() == true) {
                //..то добавляем запись в базу данных и сохраняем:
                App.Database.Employees.Add(empl);
                App.Database.SaveChanges();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Редактировать" - редактирование выбранного сотрудника
        /// </summary>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //берем выбранную запись из датагрида и преобразуем в Employee(сотрудника): 
            var empl = dgEmpl.SelectedItem as Employee;

            if (empl == null) {
                //если сотрудник не выбран, информируем об этом пользователя
                MessageBox.Show("Сначала выберите сотрудника!");
            } else {
                /* иначе снимаем полную копию сотрудника для редактирования, чтобы не повредить исходные
                 * данные, если пользователь нажмету "отмену"
                 * Данный код можно сократить, если использовать "поверхностное/неглубокое копирование",
                 * подробнее см. https://msdn.microsoft.com/ru-ru/library/system.object.memberwiseclone(v=vs.110).aspx */
                var emplCopy = new Employee
                {
                    Id = empl.Id,
                    FullName = empl.FullName,
                    Photo = empl.Photo,
                    Position = empl.Position,
                    PositionId = empl.PositionId
                };
                //создаем форму редактирования сотрудника:
                EmployeeDetail form = new EmployeeDetail(emplCopy);
                //показываем форму и анализируем результат 
                if (form.ShowDialog() == true) {
                    //если вызов диалога завершен успешно, заменяем в БД запись:
                    empl.FullName = emplCopy.FullName;
                    empl.Photo = emplCopy.Photo;
                    empl.Position = emplCopy.Position;
                    empl.PositionId = emplCopy.PositionId;
                    //помечаем запись как модифицированную:
                    App.Database.Entry(empl).State = EntityState.Modified;
                    App.Database.SaveChanges();
                    //обновляем список, чтобы отредактированные данные отразились на форме
                    //в качестве альтернативые можно было реализовать INotifyPropertyChanged 
                    //для тех полей, чьи изменения необходимо отобразить
                    var collectionView = CollectionViewSource.GetDefaultView(App.Database.Employees.Local);
                    collectionView?.Refresh();
                }
            }
        }

    }
}
