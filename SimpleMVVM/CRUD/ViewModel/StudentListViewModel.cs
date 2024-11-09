using CRUD.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CRUD.ViewModel
{
    class StudentListViewModel : INotifyPropertyChanged
    {

        public StudentListViewModel()
        {
            AvgRateValues = new List<string>() { "Все", "Троечники", "Хорошисты", "Отличники" };
            Groups = new List<Group>() { new Group() { Name = "Все группы", Id = -1 } };
            SelectedGroup = Groups[0];
            Groups.AddRange(App.DataModel.Groups);
            //определяем уровень прав пользователя на операции добавления, удаления:
            /*var result = App.DataModel.CRUDs.FirstOrDefault(c => c.UserId == App.CurrentUser.Id && c.UniversityComponent.Name == "StudentsList");
            if( result != null) {
                AddVisibility = result.CanCreate ? Visibility.Visible : Visibility.Collapsed;
                DeleteVisibility = result.CanDelete ? Visibility.Visible : Visibility.Collapsed;
            } else {
                AddVisibility = DeleteVisibility = Visibility.Collapsed;
            }*/
        }
        #region CRUD
        public Visibility AddVisibility { get; private set; }
        public Visibility DeleteVisibility { get; private set; }
        #endregion
        #region Списочный состав студентов
        List<Student> _students;
        public List<Student> Students
        {
            get {
                return _students;
            }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }
        Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }
        public List<Group> Groups
        {
            get; set;
        }
        public Group _selectedGroup;
        public Group SelectedGroup
        {
            get
            {
                return _selectedGroup;
            }
            set
            {
                _selectedGroup = value;
                ApplyFilters();
                OnPropertyChanged();

            }
        }
        #endregion
        #region Рассчетные свойства
        public List<string> AvgRateValues { get; set; }
        double _groupAvgRate;
        public double GroupAvgRate
        {
            get { return _groupAvgRate; }
            set
            {
                _groupAvgRate = value;
                OnPropertyChanged();
            }
        }
        int _studentsCount;
        public int StudentsCount
        {
            get { return _studentsCount; }
            set
            {
                _studentsCount = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Фильтры

        string _filterName = string.Empty;
        public String FilterName
        {
            get
            {
                return _filterName;
            }
            set
            {
                _filterName = value.Trim();
                ApplyFilters();
                OnPropertyChanged();
            }
        }
        int _filterAvgRate;
        public int FilterAvgRate
        {
            get { return _filterAvgRate; }
            set {
                _filterAvgRate = value;
                ApplyFilters();
                OnPropertyChanged();
            }
        }
        bool FilterByAvgRate(Student student)
        {
            if (FilterAvgRate == 0)
                return true;
            else if (FilterAvgRate == 1)
                return student.AvgRate < 3.5;
            else if (FilterAvgRate == 2)
                return student.AvgRate >= 3.5 && student.AvgRate < 4.5;
            else
                return student.AvgRate >= 4.5;
        }
        void ApplyFilters()
        {
            var result = App.DataModel.Students.Where(
                student =>
                    (SelectedGroup.Id == -1 ? true : student.Group.Id == SelectedGroup.Id) &&
                    (student.LastName.StartsWith(FilterName))
                ).Where(FilterByAvgRate);
            Students = new List<Student>(result);
            
            StudentsCount = Students.Count;
            GroupAvgRate = StudentsCount > 0 ? Students.Average(s => s.AvgRate) : 0;
        }

        #endregion
        #region INotifyPropertyChanged
        public void OnPropertyChanged( [CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
