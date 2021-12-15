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
using PetShelter.ViewModel;
using PetShelter.Model;

namespace PetShelter.View.FiltreWindows
{
    /// <summary>
    /// Логика взаимодействия для StateValue_filtre.xaml
    /// </summary>
    public partial class StateValue_filtre : Window
    {
        public Main_VM ViewModel { get; set; }
        public List<List<string>> SelectedValues { get; set; }

        public StateValue_filtre(Main_VM vm)
        {
            InitializeComponent();
            ViewModel = vm;
            FillStackPannels();
        }

        public void FillStackPannels()
        {
            foreach (State st in ViewModel.States)
            {
                var temp = new CheckBox();
                temp.Content = st.Name;
                temp.Margin = new Thickness(20, 20, 0, 0);
                StackPanel1.Children.Add(temp);
            }

            foreach (Animal animal in ViewModel.Animals)
            {
                var temp = new CheckBox();
                temp.Content = animal.Name;
                temp.Margin = new Thickness(20, 20, 0, 0);
                StackPanel2.Children.Add(temp);
            }

        }

        private void FiltreButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedValues = new List<List<string>>();
            for (int i = 0; i < 2; i++)
            {
                var res = new List<string>();
                UIElementCollection boxes = i == 0 ? StackPanel1.Children : StackPanel2.Children;

                foreach (CheckBox ch in boxes)
                {
                    if (ch.IsChecked == true)
                        res.Add(ch.Content.ToString());
                }

                SelectedValues.Add(res);
            }

            DialogResult = true;
        }
    }
}
