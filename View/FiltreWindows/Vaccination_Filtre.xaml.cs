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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PetShelter.View.FiltreWindows
{
    /// <summary>
    /// Логика взаимодействия для Vaccination_Filtre.xaml
    /// </summary>
    public partial class Vaccination_Filtre : Window
    {
        private DateTime to;
        private DateTime from;

        public DateTime FromDate
        {
            get { return from; }
            set
            {
                from = value;
                OnPropertyChanged("FromDate");
            }
        }
        public DateTime ToDate
        {
            get { return to; }
            set
            {
                to = value;
                OnPropertyChanged("ToDate");
            }
        }
        public Main_VM ViewModel { get; set; }
        public List<List<string>> SelectedValues { get; set; }

        public Vaccination_Filtre(Main_VM vm)
        {
            InitializeComponent();
            ViewModel = vm;
            FillStackPannels();

            ToDate = DateTime.Now.Date;
            FromDate = new DateTime(2010, 1, 1);

            DataContext = this;
        }

        public void FillStackPannels()
        {
            foreach (Vaccine item in ViewModel.Vaccines)
            {
                var temp = new CheckBox();
                temp.Content = item.VaccineName;
                temp.Margin = new Thickness(20, 20, 0, 0);
                StackPanel1.Children.Add(temp);
            }

            foreach (Animal item in ViewModel.Animals)
            {
                var temp = new CheckBox();
                temp.Content = item.AnimalID + " " + item.Name;
                temp.Margin = new Thickness(20, 20, 0, 0);
                StackPanel2.Children.Add(temp);
            }
        }

        private void FiltreButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckDate())
            {
                return;
            }

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private bool CheckDate()
        {
            var d = new DateTime(2000, 1, 1);

            if (ToDate < d || ToDate > DateTime.Now
                || FromDate < d || FromDate > DateTime.Now)
            {
                MessageBox.Show("Введені дати виходять за рамки дозволених, перевірте правильність введення.", "Помилка введення дати");
                return false;
            }

            return true;
        }
    }
}