using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class Konto
    {
        public int IdKonta { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }

        public virtual Klient Klient { get; set; }
        public virtual Pracownik Pracownik { get; set; }
    }
}
