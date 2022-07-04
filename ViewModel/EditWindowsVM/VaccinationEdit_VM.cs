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
    public class VaccinationEdit_VM : StandartViewModel
    {
        private DataContext db;

        public Vaccination Vaccination { get; set; }
        public int AnimalIndex
        {
            get { return ChoosableAnimals.ToList().FindIndex(i => i.AnimalID == Vaccination.AnimalID); }
            set { }
        }
        public int VaccineIndex
        {
            get { return Vaccines.ToList().FindIndex(i => i.VaccineID == Vaccination.VaccineID); }
            set { }
        }

        public IEnumerable<Animal> ChoosableAnimals { get; set; }

        public VaccinationEdit_VM(Vaccination v)
        {
            Vaccination = v;

            if (Vaccination.VaccinationDate == new DateTime(1, 1, 1))
                Vaccination.VaccinationDate = DateTime.Now.Date;
            db = new DataContext();
            db.Animals.Load();
            Animals = db.Animals.Local.ToBindingList();
            db.Vaccines.Load();
            Vaccines = db.Vaccines.Local.ToBindingList();
            db.Contracts.Load();
            Contracts = db.Contracts.Local.ToBindingList();

            ChoosableAnimals = Animals.Where(i => i.Contracts.Count == 0);
            if (Vaccination.Animal != null)
            {
                var c = ChoosableAnimals.Select(an => an).ToList();
                c.Add(Vaccination.Animal);
                ChoosableAnimals = c;
            }
        }
    }
}
