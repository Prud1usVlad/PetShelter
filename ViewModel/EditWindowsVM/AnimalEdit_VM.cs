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
    public class AnimalEdit_VM : StandartViewModel
    {
        private DataContext db;
        private int sex;


        public Animal Animal { get; set; }
        public int RoomIndex
        {
            get { return Rooms.ToList().FindIndex(r => r.RoomID == Animal.RoomID); }
            set { }
        }
        public int SexIndex
        {
            get { return sex; }
            set 
            { 
                sex = value;
                Animal.Sex = value == 0 ? "Жіноча" : "Чоловіча"; 
            }
        }
        public AnimalEdit_VM(Animal a)
        {
            Animal = a;
            db = new DataContext();
            db.Rooms.Load();

            Rooms = db.Rooms.Local.ToBindingList();

            sex = Animal.Sex == "Жіноча" ? 0 : Animal.Sex == "Чоловіча" ? 1 : -1;
        }
    }
}
