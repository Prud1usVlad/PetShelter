using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelter.Model;
using System.Reflection; 

namespace PetShelter.ViewModel
{
    public partial class Main_VM
    {
        private DbEntity Copy(DbEntity toCopy)
        {
            var type = toCopy.GetType();

            switch(type.Name)
            {
                case "Animal":
                    return CopyAnimal(toCopy);
                default:
                    return new DbEntity();
            }


        }

        private DbEntity CopyAnimal(DbEntity toCopy)
        {
            var res = new Animal();

            foreach (PropertyInfo prop in typeof(Animal).GetProperties())
            {
                prop.SetValue(res, prop.GetValue(toCopy));
            }

            res.Room = Rooms.Where(r => r.RoomID == res.RoomID).FirstOrDefault();
            return res;
        }

    }
}
