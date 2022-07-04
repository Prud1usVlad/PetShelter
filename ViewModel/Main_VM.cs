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
        public static Dictionary<string, string> Dictionary = new Dictionary<string, string>
        {
            {"AnimalID", "№ Паспорту тварини" },
            {"Name" , "Ім'я" },
            {"Sex", "Стать" },
            {"AnimalKind", "Вид тварини" },
            {"Height", "Висота" },
            {"Weight", "Вага" },
            {"Color", "Окрас" },
            {"BirthDate", "Дата народження" },
            {"RegistrationDate", "Дата регістрації" },
            {"QuarantineDays", "Днів у карантині" },
            {"RoomID", "Номер кімнати" },
            {"GroupID", "Номер групи" },
            {"PassNum", "Номер перепустки" },
            {"Shift", "Зміна" },
            {"MajorAnimalKind", "Основний вид тварин" },
            {"MedicalEducation", "Медична освіта" },
            {"ClientID", "Номер клієнта" },
            {"IDCardNum", "Номер паспорта" },
            {"IDCardSeries", "Серія паспорта" },
            {"FirstName", "Ім'я людини" },
            {"SecondName", "Фамілія" },
            {"ThirdName", "По батькові" },
            {"DateOfAdding", "Дата регістрації" },
            {"Region", "Область" },
            {"City", "Місто" },
            {"Street", "Вулиця" },
            {"BuildingNum", "Номер будинку" },
            {"FlatNum", "Номер квартири" },
            {"Phone", "Номер телефону" },
            {"Email", "Електронна пошта" },
            {"ContractNum", "Номер договору" },
            {"SigningDate", "Дата підписання" },
            {"TerminationDate", "Дата розторгнення" },
            {"EntryDate", "Дата зарахування" },
            {"Profession", "Професія" },
            {"Priority", "Приорітет" },
            {"Readiness", "Готовність" },
            {"AdditionalCare", "Додатковий догляд" },
            {"Description", "Описання" },
            {"ProducerID", "НомерВиробника" },
            {"Title", "Назва виробника" },
            {"Adress", "Адреса" },
            {"MaxAnimalAmount", "Максимальна кількість тварин" },
            {"StateValueID", "Номер значення стану" },
            {"StateID", "Номер стану" },
            {"Value", "Значення" },
            {"VaccinationDate", "Дата вакцинації" },
            {"VaccineID", "Номер вакцини" },
            {"VaccineName", "Назва вакцини" },
            {"ActiveSubstanceName", "Активна речовина" },
            {"ActiveSubstanceLatinName", "Активна речовина латинню" },
            {"RevaccinationDogs", "Ревакцинація - собаки" },
            {"RevaccinationRodents", "Ревакцинація - гризуни" },
            {"RevaccinationMeatEaters", "Ревакцинація - м'ясоїди" },
            {"Producer", "Виробник"},
            {"Employment", "Зайнятість"},
            {"InShelter", "В притулку"}
        };

        private IEnumerable<AnimalInfo> animalInfos;
        private IEnumerable<Animal> animals;

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

        public IEnumerable<AnimalInfo> AnimalInfos
        {
            get { return animalInfos; }
            set
            {
                animalInfos = value;
                OnPropertyChanged("AnimalInfos");
            }
        }
        public new IEnumerable<Animal> Animals
        {
            get { return animals; }
            set
            {
                animals = value;
                OnPropertyChanged("Animals");
                var res = new BindingList<AnimalInfo>();
                foreach (Animal a in value)
                {
                    res.Add(new AnimalInfo(a));
                }

                AnimalInfos = res;
            }
        }

        public IEnumerable<DbEntity> ItemSource 
        { 
            get { return itemSource; }
            set
            {
                itemSource = value;
                SourceProperties = value.First().GetProperies().Keys.Select(i => Dictionary[i]).ToList();
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
                        IEditWindow window = entity.WindowType.GetConstructors()[1].Invoke(new object[] { entity, ("Додавання ", "Додати") }) as IEditWindow;

                        if ((window as Window).ShowDialog() == true)
                        {
                            entity = Copy(window.Entity);
                            db.GetDBSet(entity).Add(entity);
                            db.SaveChanges();
                        }

                        if (entity is Animal)
                        {
                            db.StateValues.Load();
                            StateValues = db.StateValues.Local.ToBindingList();
                            ChooseGroup(entity as Animal);
                        }
                        else if (entity is StateValue)
                        {
                            ChooseGroup((entity as StateValue).Animal);
                        }
                        db.SaveChanges();

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
                        IEditWindow window = entity.WindowType.GetConstructors()[1].Invoke(new object[] { Copy(entity), ("Редагування ", "Редагувати дані") }) as IEditWindow;

                        if ((window as Window).ShowDialog() == true)
                        {
                            Animal anim = (selected is StateValue) ? (selected as StateValue).Animal : null;

                            entity.CopyProperties(window.Entity);
                            db.Entry(entity).State = EntityState.Modified;

                            if (entity is StateValue)
                            {
                                db.StateValues.Load();
                                StateValues = db.StateValues.Local.ToBindingList();
                                ChooseGroup(anim);
                            }
                        }

                        if (entity is Animal)
                        {
                            db.StateValues.Load();
                            StateValues = db.StateValues.Local.ToBindingList();
                            ChooseGroup(entity as Animal);
                        }

                        if (entity is StateValue)
                        {
                            db.StateValues.Load();
                            StateValues = db.StateValues.Local.ToBindingList();
                            ChooseGroup((entity as StateValue).Animal);
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

                        Animal anim = (selected is StateValue) ? (selected as StateValue).Animal : null;


                        DbEntity item = selected as DbEntity;

                        db.GetDBSet(item).Remove(item);
                        db.SaveChanges();

                        if (item is StateValue)
                        {
                            db.StateValues.Load();
                            StateValues = db.StateValues.Local.ToBindingList();
                            ChooseGroup(anim);
                        }

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

                        PropertyInfo prop = itemSource.First().GetType().GetProperty(Dictionary.Where(v => v.Value == sett.Criterion).First().Key);

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
            db.Users.Load();

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
            Users = db.Users.Local.ToBindingList();

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
                if (details.Keys.Contains(key))
                {
                    count = 2;
                    while (details.Keys.Contains(key + " №" + count))
                    {
                        count++;
                    }
                }

                
                key += (count == 1) ? "" : " №" + count;

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
                case "AnimalInfo":
                    DataGridSource = newItemSource.Select(e => e as AnimalInfo);
                    break;
                case "Client":
                    DataGridSource = newItemSource.Select(e => e as Client);
                    break;
                case "Vaccine":
                    DataGridSource = newItemSource.Select(e => e as Vaccine);
                    break;
                case "Vaccination":
                    DataGridSource = newItemSource.Select(e => e as Vaccination);
                    break;
                case "Producer":
                    DataGridSource = newItemSource.Select(e => e as Producer);
                    break;
                case "Contract":
                    DataGridSource = newItemSource.Select(e => e as Contract);
                    break;
                case "Emploee":
                    DataGridSource = newItemSource.Select(e => e as Emploee);
                    break;
                case "InfoDepEmploee":
                    DataGridSource = newItemSource.Select(e => e as InfoDepEmploee);
                    break;
                case "Caretaker":
                    DataGridSource = newItemSource.Select(e => e as Caretaker);
                    break;
            }
        }

        private void ChooseGroup(Animal animal)
        {
            if (animal == null)
                return;

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
