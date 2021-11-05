using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.Generic;

namespace PetShelter.Model
{
    public class DbEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public virtual List<DbEntity> GetForegnEntities()
        {
            return null;
        }

        public virtual Dictionary<string, string> GetProperies()
        {
            return null;
        }
    }
}
