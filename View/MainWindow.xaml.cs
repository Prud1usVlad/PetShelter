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
using PetShelter.Model;
using PetShelter.ViewModel;
using PetShelter.ViewModel.Settings;
using System.Data.Entity;
using System.Collections;
using System.Reflection;

namespace PetShelter.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Main_VM ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new Main_VM();
            DataContext = ViewModel;
        }

        public void ShowDetails()
        {
            DetailsPanel.Children.Clear();
            Dictionary<string, Dictionary<string, string>> details = ViewModel.ChosenItemDetails;

            foreach (string key in details.Keys)
            {
                var exp = new Expander();
                exp.Header = key;
                if (DetailsPanel.Children.Count == 0)
                {
                    exp.IsExpanded = true;
                }

                var text = new TextBlock();
                text.FontSize = 14;
                text.TextWrapping = TextWrapping.Wrap;

                foreach (string innerKey in details[key].Keys)
                {
                    text.Text += innerKey + ":  " + details[key][innerKey] + "\n";
                }

                exp.Content = text;
                DetailsPanel.Children.Add(exp);
            }
        }

        private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ShowDetailsCommand.Execute(MainGrid.SelectedItem);

            ShowDetails();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string header = (sender as MenuItem).Header.ToString();

            switch (header)
            {
                case "Тварини":
                    ViewModel.ItemSource = ViewModel.Animals;
                    break;
                case "Кімнати":
                    ViewModel.ItemSource = ViewModel.Rooms;
                    break;
                case "Стани":
                    ViewModel.ItemSource = ViewModel.States;
                    break;
                case "Стани тварин":
                    ViewModel.ItemSource = ViewModel.StateValues;
                    break;
                case "Групи":
                    ViewModel.ItemSource = ViewModel.Groups;
                    break;
            }

            if (header == "Тварини" || header == "Стани тварин")
            {
                AddButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                EditButton.IsEnabled = true;
                FiltreButton.IsEnabled = true;
            } else
            {
                AddButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                EditButton.IsEnabled = false;
                FiltreButton.IsEnabled = false;
            }

        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            if (SortCriteria.SelectedItem == null)
            {
                MessageBox.Show("Для сортування оберіть критерій сортування", "Неповнота данних");
                return;
            }

            ViewModel.SortCommand.Execute(new SortSettings(SortCriteria.SelectedItem.ToString(),
                SortCriteria.SelectedIndex , SortTypeCheckBox.IsChecked));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SearchCommand.Execute(SearchInput.Text);
        }

        private void FiltreButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FiltreCommand.Execute(null);

        }
    }
}
