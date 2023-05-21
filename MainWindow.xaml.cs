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

namespace Ex3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        АгентыEntities db = АгентыEntities.GetContext();
        //авторизация
        private void enter_Click(object sender, RoutedEventArgs e)
        {
            var user = from p in db.Users where p.Login == login.Text 
                       && p.Password == password.Password
                       select p;
            if (user.Count() == 1)
            {
                Window1 f = new Window1();
                f.ShowDialog();
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
