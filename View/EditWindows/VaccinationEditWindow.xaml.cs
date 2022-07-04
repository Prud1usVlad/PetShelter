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
using PetShelter.ViewModel.EditWindowsVM;

namespace PetShelter.View.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для VaccinationEditWindow.xaml
    /// </summary>
    public partial class VaccinationEditWindow : Window, IEditWindow
    {
        public DbEntity Entity { get => ViewModel.Vaccination; }
        public VaccinationEdit_VM ViewModel { get; set; }

        public VaccinationEditWindow()
        {
            InitializeComponent();
        }

        public VaccinationEditWindow(DbEntity entity, (string Label, string ButtonLabel) labels)
        {
            ViewModel = new VaccinationEdit_VM(entity as Vaccination);
            InitializeComponent();

            Accept.Content = labels.ButtonLabel;
            MainLabel.Text = labels.Label + MainLabel.Text;

            DataContext = ViewModel;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckDate(ViewModel.Vaccination.VaccinationDate))
                return;

            var vaccination = Entity as Vaccination;
            var a = AnimalCombo.SelectedItem as Animal;
            var v = VaccineCombo.SelectedItem as Vaccine;

            int animal = a == null ? -1 : a.AnimalID;
            int vaccine = v == null ? -1 : v.VaccineID;

            if (animal == -1)
            {
                MessageBox.Show("Для продовження формування документу вам потрібно обрати тварину", "Не повні дані");
                return;
            }
            else if (vaccine == -1)
            {
                MessageBox.Show("Для продовження формування документу вам потрібно обрати вакцину", "Не повні дані");
                return;
            }

            vaccination.AnimalID = animal;
            vaccination.VaccineID = vaccine;

            DialogResult = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private bool CheckDate(DateTime? date)
        {
            if (date < new DateTime(2010, 1, 1) || date > DateTime.Now)
            {
                MessageBox.Show("Введені дати виходять за рамки дозволених, перевірте правильність введення.", "Помилка введення дати");
                return false;
            }

            return true;
        }
    }
}
