using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class TypPlatnosci
    {
        public TypPlatnosci()
        {
            Platnosc = new HashSet<Platnosc>();
        }

        public int IdTypuPlatnosci { get; set; }
        public string NazwaTypuPlatnosci { get; set; }

        public virtual ICollection<Platnosc> Platnosc { get; set; }
    }
}
