using System;
using System.Collections.Generic;

namespace Uride.Models
{
    public partial class Stranka
    {
        public Stranka()
        {
            Voznja = new HashSet<Voznja>();
        }

        public int StrankaId { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string Naslov { get; set; }
        public string MobStev { get; set; }
        public string UpImeId { get; set; }

        public ICollection<Voznja> Voznja { get; set; }
    }
}
