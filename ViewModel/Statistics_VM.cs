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
using PetShelter.ViewModel.Settings;
using System.Linq;
using System.Data;

namespace PetShelter.ViewModel
{
    public class Statistics_VM : StandartViewModel
    {
        private DataContext db;
        private DateTime to;
        private DateTime from;
        private DataTable table2;

        public DataTable FirstStat { get; set; }
        public DataTable SecondStat 
        { 
            get { return table2; } 
            set 
            { 
                table2 = value;
                OnPropertyChanged("SecondStat");
            }
        }
        public DataTable ThirdStat { get; set; }
        public DataTable FourthStat { get; set; }

        public DateTime FromDate
        {
            get { return from; }
            set 
            { 
                from = value;
                OnPropertyChanged("FromDate");
            }
        }

        public DateTime ToDate
        {
            get { return to; }
            set 
            { 
                to = value;
                OnPropertyChanged("ToDate");
            }
        } 
        

        public Statistics_VM()
        {
            db = new DataContext();
            db.Animals.Load();
            db.Groups.Load();
            db.Rooms.Load();
            db.States.Load();
            db.StateValues.Load();
            db.Contracts.Load();
            db.Clients.Load();
            db.InfoDepEmploees.Load();
            db.Emploees.Load();
            db.Vaccinations.Load();
            db.Vaccines.Load();
            db.Producers.Load();
            db.Caretakers.Load();


            Vaccinations = db.Vaccinations.Local.ToBindingList();
            Vaccines = db.Vaccines.Local.ToBindingList();
            Producers = db.Producers.Local.ToBindingList();
            Caretakers = db.Caretakers.Local.ToBindingList();
            Animals = db.Animals.Local.ToBindingList();
            Rooms = db.Rooms.Local.ToBindingList();
            Groups = db.Groups.Local.ToBindingList();
            States = db.States.Local.ToBindingList();
            Contracts = db.Contracts.Local.ToBindingList();
            Clients = db.Clients.Local.ToBindingList();
            InfoDepEmploees = db.InfoDepEmploees.Local.ToBindingList();
            StateValues = db.StateValues.Local.ToBindingList();
            Emploees = db.Emploees.Local.ToBindingList();

            ToDate = DateTime.Now.Date;
            FromDate = new DateTime(2010, 1, 1);

            FillDatatable1();
            FillDatatable2();
            FillDatatable3();
            FillDatatable4();
           
        }

        public void FillDatatable1()
        {
            var table = new DataTable("firstStat");

            DataColumn col = new DataColumn("Ім'я тварини");
            table.Columns.Add(col);

            col = new DataColumn("Доглядач");
            table.Columns.Add(col);

            col = new DataColumn("Група");
            table.Columns.Add(col);

            foreach(Animal a in Animals)
            {
                var c = Emploees.Where(e => e.PassNum == a.Room.Caretakers.First().PassNum).First();
                table.Rows.Add(new object[] { a.Name, c.FirstName + " " + c.SecondName, a.GroupID + " - " + a.Group.Description});
            }

            FirstStat = table;
        }

        public void FillDatatable2()
        {
            if (!CheckDate()) return;

            var table = new DataTable("secondStat");

            DataColumn col = new DataColumn("Ім'я тварини");
            table.Columns.Add(col);

            col = new DataColumn("Доглядач");
            table.Columns.Add(col);

            col = new DataColumn("Дата");
            table.Columns.Add(col);

            col = new DataColumn("Препарат");
            table.Columns.Add(col);

            col = new DataColumn("Виробник");
            table.Columns.Add(col);

            foreach (Vaccination v in Vaccinations)
            {
                if (v.VaccinationDate <= ToDate && v.VaccinationDate >= FromDate)
                {
                    var c = Emploees.Where(e => e.PassNum == v.Animal.Room.Caretakers.First().PassNum).First();
                    table.Rows.Add(new object[] { v.Animal.Name, c.FirstName + " " + c.SecondName,
                    v.VaccinationDate, v.Vaccine.VaccineName, v.Vaccine.Producer.Title });
                }
            }

            SecondStat = table;
        }

        public void FillDatatable3()
        {
            var table = new DataTable("thirdStat");

            DataColumn col = new DataColumn("Рік");
            table.Columns.Add(col);

            col = new DataColumn("Кількість тварин");
            table.Columns.Add(col);

            col = new DataColumn("Кількість клієнтів");
            table.Columns.Add(col);

            col = new DataColumn("Кількість договорів");
            table.Columns.Add(col);

            table.Rows.Add(new object[] { 2019, Animals.Where(a => a.RegistrationDate.Value.Year == 2019).Count(),
                Clients.Where(c => c.DateOfAdding.Value.Year == 2019).Count(),
                Contracts.Where(c => c.SigningDate.Value.Year == 2019).Count(), });

            table.Rows.Add(new object[] { 2020, Animals.Where(a => a.RegistrationDate.Value.Year == 2020).Count(),
                Clients.Where(c => c.DateOfAdding.Value.Year == 2020).Count(),
                Contracts.Where(c => c.SigningDate.Value.Year == 2020).Count(), });

            table.Rows.Add(new object[] { 2021, Animals.Where(a => a.RegistrationDate.Value.Year == 2021).Count(),
                Clients.Where(c => c.DateOfAdding.Value.Year == 2021).Count(),
                Contracts.Where(c => c.SigningDate.Value.Year == 2021).Count(), });

            ThirdStat = table;
        }

        public void FillDatatable4()
        {
            var table = new DataTable("fourthStat");

            DataColumn col = new DataColumn("Стан");
            table.Columns.Add(col);

            col = new DataColumn("Кількість тварин");
            table.Columns.Add(col);

            col = new DataColumn("Відсоток");
            table.Columns.Add(col);

            foreach (State s in States)
            {
                var count = Animals.Where(a => a.StateValues.Select(sv => sv.State.StateID).Contains(s.StateID)).Count();
                table.Rows.Add(new object[] { s.Name, count, count * 100 / Animals.Count()});
            }

            FourthStat = table;
        }

        private bool CheckDate()
        {
            var d = new DateTime(2000, 1, 1);

            if (ToDate < d || ToDate > DateTime.Now
                || FromDate < d || FromDate > DateTime.Now)
            {
                MessageBox.Show("Введені дати виходять за рамки дозволених, перевірте правильність введення.", "Помилка введення дати");
                return false;
            }

            return true;
        }

        
    }
}
