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
    /// Логика взаимодействия для ClientEditWindow.xaml
    /// </summary>
    public partial class ClientEditWindow : Window, IEditWindow
    {
        public DbEntity Entity { get => ViewModel.Client; }
        public ClientEdit_VM ViewModel { get; set; }

        public ClientEditWindow()
        {
            InitializeComponent();
        }

        public ClientEditWindow(DbEntity entity, (string Label, string ButtonLabel) labels)
        {
            InitializeComponent();
            ViewModel = new ClientEdit_VM(entity as Client);

            Accept.Content = labels.ButtonLabel;
            MainLabel.Text = labels.Label + MainLabel.Text;
            DataContext = ViewModel;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDate(ViewModel.Client.DateOfAdding))


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
