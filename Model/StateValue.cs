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
    
    public partial class StateValue : DbEntity
    {
        public int StateValueID { get; set; }
        public Nullable<int> StateID { get; set; }
        public Nullable<int> AnimalID { get; set; }
        public string Value { get; set; }
    
        internal virtual Animal Animal { get; set; }
        internal virtual State State { get; set; }

        public override List<DbEntity> GetForegnEntities()
        {
            return new List<DbEntity> { State };
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
            return "StateValue";
        }
    }
}