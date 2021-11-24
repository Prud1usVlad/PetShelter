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
            DataContext = ViewModel;
        }

        public void SetRoomItems() 
        {
            var res = new List<ComboBoxItem>();

            foreach (Room room in ViewModel.Rooms)
            {
                var textBlock = new ComboBoxItem();
                textBlock.Content = room.Name;

                res.Add(textBlock);
            }

            RoomsItems = res;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            var animal = Entity as Animal;
            var r = RoomsComboBox.SelectedItem as Room;
            var s = SexCheckbox.SelectedItem as ComboBoxItem;
            animal.RoomID = r == null ? 300001 : r.RoomID;
            animal.Sex = s == null ? "Не вибрано" : s.Content.ToString();

            DialogResult = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
