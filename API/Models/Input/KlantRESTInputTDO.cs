using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Input {
    public class KlantRESTInputTDO {
        public int KlantID { get; private set; }

        public string Naam { get; private set; }

        public string Adres { get; private set; }

        public int AantalBestellingen { get; private set; }

        private List<Bestelling> _Bestellingen = new List<Bestelling>();
    }
}
