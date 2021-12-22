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

    public partial class Vaccine : DbEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vaccine()
        {
            this.Vaccinations = new HashSet<Vaccination>();
        }

        private int vaccineID;
        private string vaccineName;
        private string activeSubstanceName;
        private string activeSubstanceLatinName;
        private Nullable<int> revaccinationDogs;
        private Nullable<int> revaccinationRodents;
        private Nullable<int> revaccinationMeatEaters;
        private Nullable<int> producerID;

        public int VaccineID
        {
            get { return vaccineID; }
            set
            {
                vaccineID = value;
                OnPropertyChanged("VaccineID");
            }
        }
        public string VaccineName
        {
            get { return vaccineName; }
            set
            {
                vaccineName = value;
                OnPropertyChanged("VaccineName");
            }
        }
        public string ActiveSubstanceName
        {
            get { return activeSubstanceName; }
            set
            {
                activeSubstanceName = value;
                OnPropertyChanged("ActiveSubstanceName");
            }
        }
        public string ActiveSubstanceLatinName
        {
            get { return activeSubstanceLatinName; }
            set
            {
                activeSubstanceLatinName = value;
                OnPropertyChanged("ActiveSubstanceLatinName");
            }
        }
        public Nullable<int> RevaccinationDogs
        {
            get { return revaccinationDogs; }
            set
            {
                revaccinationDogs = value;
                OnPropertyChanged("RevaccinationDogs");
            }
        }
        public Nullable<int> RevaccinationRodents
        {
            get { return revaccinationRodents; }
            set
            {
                revaccinationRodents = value;
                OnPropertyChanged("RevaccinationRodents");
            }
        }
        public Nullable<int> RevaccinationMeatEaters
        {
            get { return revaccinationMeatEaters; }
            set
            {
                revaccinationMeatEaters = value;
                OnPropertyChanged("RevaccinationMeatEaters");
            }
        }
        public Nullable<int> ProducerID
        {
            get { return producerID; }
            set
            {
                producerID = value;
                OnPropertyChanged("ProducerID");
            }
        }

        internal virtual Producer Producer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<Vaccination> Vaccinations { get; set; }

        public override List<DbEntity> GetForegnEntities()
        {
            var res = new List<DbEntity> { Producer };

            foreach (Vaccination item in Vaccinations)
            {
                res.Add(item);
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

        public override void CopyProperties(DbEntity toCopy)
        {
            var item = toCopy as Vaccine;

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                prop.SetValue(this, prop.GetValue(toCopy));
            }
        }

        public override string ToString()
        {
            return "Препарат";
        }

        public override string GetSearchString()
        {
            return $"{ActiveSubstanceName} {ActiveSubstanceLatinName} {producerID} {vaccineID} {vaccineName}";
        }
    }
}
