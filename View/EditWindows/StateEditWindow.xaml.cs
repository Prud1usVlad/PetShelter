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
    /// Логика взаимодействия для StateEditWindow.xaml
    /// </summary>
    public partial class StateEditWindow : Window, IEditWindow
    {
        public StateValue_VM ViewModel { get; set; }
        public DbEntity Entity { get => State ; }

        public State State { get; set; }

        public StateEditWindow()
        {
            InitializeComponent();
        }

        public StateEditWindow(DbEntity state, (string Label, string ButtonLabel) labels )
        {
            State = state as State;
            InitializeComponent();

            Accept.Content = labels.ButtonLabel;
            MainLabel.Text = labels.Label + MainLabel.Text;

            DataContext = this;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            var entity = Entity as State;

            if (entity.Name == null || entity.Name == "")
            {
                MessageBox.Show("Перед підтвердженням впишіть назву стану", "Не повноцінні дані");
            }
            else
            {
                entity.Name = NameInput.Text;
            }


            DialogResult = true;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
