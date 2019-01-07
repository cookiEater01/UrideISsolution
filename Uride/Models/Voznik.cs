using System;
using System.Collections.Generic;

namespace Uride.Models
{
    public partial class Voznik
    {
        public Voznik()
        {
            Voznja = new HashSet<Voznja>();
        }

        public int StVozniske { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string Naslov { get; set; }
        public int AvtoId { get; set; }
        public string MobStev { get; set; }
        public bool Upokojen { get; set; }
        public string UpImeId { get; set; }

        public virtual Vozilo Avto { get; set; }
        public virtual ICollection<Voznja> Voznja { get; set; }
    }
}
