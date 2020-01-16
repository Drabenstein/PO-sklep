using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public class Payment : IEntity
    {
        public int IdPlatnosci { get; set; }
        public DateTime DataPlatnosci { get; set; }
        public int IdTypuPlatnosci { get; set; }

        public TypPlatnosci TypPlatnosci { get; set; }
        public IList<Zamowienie> Zamowienia { get; set; }
    }
}
