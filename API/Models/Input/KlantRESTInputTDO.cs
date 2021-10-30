using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Input {
    public class KlantRESTInputTDO {
        public int KlantID { get; set; }

        public string Naam { get; set; }

        public string Adres { get; set; }

        private List<Bestelling> _Bestellingen = new List<Bestelling>();
    }
}
