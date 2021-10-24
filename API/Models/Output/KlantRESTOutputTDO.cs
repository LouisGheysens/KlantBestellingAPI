using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Output {
    public class KlantRESTOutputTDO {
        public KlantRESTOutputTDO(int KlantID, string naam, string adres, List<string> Bestelling) {

            KlantId = KlantID;
            Naam = naam;
            Adres = adres;
            Bestellingen = Bestelling;
        }

        public int KlantId { get; set; }

        public string Naam { get; set; }

        public string Adres { get; set; }

        public List<string> Bestellingen;
    }
}
