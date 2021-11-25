﻿using System.ComponentModel;
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
        private RelayCommand addStateValueCommand;
        private RelayCommand saveStateValues;

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
        public List<StateValue> NewStateValues { get; set; }

        public RelayCommand AddStateValueCommand
        {
            get
            {
                return addStateValueCommand ??
                    (addStateValueCommand = new RelayCommand(o => 
                    {
                        var newItem = new StateValue();
                        newItem.AnimalID = Animal.AnimalID;

                        var window = new StateValueEditWindow(newItem);

                        if (window.ShowDialog() == true)
                        {
                            NewStateValues.Add(window.ViewModel.StateValue);
                        }
                    }));
            }
        }

        public RelayCommand SaveStateValues
        {
            get
            {
                return saveStateValues ??
                    (saveStateValues = new RelayCommand((o) => 
                    {
                        foreach (StateValue stateValue in NewStateValues)
                        {
                            db.StateValues.Local.Add(stateValue);
                        }

                        db.SaveChanges();
                    }));
            }
        }

        public AnimalEdit_VM(Animal a)
        {
            Animal = a;
            db = new DataContext();
            db.Rooms.Load();
            db.StateValues.Load();

            Rooms = db.Rooms.Local.ToBindingList();

            sex = Animal.Sex == "Жіноча" ? 0 : Animal.Sex == "Чоловіча" ? 1 : -1;

            NewStateValues = new List<StateValue>();
        }
    }
}