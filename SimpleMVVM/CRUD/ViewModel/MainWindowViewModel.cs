using CRUD.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            if ( App.CurrentUser == null) {
                throw new Exception("Не задан активный пользователь. Продолжение работы невозможно");
            }
            //выбираем все объекты, к которым имеет доступ текущий пользователь
            userRights = App.DataModel.CRUDs.Where(c => c.UserId == App.CurrentUser.Id).ToList();//и сохраняем их

            StudenListCommand = new RelayCommand(
                (param) => FrameContent = new StudentList(),
                (param)=> userRights.FirstOrDefault( c=>c.UniversityComponent.Name == "StudentsList") != null);
            StudentNotesCommand = new RelayCommand(
                (param) => FrameContent = new StudentNotes(),
                (param) => userRights.FirstOrDefault(c => c.UniversityComponent.Name == "StudentsNotes") != null
            );
        }


        public RelayCommand StudenListCommand { get; private set; }
        public RelayCommand StudentNotesCommand { get; private set; }

        List<CRUD.Model.CRUD> userRights;

        object _frameContent;
        public object FrameContent
        {
            get
            {
                return _frameContent;
            }
            set
            {
                _frameContent = value;
                OnPropertyChanged();
            }
        }
        #region CRUD

        #endregion
        #region INotifyPropertyChanged
        public void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
