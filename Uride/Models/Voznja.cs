using System;
using System.Collections.Generic;

namespace Uride.Models
{
    public partial class Voznja
    {
        public Voznja()
        {
            Transakcija = new HashSet<Transakcija>();
        }

        public int VoznjaId { get; set; }
        public int UporabnikId { get; set; }
        public int VoznikId { get; set; }
        public decimal DolzinaPoti { get; set; }

        public virtual Stranka Uporabnik { get; set; }
        public virtual Voznik Voznik { get; set; }
        public virtual ICollection<Transakcija> Transakcija { get; set; }
    }
}
