using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Output {
    public class KlantRESTOutputTDO {

        public int KlantID { get; private set; }

        public string Naam { get; private set; }

        public string Adres { get; private set; }

        public int AantalBestellingen { get; private set; }

        private List<string> _Bestellingen = new List<string>();

        public KlantRESTOutputTDO(int id, string naam, string adres, List<string> bestelling) {
            this.KlantID = id;
            this.Naam = naam;
            this.Adres = adres;
            this._Bestellingen = bestelling;
        }
    }
}
