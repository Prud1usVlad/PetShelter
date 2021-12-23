using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity;
using System.Collections.Generic;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Reflection;
using PetShelter.Model;
using PetShelter.View;
using PetShelter.View.EditWindows;
using PetShelter.ViewModel;
using System.Linq;

namespace PetShelter.ViewModel.EditWindowsVM
{
    public class ContractEdit_VM : StandartViewModel
    {
        private DataContext db;

        public Contract Contract { get; set; }
        public int AnimalIndex
        {
            get { return ChoosableAnimals.ToList().FindIndex(i => i.AnimalID == Contract.AnimalID); }
            set { }
        }
        public int ClientIndex
        {
            get { return Clients.ToList().FindIndex(i => i.ClientID == Contract.ClientID); }
            set { }
        }
        public int EmploeeIndex
        {
            get { return InfoDepEmploees.ToList().FindIndex(i => i.PassNum == Contract.PassNum); }
            set { }
        }
        public IEnumerable<Animal> ChoosableAnimals { get; set; }

        public ContractEdit_VM(Contract a)
        {
            Contract = a;
            db = new DataContext();
            db.Clients.Load();
            Clients = db.Clients.Local.ToBindingList();
            db.Animals.Load();
            Animals = db.Animals.Local.ToBindingList();
            db.InfoDepEmploees.Load();
            InfoDepEmploees = db.InfoDepEmploees.Local.ToBindingList();
            db.Emploees.Load();
            Emploees = db.Emploees.Local.ToBindingList();
            db.Contracts.Load();
            Contracts = db.Contracts.Local.ToBindingList();

            ChoosableAnimals = Animals.Where(i => i.Contracts.Count == 0 || i.Contracts.Last().TerminationDate < DateTime.Now);
            if (Contract.Animal !=  null)
            {
                var c = ChoosableAnimals.Select(an => an).ToList();
                c.Add(Contract.Animal);
                ChoosableAnimals = c;
            }
        }
    }
}
