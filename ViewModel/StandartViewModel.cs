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
    public class StandartVievModel : INotifyPropertyChanged
    {
        private IEnumerable<Animals> animals;
        private IEnumerable<Caretakers> caretakers;
        private IEnumerable<Clients> clients;
        private IEnumerable<Contracts> contracts;
        private IEnumerable<InfoDepEmploees> infoDepEmploees;
        private IEnumerable<Rooms> rooms;
        private IEnumerable<Vaccinations> vaccinations;
        private IEnumerable<Vaccines> vaccines;
        private IEnumerable<Groups> groups;
        private IEnumerable<States> states;
        private IEnumerable<StateValues> stateValues;

        public IEnumerable<Animals> Animals
        {
            get { return animals; }
            set
            {
                animals = value;
                OnPropertyChanged("Animals");
            }
        }

        public IEnumerable<Caretakers> Caretakers
        {
            get { return caretakers; }
            set
            {
                caretakers = value;
                OnPropertyChanged("Caretakers");
            }
        }

        public IEnumerable<Clients> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged("Clients");
            }
        }

        public IEnumerable<Contracts> Contracts
        {
            get { return contracts; }
            set
            {
                contracts = value;
                OnPropertyChanged("Contracts");
            }
        }

        public IEnumerable<Rooms> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged("Rooms");
            }
        }

        public IEnumerable<Vaccinations> Vaccinations
        {
            get { return vaccinations; }
            set
            {
                vaccinations = value;
                OnPropertyChanged("Vaccinations");
            }
        }

        public IEnumerable<Vaccines> Vaccines
        {
            get { return vaccines; }
            set
            {
                vaccines = value;
                OnPropertyChanged("Vaccines");
            }
        }

        public IEnumerable<InfoDepEmploees> InfoDepEmploees
        {
            get { return infoDepEmploees; }
            set
            {
                infoDepEmploees = value;
                OnPropertyChanged("InformationDepartmentEmploees");
            }
        }

        public IEnumerable<Groups> Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                OnPropertyChanged("Groups");
            }
        }

        public IEnumerable<States> States
        {
            get { return states; }
            set
            {
                states = value;
                OnPropertyChanged("States");
            }
        }

        public IEnumerable<StateValues> StateValues
        {
            get { return stateValues; }
            set
            {
                stateValues = value;
                OnPropertyChanged("StateValues");
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