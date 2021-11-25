using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelter.Model;
using System.Data.Entity;

namespace PetShelter.ViewModel.EditWindowsVM
{
    public class StateValue_VM : StandartViewModel
    {
        private DataContext db;

        public StateValue StateValue { get; set; }
        public int StateIndex
        {
            get { return States.ToList().FindIndex(s => s.StateID == StateValue.StateID); }
            set { }
        }
        public int AnimalIndex
        {
            get { return Animals.ToList().FindIndex(a => a.AnimalID == StateValue.AnimalID); }
            set { }
        }
        public StateValue_VM(StateValue s)
        {
            StateValue = s;
            db = new DataContext();
            db.States.Load();
            db.Animals.Load();

            States = db.States.Local.ToBindingList();
            Animals = db.Animals.Local.ToBindingList();
        }
    }
}
