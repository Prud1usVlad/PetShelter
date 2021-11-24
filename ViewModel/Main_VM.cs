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
                            db.SaveChanges();
                        }
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
                    if (selected == null)
                        return;

                    var item = selected as DbEntity;
                    var details = new Dictionary<string, Dictionary<string, string>> 
                    {
                        { item.ToString(), item.GetProperies()}
                    };

                    AddDetails(item, details);

                    ChosenItemDetails = details;
                }));
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

            Animals = db.Animals.Local.ToBindingList();
            Rooms = db.Rooms.Local.ToBindingList();

            ChosenItemDetails = null;
        }


        private Type GetEntityType(IEnumerable list)
        {
            return list.GetType().GetInterface("IEnumerable`1").GetGenericArguments()[0];
        }

        private void AddDetails(DbEntity item, Dictionary<string, Dictionary<string, string>> details)
        {
            List<DbEntity> foregnEntities = item.GetForegnEntities();

            if (foregnEntities == null)
                return;


            foreach (DbEntity foregnItem in foregnEntities)
            {
                if (foregnItem == null)
                    continue;

                string key = foregnItem.ToString();
                int count = 1;
                while (details.Keys.Contains(key + " №" + count))
                {
                    count++;
                }
                key += " №" + count;

                details.Add(key, foregnItem.GetProperies());

                AddDetails(foregnItem, details);
            }
        }

    }
}
