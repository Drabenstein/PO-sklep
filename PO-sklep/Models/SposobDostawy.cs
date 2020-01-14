using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public class SposobDostawy : IEntity
    {
        public int IdSposobuDostawy { get; set; }
        public string NazwaSposobuDostawy { get; set; }
        public decimal? CenaDostawy { get; set; }

        public IList<Zamowienie> Zamowienia { get; set; }
    }
}
