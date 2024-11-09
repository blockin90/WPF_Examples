using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUD.View
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void AutorizClick(object sender, RoutedEventArgs e)
        {
            var user = App.DataModel.Users.FirstOrDefault(u => u.Login == tbLogin.Text.Trim() && u.Password == tbPassword.Password);
            if( user != null) {
                App.CurrentUser = user;
                MainWindow window = new MainWindow();
                this.Close();
                window.ShowDialog();
            } else {
                MessageBox.Show("эээ...ну нет!", "Нейтральное сообщение");
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
