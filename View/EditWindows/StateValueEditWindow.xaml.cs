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
    /// Логика взаимодействия для StateValueEditWindow.xaml
    /// </summary>
    public partial class StateValueEditWindow : Window, IEditWindow
    {
        public DbEntity Entity { get => ViewModel.StateValue; }
        public StateValue_VM ViewModel { get; set; }

        public StateValueEditWindow()
        {
            InitializeComponent();
        }

        public StateValueEditWindow(DbEntity stateValue)
        {
            InitializeComponent();
            ViewModel = new StateValue_VM(stateValue as StateValue);

            DataContext = ViewModel;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            var entity = Entity as StateValue;
            var animal = AnimalsComboBox.SelectedItem as Animal;
            var state = StateCheckbox.SelectedItem as State;

            if (animal == null)
            {
                MessageBox.Show("Перед підтвердженням виберіть тварину зі списку", "Не повноцінні дані");
            }
            else if (state == null)
            {
                MessageBox.Show("Перед підтвердженням виберіть стан зі списку", "Не повноцінні дані");
            }
            else
            {
                entity.StateID = state.StateID;
                entity.AnimalID = animal.AnimalID;
            }


            DialogResult = true;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
