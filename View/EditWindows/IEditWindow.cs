using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelter.Model;

namespace PetShelter.View.EditWindows
{
    interface IEditWindow
    {
        DbEntity Entity { get; }
    }
}
