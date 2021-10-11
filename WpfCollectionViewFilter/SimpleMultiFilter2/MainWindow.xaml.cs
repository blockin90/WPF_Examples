using CollectionViewsExample.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace SimpleMultiFilter2
{
    /* В качестве функции фильтра для  CollectionView для студентов выступает метод StudentFilter. 
     * Данный метод по очереди вызывает все методы из списка filters, в котором хранятся фильтрующие функции.
     * Список функций описан в регионе "Функции фильтрации". Каждая функция, по сути, привязана к содержимому
     * какого-то элемента управления.
     * Набор функций в списке filters определяется значениями CheckBox'ов:
     * установка CheckBox'а добавляет соответствующий фильтр, снятие - удаляет (см. регион "установка,сброс фильтров").
     * 
     * Удаление или добавление нового фильтра вызывает перефильтрацию всей коллекции, т.о. содержимое DataGrid'а обновляется.
     * Кроме того, чтобы обеспечить динамическую фильтрацию, на элементы управления, отвечающие за фильтрацию установлены 
     * обработчики событий изменения их содержимого (для TextBox'ов это TextChanged, для ComboBox'а - SelectionChanged). 
     * См. метод ApplyFilters  */


    public partial class MainWindow : Window
    {
        /// <summary>
        /// Cписок применяемых фильтров
        /// </summary>
        /// <remarks>
        /// представлен типом ObservableCollection, т.к. данный тип позволяет отслеживать изменения коллекции - при каждом 
        /// изменении коллекции генерируется событие. Это нам поможет контролировать добавление/удаление фильтров,
        /// т.к. в этом случае необходимо обновлять содержимое DataGrid'а
        /// </remarks>
        private ObservableCollection<Predicate<Student>> filters = new ObservableCollection<Predicate<Student>>();
        public MainWindow()
        {
            InitializeComponent();

            //подписываемся на событие "коллекция изменена" для списка фильтров
            filters.CollectionChanged += Filters_CollectionChanged;
            
        }

        /// <summary>
        /// обработчик события загрузки окна
        /// </summary>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //грузим студентов
            var students = await App.DatabaseContext.Students.ToArrayAsync();
            var studentsView = CollectionViewSource.GetDefaultView(students);
            studentsView.Filter = StudentFilter;
            studentDataGrid.ItemsSource = studentsView;
            //создаем массив групп, пока с единственным элементом "все группы":
            var groups = new[] { new Group() { Name = "Все группы", Id = -1 } };
            //грузим список группы из БД:
            var dbGroups = await App.DatabaseContext.Groups.ToArrayAsync();
            //и объединяем оба массива
            groups = groups.Union(dbGroups).ToArray();
            //после чего устанавливаем его источником данных ComboBox'а:
            cbGroupsFilter.ItemsSource = groups;
        }

        /// <summary>
        /// обработчик события изменения коллекции
        /// </summary>
        private void Filters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //получаем представление данных:
            var studentsView = CollectionViewSource.GetDefaultView(studentDataGrid.ItemsSource);
            //и вызываем его обновление, чтобы учесть изменение набора фильтров
            studentsView.Refresh();
        }
        
        #region Функции фильтрации
        /// <summary>
        /// Функция фильтрации набора студентов
        /// </summary>
        /// <param name="o">элемент коллекции студентов</param>
        /// <returns>true, если студент принимается, иначе false</returns>
        private bool StudentFilter(object o)
        {
            var s = o as Student;
            //если хотя бы один из фильтров вернул false, элемент должен быть отброен, поэтому возвращаем false
            if (filters.Any(f => f(s) == false)) {
                return false;
            }
            return true;
        }

        private bool StudentFilter2(object o)
        {
            var s = o as Student;
            //перебираем список фильтров
            for(int i = 0; i < filters.Count; i++) {
                //если хотя бы один из них вернул false, элемент должен быть отброен, поэтому возвращаем false
                if (filters[i](s) == false) {
                    return false;
                }
            }
            return true;
        }

        private bool FilterByLastName(Student s)
        {
            return s.LastName.Contains(tbLastNameFilter.Text);
        }

        private bool FilterByFirstName(Student s)
        {
            return s.FirstName.Contains(tbFirstNameFilter.Text);
        }

        private bool FilterByGroup(Student s)
        {
            var group = (cbGroupsFilter.SelectedItem as Group);
            if (group == null || group.Id == -1) { //группа не выбрана
                return true;
            }
            return s.GroupId == group.Id;
        }
        #endregion

        #region установка,сброс фильтров
        private void EnableLastNameFilter_Checked(object sender, RoutedEventArgs e)
        {
            filters.Add(FilterByLastName);
        }

        private void EnableLastNameFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            filters.Remove(FilterByLastName);
        }

        private void EnableFirstNameFilter_Checked(object sender, RoutedEventArgs e)
        {
            filters.Add(FilterByFirstName);
        }

        private void EnableFirstNameFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            filters.Remove(FilterByFirstName);
        }

        private void EnableGroupFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            filters.Remove(FilterByGroup);
        }

        private void EnableGroupFilter_Checked(object sender, RoutedEventArgs e)
        {
            filters.Add(FilterByGroup);
        }
        #endregion

        /// <summary>
        /// Обработчик изменения содержимого контроллов, ответственных за фильтрацию
        /// </summary>
        private void ApplyFilters(object sender, RoutedEventArgs e)
        {
            var studentsView = CollectionViewSource.GetDefaultView(studentDataGrid.ItemsSource);
            studentsView.Refresh();
        }
    }
}
