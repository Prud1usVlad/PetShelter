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
    /// Логика взаимодействия для ContractEditWindow.xaml
    /// </summary>
    public partial class ContractEditWindow : Window, IEditWindow
    {
        public DbEntity Entity { get => ViewModel.Contract; }
        public ContractEdit_VM ViewModel { get; set; }

        public ContractEditWindow()
        {
            InitializeComponent();
        }

        public ContractEditWindow(DbEntity entity, (string Label, string ButtonLabel) labels)
        {
            ViewModel = new ContractEdit_VM(entity as Contract);
            InitializeComponent();

            Accept.Content = labels.ButtonLabel;
            MainLabel.Text = labels.Label + MainLabel.Text;

            DataContext = ViewModel;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckDate(ViewModel.Contract.SigningDate) && !CheckDate(ViewModel.Contract.TerminationDate))
                return;

            var contract = Entity as Contract;
            var a = AnimalCombo.SelectedItem as Animal;
            var em = EmploeeCombo.SelectedItem as InfoDepEmploee;
            var c = ClientCombo.SelectedItem as Client;

            int animal = a == null ? -1 : a.AnimalID;
            int emploee = em == null ? -1 : em.PassNum;
            int client = c == null ? -1 : c.ClientID;

            if (animal == -1)
            {
                MessageBox.Show("Для продовження формування документу вам потрібно обрати тварину", "Не повні дані");
                return;
            }
            else if (emploee == -1)
            {
                MessageBox.Show("Для продовження формування документу вам потрібно обрати робітника", "Не повні дані");
                return;
            }
            else if (client == -1)
            {
                MessageBox.Show("Для продовження формування документу вам потрібно обрати клієнта", "Не повні дані");
                return;
            }

            contract.AnimalID = animal;
            contract.ClientID = client;
            contract.PassNum = emploee;
            contract.IDCardNum = int.Parse(c.IDCardNum);

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
