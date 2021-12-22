using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelter.Model;
using System.Data.Entity;

namespace PetShelter.ViewModel.EditWindowsVM
{
    public class ClientEdit_VM : StandartViewModel
    {
        public Client Client{ get; set; }

        public ClientEdit_VM(Client c)
        {
            Client = c;
        }
    }
}
