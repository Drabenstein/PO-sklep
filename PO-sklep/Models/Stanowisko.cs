using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public class Stanowisko : IEntity
    {
        public int IdStanowiska { get; set; }
        public string NazwaStanowiska { get; set; }

        public IList<Pracownik> Pracownicy { get; set; }
    }
}
