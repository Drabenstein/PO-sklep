using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public class TypPlatnosci : IEntity
    {
        public int IdTypuPlatnosci { get; set; }
        public string NazwaTypuPlatnosci { get; set; }

        public IList<Payment> Platnosci { get; set; }
    }
}