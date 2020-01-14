using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class Kategoria
    {
        public Kategoria()
        {
            InverseIdNadkategoriiNavigation = new HashSet<Kategoria>();
            Pracownik = new HashSet<Pracownik>();
            Produkt = new HashSet<Produkt>();
        }

        public int IdKategorii { get; set; }
        public string NazwaKategorii { get; set; }
        public int? IdNadkategorii { get; set; }

        public virtual Kategoria IdNadkategoriiNavigation { get; set; }
        public virtual ICollection<Kategoria> InverseIdNadkategoriiNavigation { get; set; }
        public virtual ICollection<Pracownik> Pracownik { get; set; }
        public virtual ICollection<Produkt> Produkt { get; set; }
    }
}
