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

    public partial class Emploee : DbEntity
    {
        private int passNum;
        private string iDCardNum;
        private string iDCardSeries;
        private string secondName;
        private string firstName;
        private string thirdName;
        private Nullable<System.DateTime> entryDate;
        private string profession;

        public int PassNum
        {
            get { return passNum; }
            set
            {
                passNum = value;
                OnPropertyChanged("PassNum");
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
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged("SecondName");
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
        public string ThirdName
        {
            get { return thirdName; }
            set
            {
                thirdName = value;
                OnPropertyChanged("ThirdName");
            }
        }
        public Nullable<System.DateTime> EntryDate
        {
            get { return entryDate; }
            set
            {
                entryDate = value;
                OnPropertyChanged("EntryDate");
            }
        }
        public string Profession
        {
            get { return profession; }
            set
            {
                profession = value;
                OnPropertyChanged("Profession");
            }
        }

        internal virtual Caretaker Caretaker { get; set; }
        internal virtual InfoDepEmploee InfoDepEmploee { get; set; }

        public override List<DbEntity> GetForegnEntities()
        {
            return (profession == "Доглядач") ? new List<DbEntity> { Caretaker } : new List<DbEntity> { InfoDepEmploee };
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
            var item = toCopy as Emploee;

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                prop.SetValue(this, prop.GetValue(toCopy));
            }
        }

        public override string ToString()
        {
            return "Працівник";
        }

        public override string GetSearchString()
        {
            return $"{PassNum} {IDCardNum} {IDCardSeries} {secondName} {firstName} {thirdName} {profession}";
        }

    }
}
