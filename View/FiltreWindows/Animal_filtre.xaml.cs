﻿using System;
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
    /// Логика взаимодействия для Animal_filtre.xaml
    /// </summary>
    public partial class Animal_filtre : Window
    {
        public Main_VM ViewModel { get; set; }
        public List<List<string>> SelectedValues { get; set; }

        public Animal_filtre(Main_VM vm)
        {
            InitializeComponent();
            ViewModel = vm;
            FillStackPannels();
        }

        public void FillStackPannels()
        {
            IEnumerable<string> kinds = ViewModel.Animals.GroupBy(a => a.AnimalKind).Select(a => a.First().AnimalKind);

            foreach (string str in kinds)
            {
                var temp = new CheckBox();
                temp.Content = str;
                temp.Margin = new Thickness(20, 20, 0, 0);
                StackPanel1.Children.Add(temp);
            }

            foreach (Group group in ViewModel.Groups)
            {
                var temp = new CheckBox();
                temp.Content = group.Description;
                temp.Margin = new Thickness(20, 20, 0, 0);
                StackPanel2.Children.Add(temp);
            }

            foreach (Room room in ViewModel.Rooms)
            {
                var temp = new CheckBox();
                temp.Content = room.Name;
                temp.Margin = new Thickness(20, 20, 0, 0);
                StackPanel3.Children.Add(temp);
            }


        }

        private void FiltreButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedValues = new List<List<string>>();
            for (int i = 0; i < 3; i++)
            {
                var res = new List<string>();
                UIElementCollection boxes = i == 0 ? StackPanel1.Children : i == 1 ? StackPanel2.Children : StackPanel3.Children;

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
