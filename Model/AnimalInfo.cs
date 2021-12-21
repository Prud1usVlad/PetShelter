using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PetShelter.Model
{
    public class AnimalInfo : DbEntity
    {
        string name;
        bool inShelter;
        DateTime? registrationDate;
        string readiness;
        Nullable<int> priority;
        private Animal animal;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public bool InShelter
        {
            get { return inShelter; }
            set
            {
                inShelter = value;
                OnPropertyChanged("InShelter");
            }
        }
        public DateTime? RegistrationDate {
            get { return registrationDate; }
            set
            {
                registrationDate = value;
                OnPropertyChanged("RegistrationDate");
            }
        }
        public string Readiness 
        {
            get { return readiness; }
            set
            {
                readiness = value;
                OnPropertyChanged("Readiness");
            }
        }
        public Nullable<int> Priority {
            get { return priority; }
            set
            {
                priority = value;
                OnPropertyChanged("Priority");
            }
        }

        public override List<DbEntity> GetForegnEntities()
        {
            var res = new List<DbEntity>();

            foreach (StateValue val in animal.StateValues)
            {
                res.Add(val);
            }

            res.Add(animal.Group);
            res.Add(animal.Room);

            foreach (Vaccination val in animal.Vaccinations)
            {
                res.Add(val);
                res.Add(val.Vaccine);
            }

            foreach (Contract val in animal.Contracts)
            {
                res.Add(val);
            }


            return res;
        }

        public override Dictionary<string, string> GetProperies()
        {
            var res = new Dictionary<string, string>();

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                res.Add(prop.Name, (prop.GetValue(this) ?? "-").ToString());
            }

            return res;
        }

        public override string ToString()
        {
            return $"{animal.AnimalKind} {Name}";
        }

        public override void CopyProperties(DbEntity toCopy)
        {
            var animal = toCopy as Animal;

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                prop.SetValue(this, prop.GetValue(animal));
            }
        }

        public override string GetSearchString()
        {
            return Name + " " + animal.AnimalID;
        }

        public AnimalInfo()
        {

        }

        public AnimalInfo(Animal a)
        {
            animal = a;

            Name = a.Name;
            inShelter = (a.Contracts.Count > 0) ? false : true;
            RegistrationDate = a.RegistrationDate;
            readiness = a.Group.Readiness;
            priority = a.Group.Priority;
        }
    }
}
