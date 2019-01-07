using System;
using System.Collections.Generic;

namespace Uride.Models
{
    public partial class Transakcija
    {
        public int StRacuna { get; set; }
        public decimal Znesek { get; set; }
        public int Idvoznje { get; set; }

        public Voznja IdvoznjeNavigation { get; set; }
    }
}
