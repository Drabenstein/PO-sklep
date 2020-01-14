using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class Faktura
    {
        public int IdFaktury { get; set; }
        public string SeriaFaktury { get; set; }
        public DateTime DataWystawienia { get; set; }
        public int IdZamowienia { get; set; }

        public virtual Zamowienie IdZamowieniaNavigation { get; set; }
    }
}
