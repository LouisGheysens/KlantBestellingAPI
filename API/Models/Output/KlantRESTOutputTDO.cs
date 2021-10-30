using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Output {
    public class KlantRESTOutputTDO {

        public int KlantID { get; set; }

        public string Naam { get; set; }

        public string Adres { get; set; }

        private List<Bestelling> _Bestellingen = new List<Bestelling>();

        public KlantRESTOutputTDO(int KlantID, string naam, string adres, List<Bestelling> bestellingen) {

            this.KlantID = KlantID;
            Naam = naam;
            Adres = adres;
            this._Bestellingen = bestellingen;
        }
    }
}
