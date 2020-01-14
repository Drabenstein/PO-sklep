using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public class StatusZamowienia : IEntity
    {
        public int IdStatusu { get; set; }
        public string NazwaStatusu { get; set; }

        public IList<Zamowienie> Zamowienia { get; set; }
    }
}
