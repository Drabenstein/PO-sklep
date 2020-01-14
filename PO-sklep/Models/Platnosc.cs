using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class Platnosc
    {
        public Platnosc()
        {
            Zamowienie = new HashSet<Zamowienie>();
        }

        public int IdPlatnosci { get; set; }
        public DateTime DataPlatnosci { get; set; }
        public int IdTypuPlatnosci { get; set; }

        public virtual TypPlatnosci IdTypuPlatnosciNavigation { get; set; }
        public virtual ICollection<Zamowienie> Zamowienie { get; set; }
    }
}
