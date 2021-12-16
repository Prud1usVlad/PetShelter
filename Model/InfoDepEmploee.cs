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

    public partial class InfoDepEmploee : DbEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InfoDepEmploee()
        {
            this.Contracts = new HashSet<Contract>();
        }

        private int passNum;
        private string employment;
        private string email;
        private string phone;

        public int PassNum
        {
            get { return passNum; }
            set
            {
                passNum = value;
                OnPropertyChanged("PassNum");
            }
        }
        public string Employment
        {
            get { return employment; }
            set
            {
                employment = value;
                OnPropertyChanged("Employment");
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
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<Contract> Contracts { get; set; }
        internal virtual Emploee Emploee { get; set; }

        public override List<DbEntity> GetForegnEntities()
        {
            var res = new List<DbEntity> { Emploee };

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
            var item = toCopy as InfoDepEmploee;

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                prop.SetValue(this, prop.GetValue(toCopy));
            }
        }

        public override string ToString()
        {
            return "Працівник ВІ";
        }

        public override string GetSearchString()
        {
            return $"{PassNum} {Email} {Phone}";
        }

    }
}
