using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web2020.Models
{
    public class Buss
    {
        public int Id { get; set; }
        public string reiserFra { get; set; }
        public string reserTil { get; set; }
        public string tidspunkten { get; set; }
        public string fornavn { get; set; }
        public string etternavn { get; set; }
        public string adresse { get; set; }
        public string telefonnr { get; set; }
    }
}
