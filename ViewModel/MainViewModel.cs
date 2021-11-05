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

namespace PetShelter.ViewModel
{
    public class MainViewModel : StandartVievModel
    {
        private DataContext db;
        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private RelayCommand deleteCommand;
        private RelayCommand copyCommand;
        private RelayCommand showDetailsCommand;

        public Dictionary<Type, (Type WindowType, IEnumerable DataList)> typesConnections;

        public Dictionary<string, Dictionary<string, string>> ChosenItemDetails { get; set; }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand((dataSource) =>
                    {
                        //ConstructorInfo[] constructors = GetEntityType(dataSource as IEnumerable).GetConstructors();
                        //IStandartEntity entity = constructors[0].Invoke(null) as IStandartEntity;

                        //entity.AddToDb(db);
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
                        //IStandartEntity item = selected as IStandartEntity;
                        //item.UpdateDb(db);
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

                        //IStandartEntity item = selected as IStandartEntity;
                        //item.DeleteFromDb(db);
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

                    foreach (DbEntity foregnItem in item.GetForegnEntities())
                    {
                        details.Add(foregnItem.ToString(), foregnItem.GetProperies());

                        List<DbEntity> innerEntities = foregnItem.GetForegnEntities();
                        if (innerEntities != null)
                        {
                            foreach (DbEntity inner in innerEntities)
                            {
                                details.Add(inner.ToString(), inner.GetProperies());
                            }
                        }
                    }

                    ChosenItemDetails = details;
                }));
            }
        }



        public MainViewModel()
        {
            db = new DataContext();
            db.Animals.Load();
            db.Caretakers.Load();
            db.Clients.Load();
            db.Contracts.Load();
            db.Groups.Load();
            db.InfoDepEmploees.Load();
            db.Producers.Load();
            db.Rooms.Load();
            db.States.Load();
            db.StateValues.Load();
            db.Vaccinations.Load();
            db.Vaccines.Load();

            Animals = db.Animals.Local.ToBindingList();

            ChosenItemDetails = null;

            //typesConnections = new Dictionary<Type, (Type WindowType, IEnumerable DataList)>
            //{
            //    { typeof(Animal), (typeof(AnimalWindow), Animals) },
            //    { typeof(Emploee), (typeof(EmploeeWindow), Emploees) },

            //};
        }


        private Type GetEntityType(IEnumerable list)
        {
            return list.GetType().GetInterface("IEnumerable`1").GetGenericArguments()[0];
        }

    }
}
