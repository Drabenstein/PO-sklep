using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class Pracownik
    {
        public int IdPracownika { get; set; }
        public string ImiePracownika { get; set; }
        public string NazwiskoPracownika { get; set; }
        public int IdStanowiska { get; set; }
        public int IdKonta { get; set; }
        public int? IdKategorii { get; set; }

        public virtual Kategoria IdKategoriiNavigation { get; set; }
        public virtual Konto IdKontaNavigation { get; set; }
        public virtual Stanowisko IdStanowiskaNavigation { get; set; }
    }
}
