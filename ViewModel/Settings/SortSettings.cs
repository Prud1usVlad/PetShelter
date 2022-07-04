using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.ViewModel.Settings
{
    class SortSettings
    {
        public string Criterion { get; set; }
        public int CriterionIndex { get; set; }
        public bool? SortAsc { get; set; }

        public SortSettings(string criterion, int index, bool? sortAsc)
        {
            Criterion = criterion;
            CriterionIndex = index;
            SortAsc = sortAsc;
        }
    }
}
