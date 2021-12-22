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
    using PetShelter.View.EditWindows;

    public partial class Client : DbEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Contracts = new HashSet<Contract>();
        }

        private int clientID;
        private string iDCardNum;
        private string iDCardSeries;
        private string firstName;
        private string secondName;
        private string thirdName;
        private Nullable<System.DateTime> dateOfAdding;
        private string region;
        private string city;
        private string street;
        private string buildingNum;
        private Nullable<int> flatNum;
        private string phone;
        private string email;

        public int ClientID
        {
            get { return clientID; }
            set
            {
                clientID = value;
                OnPropertyChanged("ClientID");
            }
        }
        public string IDCardNum
        {
            get { return iDCardNum; }
            set
            {
                iDCardNum = value;
                OnPropertyChanged("IDCardNum");
            }
        }
        public string IDCardSeries
        {
            get { return iDCardSeries; }
            set
            {
                iDCardSeries = value;
                OnPropertyChanged("IDCardSeries");
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string ThirdName
        {
            get { return thirdName; }
            set
            {
                thirdName = value;
                OnPropertyChanged("ThirdName");
            }
        }
        public Nullable<System.DateTime> DateOfAdding
        {
            get { return dateOfAdding; }
            set
            {
                dateOfAdding = value;
                OnPropertyChanged("DateOfAdding");
            }
        }
        public string Region
        {
            get { return region; }
            set
            {
                region = value;
                OnPropertyChanged("Region");
            }
        }
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }
        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                OnPropertyChanged("Street");
            }
        }
        public string BuildingNum
        {
            get { return buildingNum; }
            set
            {
                buildingNum = value;
                OnPropertyChanged("BuildingNum");
            }
        }
        public Nullable<int> FlatNum
        {
            get { return flatNum; }
            set
            {
                flatNum = value;
                OnPropertyChanged("FlatNum");
            }
        }
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        internal override Type WindowType => typeof(ClientEditWindow);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<Contract> Contracts { get; set; }

        public override List<DbEntity> GetForegnEntities()
        {
            var res = new List<DbEntity>();
            foreach (Contract item in Contracts)
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
            var item = toCopy as Client;

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                prop.SetValue(this, prop.GetValue(toCopy));
            }
        }

        public override string ToString()
        {
            return "Клієнт";
        }

        public override string GetSearchString()
        {
            return $"{ClientID} {IDCardNum} {IDCardSeries} {FirstName} {SecondName}" +
                $" {thirdName} {region} {city} {street} {buildingNum} {phone} {email} {flatNum}";
        }
    }
}
