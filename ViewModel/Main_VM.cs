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

namespace PetShelter.ViewModel
{
    public partial class Main_VM : StandartViewModel
    {
        private DataContext db;
        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private RelayCommand deleteCommand;
        private RelayCommand copyCommand;
        private RelayCommand showDetailsCommand;
        private RelayCommand sortCommand;
        private RelayCommand searchCommand;
        private RelayCommand filtreCommand;


        private IEnumerable<DbEntity> itemSource;
        private IEnumerable<DbEntity> dataGridSource;
        private List<string> sourceProperties;

        public IEnumerable<DbEntity> ItemSource 
        { 
            get { return itemSource; }
            set
            {
                itemSource = value;
                SourceProperties = value.First().GetProperies().Keys.ToList();
                DataGridSource = value;
                OnPropertyChanged("ItemSource");
            }
        }

        public IEnumerable<DbEntity> DataGridSource
        {
            get { return dataGridSource; }
            set
            {
                dataGridSource = value;
                OnPropertyChanged("DataGridSource");
            }
        }

        public Dictionary<string, Dictionary<string, string>> ChosenItemDetails { get; set; }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand((dataSource) =>
                    {
                        ConstructorInfo[] constructors = GetEntityType(dataSource as IEnumerable).GetConstructors();
                        DbEntity entity = constructors[0].Invoke(null) as DbEntity;
                        IEditWindow window = entity.WindowType.GetConstructors()[1].Invoke(new[] { entity }) as IEditWindow;

                        if ((window as Window).ShowDialog() == true)
                        {
                            entity = Copy(window.Entity);
                            db.GetDBSet(entity).Add(entity);
                            db.SaveChanges();
                        }

                        if (entity is Animal)
                        {
                            StateValues = db.StateValues.Local.ToBindingList();
                        }
                    }));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand((selected) =>
                    {
                        if (selected == null)
                            return;

                        DbEntity entity = selected as DbEntity;
                        IEditWindow window = entity.WindowType.GetConstructors()[1].Invoke(new[] { Copy(entity) }) as IEditWindow;

                        if ((window as Window).ShowDialog() == true)
                        {
                            entity.CopyProperties(window.Entity);
                            db.Entry(entity).State = EntityState.Modified;
                        }

                        if (entity is Animal)
                        {
                            db.StateValues.Load();
                            StateValues = db.StateValues.Local.ToBindingList();
                            ChooseGroup(entity as Animal);
                        }

                        db.SaveChanges();
                    }));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand((selected) =>
                    {
                        if (selected == null)
                            return;

                        MessageBoxResult result = MessageBox.Show("Ви дійсно хочете видалити елемент?", "Видалення", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.No)
                            return;

                        DbEntity item = selected as DbEntity;
                        //if (item is Animal)
                        //{
                        //    var a = item as Animal;
                        //    while(a.StateValues.Count > 0)
                        //        db.StateValues.Remove(a.StateValues.First());
                        //}
                        db.GetDBSet(item).Remove(item);
                        db.SaveChanges();
                    }));
            }
        }

        public RelayCommand CopyCommand
        {
            get
            {
                return copyCommand ??
                    (copyCommand = new RelayCommand((selected) =>
                    {
                        if (selected == null)
                            return;

                        //IStandartEntity item = selected as IStandartEntity;
                        //item.AddToDb(db);
                    }));
            }
        }

        public RelayCommand ShowDetailsCommand
        {
            get
            {
                return showDetailsCommand ?? (showDetailsCommand = new RelayCommand((selected) =>
                {
                    if (selected == null || selected as DbEntity == null)
                        return;

                    var item = selected as DbEntity;
                    var details = new Dictionary<string, Dictionary<string, string>> 
                    {
                        { item.ToString(), item.GetProperies()}
                    };

                    AddDetails(item, details, item.GetType());

                    ChosenItemDetails = details;
                }));
            }
        }

        public RelayCommand SortCommand
        {
            get
            {
                return sortCommand ??
                    (sortCommand = new RelayCommand((settings) =>
                    {
                        var sett = settings as SortSettings;

                        PropertyInfo prop = itemSource.First().GetType().GetProperty(sett.Criterion);

                        if (sett.SortAsc == true)
                        {
                            DataGridSource = DataGridSource.OrderBy(o => prop.GetValue(o));
                        }
                        else
                        {
                            DataGridSource = DataGridSource.OrderByDescending(o => prop.GetValue(o));
                        }
                    }));
            }
        }

        public RelayCommand SearchCommand
        {
            get
            {
                return searchCommand ??
                    (searchCommand = new RelayCommand((input) => 
                    {
                        var newItemSource = new BindingList<DbEntity>();

                        foreach (DbEntity entity in ItemSource)
                        {
                            if (entity.GetSearchString().ToLower().Contains(input.ToString().ToLower()))
                            {
                                newItemSource.Add(entity);
                            }
                        }

                        SetNewDataGridSource(newItemSource);

                    }));
            }
        }

        public RelayCommand FiltreCommand
        {
            get
            {
                return filtreCommand ??
                    (filtreCommand = new RelayCommand((o) => 
                    {
                        var filtre = new Filtration(itemSource, this);
                        filtre.StartFiltring();

                    }));
            }
        }

        public List<string> SourceProperties 
        { 
            get { return sourceProperties; } 
            set
            {
                sourceProperties = value;
                OnPropertyChanged("SourceProperties");
            }
        }

        public Main_VM()
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
            db.Caretakers.Load();
            db.Producers.Load();
            db.Vaccinations.Load();
            db.Vaccines.Load();

            Animals = db.Animals.Local.ToBindingList();
            Rooms = db.Rooms.Local.ToBindingList();
            Groups = db.Groups.Local.ToBindingList();
            States = db.States.Local.ToBindingList();
            Contracts = db.Contracts.Local.ToBindingList();
            Clients = db.Clients.Local.ToBindingList();
            InfoDepEmploees = db.InfoDepEmploees.Local.ToBindingList();
            StateValues = db.StateValues.Local.ToBindingList();
            Emploees = db.Emploees.Local.ToBindingList();
            Caretakers = db.Caretakers.Local.ToBindingList();
            Producers = db.Producers.Local.ToBindingList();
            Vaccinations = db.Vaccinations.Local.ToBindingList();
            Vaccines = db.Vaccines.Local.ToBindingList();



            ChosenItemDetails = null;
            ItemSource = Animals;
        }


        private Type GetEntityType(IEnumerable list)
        {
            return list.GetType().GetInterface("IEnumerable`1").GetGenericArguments()[0];
        }

        private void AddDetails(DbEntity item, Dictionary<string, Dictionary<string, string>> details, Type forbidden)
        {
            List<DbEntity> foregnEntities = item.GetForegnEntities();

            if (foregnEntities == null || details.Count > 50)
                return;


            foreach (DbEntity foregnItem in foregnEntities)
            {
                if (foregnItem == null || foregnItem.GetType() == forbidden)
                    continue;

                string key = foregnItem.ToString();
                int count = 1;
                while (details.Keys.Contains(key + " №" + count))
                {
                    count++;
                }
                key += " №" + count;

                details.Add(key, foregnItem.GetProperies());
            }
        }

        public void SetNewDataGridSource(IEnumerable<DbEntity> newItemSource)
        {
            switch (itemSource.First().GetType().Name)
            {
                case "Animal":
                    DataGridSource = newItemSource.Select(e => e as Animal);
                    break;
                case "Group":
                    DataGridSource = newItemSource.Select(e => e as Group);
                    break;
                case "Room":
                    DataGridSource = newItemSource.Select(e => e as Room);
                    break;
                case "State":
                    DataGridSource = newItemSource.Select(e => e as State);
                    break;
                case "StateValue":
                    DataGridSource = newItemSource.Select(e => e as StateValue);
                    break;
            }
        }

        private void ChooseGroup(Animal animal)
        {
            IEnumerable<StateValue> stateValues = animal.StateValues;

            int phisycalPoints = stateValues.Where(sv => sv.StateID == 2).Count();
            int psychicalPoints = stateValues.Where(sv => sv.StateID == 1).Count();
            int socialPoints = stateValues.Where(sv => sv.StateID == 3).Count();
            int placePoints = stateValues.Where(sv => sv.StateID == 4).Count();
            int relapsePoints = stateValues.Where(sv => sv.StateID == 5).Count();
            int groupID = 2;

            if (psychicalPoints == 0 && socialPoints == 0 && phisycalPoints == 0
                && placePoints == 0 && relapsePoints == 0)
            {
                groupID = 1;
            }
            else if (psychicalPoints == 0 && socialPoints == 0 && phisycalPoints == 1
                && placePoints == 0 && relapsePoints == 0)
            {
                groupID = 2;
            }
            else if (phisycalPoints >= 2 || ((DateTime.Now - animal.BirthDate).GetValueOrDefault().TotalDays > 2555)
                && placePoints == 0 && relapsePoints <= 1)
            {
                groupID = 3;
            }
            else if (psychicalPoints <= 1 && socialPoints <= 1 && phisycalPoints >= 2
                && placePoints == 0 && relapsePoints <= 1)
            {
                groupID = 4;
            }
            else if (psychicalPoints <= 1 && socialPoints <= 1 && phisycalPoints >= 2
                && placePoints > 0 && relapsePoints <= 1)
            {
                groupID = 5;
            }
            else if (psychicalPoints == 0 && socialPoints == 0 && phisycalPoints == 0
                && placePoints == 0 && relapsePoints == 0 
                && (DateTime.Now - animal.RegistrationDate).GetValueOrDefault().TotalDays > 365)
            {
                groupID = 6;
            }
            else if ((psychicalPoints > 0 || socialPoints > 0) && phisycalPoints <= 1
                           && placePoints <= 1 && relapsePoints <= 1)
            {
                groupID = 7;
            }
            else if (psychicalPoints <= 1 && socialPoints <= 1 && phisycalPoints <= 1
                && placePoints == 0 && relapsePoints > 0)
            {
                groupID = 8;
            }
            else if (psychicalPoints == 0 && socialPoints == 0 && phisycalPoints == 0
                && placePoints > 0 && relapsePoints == 0)
            {
                groupID = 8;
            }

            animal.GroupID = groupID;

        }
        
    }
}
