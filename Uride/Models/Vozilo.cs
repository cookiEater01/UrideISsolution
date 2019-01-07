using System;
using System.Collections.Generic;

namespace Uride.Models
{
    public partial class Vozilo
    {
        public Vozilo()
        {
            Voznik = new HashSet<Voznik>();
        }

        public int AvtoId { get; set; }
        public string Znamka { get; set; }
        public string Model { get; set; }
        public double CenaKm { get; set; }
        public int StSedezev { get; set; }

        public virtual ICollection<Voznik> Voznik { get; set; }
    }
}
