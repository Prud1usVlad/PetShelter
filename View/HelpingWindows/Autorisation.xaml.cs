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

namespace PetShelter.View.HelpingWindows
{
    /// <summary>
    /// Логика взаимодействия для Autorisation.xaml
    /// </summary>
    public partial class Autorisation : Window
    {
        public IEnumerable<User> Users { get; set; }
        public User AuthorisedUser { get; set; }

        public Autorisation(IEnumerable<User> users)
        {
            InitializeComponent();
            Users = users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string pass = PasswordInput.Text;
            string log = LoginInput.Text;

            IEnumerable<User> us = Users.Where(u => u.Login == log);

            if (us.Count() == 0)
            {
                MessageBox.Show("Користувача з вказаним логіном не існує. Перевірте введення", "Помилка авторизації");
            }
            else if (us.First().Password != pass)
            {
                MessageBox.Show("Не правильно введений пароль. Перевірте введення", "Помилка авторизації");
            }
            else
            {
                AuthorisedUser = us.First();
                DialogResult = true;
            }
        }
    }
}
