using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class SposobDostawy
    {
        public SposobDostawy()
        {
            Zamowienie = new HashSet<Zamowienie>();
        }

        public int IdSposobuDostawy { get; set; }
        public string NazwaSposobuDostawy { get; set; }
        public decimal? CenaDostawy { get; set; }

        public virtual ICollection<Zamowienie> Zamowienie { get; set; }
    }
}
