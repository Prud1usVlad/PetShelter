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
    
    public partial class Vaccinations
    {
        public int VaccineID { get; set; }
        public int AnimalID { get; set; }
        public System.DateTime VaccinationDate { get; set; }
    
        public virtual Animals Animals { get; set; }
        public virtual Vaccines Vaccines { get; set; }
    }
}
