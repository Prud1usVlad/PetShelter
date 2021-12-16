//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetShelter.Model
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    
    public partial class Vaccination : DbEntity
    {
        private int vaccineID;
        private int animalID;
        private System.DateTime vaccinationDate;

        public int VaccineID
        {
            get { return vaccineID; }
            set
            {
                vaccineID = value;
                OnPropertyChanged("VaccineID");
            }
        }
        public int AnimalID {
            get { return animalID; }
            set
            {
                animalID = value;
                OnPropertyChanged("AnimalID");
            }
        }
        public System.DateTime VaccinationDate {
            get { return vaccinationDate; }
            set
            {
                vaccinationDate = value;
                OnPropertyChanged("VaccinationDate");
            }
        }

        internal virtual Animal Animal { get; set; }
        internal virtual Vaccine Vaccine { get; set; }

        public override List<DbEntity> GetForegnEntities()
        {
            return new List<DbEntity> { Animal, Vaccine };
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

        public override void CopyProperties(DbEntity toCopy)
        {
            var item = toCopy as Vaccination;

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                prop.SetValue(this, prop.GetValue(toCopy));
            }
        }

        public override string ToString()
        {
            return "Вакцинація";
        }

        public override string GetSearchString()
        {
            return $"{VaccineID} {AnimalID}";
        }
    }
}
