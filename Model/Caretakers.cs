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
    
    public partial class Caretakers
    {
        public int PassNum { get; set; }
        public string IDCardNum { get; set; }
        public string IDCardSeries { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string ThirdName { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Shift { get; set; }
        public string MajorAnimalKind { get; set; }
        public string MedicalEducation { get; set; }
        public Nullable<int> RoomID { get; set; }
    
        public virtual Rooms Rooms { get; set; }
    }
}
