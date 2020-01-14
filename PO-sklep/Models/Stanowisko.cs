using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class Stanowisko
    {
        public Stanowisko()
        {
            Pracownik = new HashSet<Pracownik>();
        }

        public int IdStanowiska { get; set; }
        public string NazwaStanowiska { get; set; }

        public virtual ICollection<Pracownik> Pracownik { get; set; }
    }
}
