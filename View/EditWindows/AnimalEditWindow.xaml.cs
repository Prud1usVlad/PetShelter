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
    /// Логика взаимодействия для AnimalEditWindow.xaml
    /// </summary>
    public partial class AnimalEditWindow : Window, IEditWindow
    {
        public DbEntity Entity { get => ViewModel.Animal; }
        public AnimalEdit_VM ViewModel { get; set; }
        public List<ComboBoxItem> RoomsItems { get; set; }

        public AnimalEditWindow()
        {
            InitializeComponent();
        }

        public AnimalEditWindow(DbEntity entity)
        {
            InitializeComponent();
            ViewModel = new AnimalEdit_VM(entity as Animal);

            if (ViewModel.Animal.AnimalID == 0)
            {
                AddStateValue.IsEnabled = false;
            }
            DataContext = ViewModel;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            var animal = Entity as Animal;
            var r = RoomsComboBox.SelectedItem as Room;
            var s = SexCheckbox.SelectedItem as ComboBoxItem;
            var k = KindCombo.SelectedItem as ComboBoxItem;

            int room = r == null ? 300001 : r.RoomID;
            string sex = s == null ? "Не вказано" : s.Content.ToString();
            string kind = k == null ? "Не вказано" : k.Content.ToString();

            if (((kind == "Кіт" || kind == "Собака") && room != 300001) || (kind == "Птах" && room != 300002) || (kind == "Плазун" && room != 300003))
            {
                MessageBox.Show("Даний вид тварин не може перебувати в обраній кімнаті, оберіть кімнату яка б підійшла тварині", "Не правильно обрана кімната");
                return;
            }

            animal.Sex = sex;
            animal.RoomID = room;
            animal.AnimalKind = kind;
            

            ViewModel.SaveStateValues.Execute(null);

            DialogResult = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
