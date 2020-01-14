using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class Klient
    {
        public Klient()
        {
            Opinia = new HashSet<Opinia>();
            Zamowienie = new HashSet<Zamowienie>();
        }

        public int IdKlienta { get; set; }
        public string ImieKlienta { get; set; }
        public string NazwiskoKlienta { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        public int? IdKonta { get; set; }

        public virtual Konto IdKontaNavigation { get; set; }
        public virtual ICollection<Opinia> Opinia { get; set; }
        public virtual ICollection<Zamowienie> Zamowienie { get; set; }
    }
}
