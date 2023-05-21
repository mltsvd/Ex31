using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace Ex3
{
    /// <summary>
    /// Логика взаимодействия для AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Window
    {
        АгентыEntities db = new АгентыEntities();
        public AddEdit()
        {
            InitializeComponent();
            db.Agents.Load();
            db.AgentTypes.Load();
            type.ItemsSource = db.AgentTypes.Local.ToList();
        }
        Agent p1;
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Agent p1 = new Agent();
            p1.ID = Class1.id;
            p1.Title = title.Text;
            p1.Priority = Convert.ToInt32(priority.Text);
            p1.AgentTypeID = db.AgentTypes.Where(p => p.Title == type.Text).FirstOrDefault().ID;
            p1.Phone = phone.Text;
            db.Agents.Add(p1);
            db.SaveChanges();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            p1.ID = Class1.id;
            p1.Title = title.Text;
            p1.Priority = Convert.ToInt32(priority.Text);
            p1.AgentTypeID = db.AgentTypes.Where(p => p.Title == type.Text).FirstOrDefault().ID;
            p1.Phone = phone.Text;
            db.SaveChanges();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Class1.click == "Edit")
            {
                p1 = db.Agents.Find(Class1.id);
                title.Text = p1.Title;
                priority.Text =  p1.Priority.ToString();
                phone.Text = p1.Phone;
            }
        }
    }
}
