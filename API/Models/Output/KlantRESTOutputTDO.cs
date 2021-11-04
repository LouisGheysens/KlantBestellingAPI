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

        private List<Bestelling> _Bestellingen = new List<Bestelling>();

        public KlantRESTOutputTDO(int id, string naam, string adres) {
            this.KlantID = id;
            this.Naam = naam;
            this.Adres = adres;
        }

        public KlantRESTOutputTDO(int id, string naam, string adres, int aantalbestellingen, List<Bestelling> bestellingen): this(id, naam, adres) {
            this.AantalBestellingen = aantalbestellingen;
            this._Bestellingen = bestellingen;
        }
    }
}
