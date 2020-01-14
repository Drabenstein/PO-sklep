using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class StatusZamowienia
    {
        public StatusZamowienia()
        {
            Zamowienie = new HashSet<Zamowienie>();
        }

        public int IdStatusu { get; set; }
        public string NazwaStatusu { get; set; }

        public virtual ICollection<Zamowienie> Zamowienie { get; set; }
    }
}
