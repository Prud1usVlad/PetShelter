using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelter.Model;
using PetShelter.View.FiltreWindows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity;
using System.Collections;
using System.Windows;

namespace PetShelter.ViewModel
{
    class Filtration
    {
        public IEnumerable<DbEntity> Entities { get; set; }
        public Main_VM ViewModel { get; set; }

        public Filtration(IEnumerable<DbEntity> entities, Main_VM vm)
        {
            Entities = entities;
            ViewModel = vm;
        }

        public void StartFiltring()
        {
            string typeName = Entities.First().GetType().Name;

            switch (typeName)
            {
                case "Animal":
                    var wind = new Animal_filtre(ViewModel);

                    if (wind.ShowDialog() == true)
                    {
                        IEnumerable<Animal> filtrated;
                        // animal kind
                        if (wind.SelectedValues[0].Count > 0)
                        {
                            filtrated = ViewModel.Animals.Where(a => wind.SelectedValues[0].Contains(a.AnimalKind));
                        }
                        else
                        {
                            filtrated = ViewModel.Animals;
                        }

                        // animal group
                        if (wind.SelectedValues[1].Count > 0)
                        {
                            filtrated = filtrated.Where(a => wind.SelectedValues[1].Contains(a.Group.Description));
                        }

                        // animal room
                        if (wind.SelectedValues[2].Count > 0)
                        {
                            filtrated = filtrated.Where(a => wind.SelectedValues[2].Contains(a.Room.Name));
                        }

                        ViewModel.SetNewDataGridSource(filtrated);
                    }

                    break;
                case "Group":
                    var wind1 = new Group_filtre(ViewModel);

                    if (wind1.ShowDialog() == true)
                    {
                        IEnumerable<Group> filtrated;
                        // group priority
                        if (wind1.SelectedValues[0].Count > 0)
                        {
                            filtrated = ViewModel.Groups.Where(g => wind1.SelectedValues[0].Contains(g.Priority.ToString()));
                        }
                        else
                        {
                            filtrated = ViewModel.Groups;
                        }

                        // group readines
                        if (wind1.SelectedValues[1].Count > 0)
                        {
                            filtrated = filtrated.Where(g => wind1.SelectedValues[1].Contains(g.Readiness));
                        }

                        ViewModel.SetNewDataGridSource(filtrated);
                    }
                    break;
                case "StateValue":
                    var wind2 = new StateValue_filtre(ViewModel);

                    if (wind2.ShowDialog() == true)
                    {
                        IEnumerable<StateValue> filtrated;
                        // StateValue state
                        if (wind2.SelectedValues[0].Count > 0)
                        {
                            filtrated = ViewModel.StateValues.Where(s => wind2.SelectedValues[0].Contains(s.State.Name));
                        }
                        else
                        {
                            filtrated = ViewModel.StateValues;
                        }

                        //StateValue animal
                        if (wind2.SelectedValues[1].Count > 0)
                        {
                            filtrated = filtrated.Where(s => wind2.SelectedValues[1].Contains(s.Animal.Name));
                        }

                        ViewModel.SetNewDataGridSource(filtrated);
                    }
                    break;
                case "AnimalInfo":
                    var wind3 = new AnimalInfo_filtre(ViewModel);

                    if (wind3.ShowDialog() == true)
                    {
                        IEnumerable<AnimalInfo> filtrated;
                        // StateValue state
                        if (wind3.SelectedValues[0].Count > 0)
                        {
                            filtrated = ViewModel.AnimalInfos.Where(i => wind3.SelectedValues[0].Contains(i.Priority.ToString()));
                        }
                        else
                        {
                            filtrated = ViewModel.AnimalInfos;
                        }

                        //StateValue animal
                        if (wind3.SelectedValues[1].Count > 0)
                        {
                            filtrated = filtrated.Where(i => wind3.SelectedValues[1].Contains(i.Readiness));
                        }

                        if (wind3.InShelterVal == true && wind3.OutaShelterVal == true ||
                            wind3.InShelterVal != true && wind3.OutaShelterVal != true)
                        {

                        }
                        else if (wind3.InShelterVal == true)
                            filtrated = filtrated.Where(i => i.InShelter == true);
                        else if (wind3.OutaShelterVal == true)
                            filtrated = filtrated.Where(i => i.InShelter == false);

                        filtrated = filtrated.Where(i => i.RegistrationDate <= wind3.ToDate && i.RegistrationDate >= wind3.FromDate );

                        ViewModel.SetNewDataGridSource(filtrated);
                    }
                    break;
                case "Client":
                    var wind4 = new Client_Filtre(ViewModel);

                    if (wind4.ShowDialog() == true)
                    {
                        IEnumerable<Client> filtrated;
                        // Client City
                        if (wind4.SelectedValues[0].Count > 0)
                        {
                            filtrated = ViewModel.Clients.Where(i => wind4.SelectedValues[0].Contains(i.Region));
                        }
                        else
                        {
                            filtrated = ViewModel.Clients;
                        }

                        // Client Region
                        if (wind4.SelectedValues[1].Count > 0)
                        {
                            filtrated = filtrated.Where(i => wind4.SelectedValues[1].Contains(i.City));
                        }

                        filtrated = filtrated.Where(i => i.DateOfAdding <= wind4.ToDate && i.DateOfAdding >= wind4.FromDate);

                        ViewModel.SetNewDataGridSource(filtrated);
                    }

                    break;


            }
        }
    }
}
