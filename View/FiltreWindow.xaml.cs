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
using System.Windows.Shapes;
using PetShelter.Model;
using PetShelter.ViewModel;
using System.Reflection;

namespace PetShelter.View
{
    /// <summary>
    /// Логика взаимодействия для FiltreWindow.xaml
    /// </summary>
    public partial class FiltreWindow : Window
    {
        public IEnumerable<DbEntity> Entities { get; set; }
        public Dictionary<string, List<string>> ChosenCheckBoxes { get; set; }

        private Dictionary<string, List<string>> data;


        public FiltreWindow(IEnumerable<DbEntity> entities)
        {
            InitializeComponent();
            Entities = entities;
            InitializeVisualElements();
        }

        private void InitializeVisualElements()
        {
            List<string> filtrableProps = Entities.First().GetFilterableProperties();

            PropertyInfo prop1 = Entities.First().GetType().GetProperty(filtrableProps[0]);
            PropertyInfo prop2 = Entities.First().GetType().GetProperty(filtrableProps[1]);
            PropertyInfo prop3 = Entities.First().GetType().GetProperty(filtrableProps[2]);

            data = new Dictionary<string, List<string>> 
            {
                { prop1 == null ? "Немає доступних" : prop1.Name, new List<string>() },
                { prop2 == null ? "Немає доступних" : prop2.Name, new List<string>() },
                { prop3 == null ? "Немає доступних" : prop3.Name, new List<string>() },
            };

            foreach (DbEntity entity in Entities)
            {
                fillDictionary(entity, prop1, data);
                fillDictionary(entity, prop2, data);
                fillDictionary(entity, prop3, data);
            }

            if(prop1 != null)
            {
                data[prop1.Name].Sort();

                foreach (string str in data[prop1.Name])
                {
                    var temp = new CheckBox();
                    temp.Content = str;
                    temp.Margin = new Thickness(20, 20, 0, 0);
                    StackPanel1.Children.Add(temp);
                }

                ColumnLabel1.Content = prop1.Name;
            }
                
            if (prop2 != null)
            {
                data[prop2.Name].Sort();

                foreach (string str in data[prop2.Name])
                {
                    var temp = new CheckBox();
                    temp.Content = str;
                    temp.Margin = new Thickness(20, 20, 0, 0);
                    StackPanel2.Children.Add(temp);
                }

                ColumnLabel2.Content = prop2.Name;
            }
                
            if (prop3 != null)
            {
                data[prop3.Name].Sort();

                foreach (string str in data[prop3.Name])
                {
                    var temp = new CheckBox();
                    temp.Content = str;
                    temp.Margin = new Thickness(20, 20, 0, 0);
                    StackPanel3.Children.Add(temp);
                }

                ColumnLabel3.Content = prop3.Name;
            }
        }

        private void fillDictionary(DbEntity ent, PropertyInfo prop, Dictionary<string, List<string>> data)
        {
            if (prop == null)
                return;

            if (prop.GetValue(ent) != null && !data[prop.Name].Contains(prop.GetValue(ent).ToString()))
            {
                data[prop.Name].Add(prop.GetValue(ent).ToString());
            }
        }

        private void FiltreButton_Click(object sender, RoutedEventArgs e)
        {
            ChosenCheckBoxes = new Dictionary<string, List<string>>
            {
                { data.Keys.ElementAt(0), new List<string>() },
                { data.Keys.ElementAt(1), new List<string>() },
                { data.Keys.ElementAt(2), new List<string>() },
            };

            foreach(CheckBox ch in StackPanel1.Children)
            {
                if (ch.IsChecked == true)
                    ChosenCheckBoxes[data.Keys.ElementAt(0)].Add(ch.Content.ToString());
            }

            foreach (CheckBox ch in StackPanel2.Children)
            {
                if (ch.IsChecked == true)
                    ChosenCheckBoxes[data.Keys.ElementAt(1)].Add(ch.Content.ToString());
            }

            foreach (CheckBox ch in StackPanel3.Children)
            {
                if (ch.IsChecked == true)
                    ChosenCheckBoxes[data.Keys.ElementAt(2)].Add(ch.Content.ToString());
            }

            DialogResult = true;
        }
    }
}
