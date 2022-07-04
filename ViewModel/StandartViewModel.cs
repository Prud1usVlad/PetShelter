using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PetShelter.Model;

namespace PetShelter.ViewModel
{
    public class StandartViewModel : INotifyPropertyChanged
    {
        private IEnumerable<Animal> animals;
        private IEnumerable<Caretaker> caretakers;
        private IEnumerable<Client> clients;
        private IEnumerable<Contract> contracts;
        private IEnumerable<Emploee> emploees;
        private IEnumerable<InfoDepEmploee> infoDepEmploees;
        private IEnumerable<Room> rooms;
        private IEnumerable<Vaccination> vaccinations;
        private IEnumerable<Vaccine> vaccines;
        private IEnumerable<Group> groups;
        private IEnumerable<State> states;
        private IEnumerable<StateValue> stateValues;
        private IEnumerable<Producer> producers;
        private IEnumerable<User> users;

        public IEnumerable<Animal> Animals
        {
            get { return animals; }
            set
            {
                animals = value;
                OnPropertyChanged("Animals");
            }
        }

        public IEnumerable<Caretaker> Caretakers
        {
            get { return caretakers; }
            set
            {
                caretakers = value;
                OnPropertyChanged("Caretakers");
            }
        }

        public IEnumerable<Client> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged("Clients");
            }
        }

        public IEnumerable<Contract> Contracts
        {
            get { return contracts; }
            set
            {
                contracts = value;
                OnPropertyChanged("Contracts");
            }
        }

        public IEnumerable<Emploee> Emploees
        {
            get { return emploees; }
            set
            {
                emploees = value;
                OnPropertyChanged("Emploees");
            }
        }

        public IEnumerable<Room> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged("Rooms");
            }
        }

        public IEnumerable<Vaccination> Vaccinations
        {
            get { return vaccinations; }
            set
            {
                vaccinations = value;
                OnPropertyChanged("Vaccinations");
            }
        }

        public IEnumerable<Vaccine> Vaccines
        {
            get { return vaccines; }
            set
            {
                vaccines = value;
                OnPropertyChanged("Vaccines");
            }
        }

        public IEnumerable<InfoDepEmploee> InfoDepEmploees
        {
            get { return infoDepEmploees; }
            set
            {
                infoDepEmploees = value;
                OnPropertyChanged("InformationDepartmentEmploees");
            }
        }

        public IEnumerable<Group> Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                OnPropertyChanged("Groups");
            }
        }

        public IEnumerable<State> States
        {
            get { return states; }
            set
            {
                states = value;
                OnPropertyChanged("States");
            }
        }

        public IEnumerable<StateValue> StateValues
        {
            get { return stateValues; }
            set
            {
                stateValues = value;
                OnPropertyChanged("StateValues");
            }
        }

        public IEnumerable<Producer> Producers
        {
            get { return producers; }
            set
            {
                producers = value;
                OnPropertyChanged("Producers");
            }
        }

        public IEnumerable<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}