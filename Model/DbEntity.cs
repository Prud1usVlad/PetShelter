using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace PetShelter.Model
{
    public class DbEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal virtual Type WindowType { get; private set; }
        internal virtual int Identifier { get; private set; }

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

        public virtual void CopyProperties(DbEntity toCopy)
        {
            return;
        }

        public virtual string GetSearchString()
        {
            return "empty";
        }
    }
}
