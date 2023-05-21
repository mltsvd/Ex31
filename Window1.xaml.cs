using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ex3
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        АгентыEntities db = new АгентыEntities();
        public Window1()
        {
            InitializeComponent();
            db.Agents.Load();
            db.AgentTypes.Load();
            lv.ItemsSource = db.Agents.Local.ToList();
            filt.ItemsSource = db.AgentTypes.Local.ToList();
        }

        private void sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lv!=null) lv.Items.SortDescriptions.Clear();
            if (sort.SelectedIndex == 1)
                //сортировка
                lv.Items.SortDescriptions.Add(new SortDescription("Priority", ListSortDirection.Descending));
            if (sort.SelectedIndex == 2)
                lv.Items.SortDescriptions.Add(new SortDescription("Priority", ListSortDirection.Ascending));
        }

        private void filt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //фильтр
            lv.ItemsSource = db.Agents.ToList().Where(p=>p.AgentTypeID == ((AgentType)filt.SelectedItem).ID).ToList();
        }

        private void find_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lv != null)
            {
                if (find.Text == "Введите для поиска") find.Text = "";
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    var row = (Agent)lv.Items[i];
                    if (row.Title.Contains(find.Text) || row.Phone.Contains(find.Text) || row.Email.Contains(find.Text))
                    {
                        object item = lv.Items[i];
                        lv.SelectedValue = item;
                        lv.ScrollIntoView(item);
                    }
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Class1.id = db.Agents.Max(p=>p.ID)+1;
            AddEdit f = new AddEdit();
            f.ShowDialog();
            lv.Items.Refresh();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (lv.SelectedItems.Count != 0)
            {
                int index = lv.SelectedIndex;
                var row = (Agent)lv.Items[index];   
                Class1.id = row.ID;
                Class1.click = "Edit";
                AddEdit f = new AddEdit();
                f.ShowDialog();
                lv.Items.Refresh();
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы хотите удалить запись?","Удаление",MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var row = (Agent)lv.Items[lv.SelectedIndex];
                db.Agents.Remove(row);
                db.SaveChanges();
                lv.Items.Refresh();
                db.Agents.Load();
                db.AgentTypes.Load();
                lv.ItemsSource = db.Agents.Local.ToList();
            }
        }
    }
}
