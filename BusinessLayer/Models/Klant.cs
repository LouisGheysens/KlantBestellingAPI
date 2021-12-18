using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models {
    public class Klant {
        public int KlantID { get; private set; }

        public string Naam { get; private set; }

        public string Adres { get; private set; }

        List<Bestelling> _bestellingen = new List<Bestelling>();

        public Klant(int id, string naam, string adres, List<Bestelling> b) {
            this._bestellingen = b;
            Zetid(id);
            ZetNaam(naam);
            ZetAdres(adres);
        }
        public Klant(string naam, string adres, List<Bestelling> b) {
            this._bestellingen = b;
            ZetNaam(naam);
            ZetAdres(adres);
        }
        public Klant(int id, string naam, string adres) {
            Zetid(id);
            ZetNaam(naam);
            ZetAdres(adres);
        }
        public Klant(string naam, string adres) {
            ZetNaam(naam);
            ZetAdres(adres);
        }
        public void Zetid(int id) {
            if (id <= 0) throw new KlantException("Klant - ZetId - ID klopt niet!");
            this.KlantID = id;
        }

        public void ZetNaam(string naam) {
            if (string.IsNullOrWhiteSpace(naam)) throw new KlantException("Klant - ZetNaam -  Naam heeft 0 karakters!");
            this.Naam = naam;
        }

        public void ZetAdres(string adres) {
            if (string.IsNullOrWhiteSpace(adres) || adres.Length < 10) throw new KlantException("Klant - ZetAdres - Adres heeft te weinig karakters!");
            this.Adres = adres;
        }

        public override string ToString() {
            return $"{KlantID}\n{Naam}\n{Adres}";
        }


        public void VerwijderBestelling(Bestelling bestelling) {
            if (bestelling == null) throw new KlantException("Klant : VerwijderBestelling - bestelling is null");
            if (!_bestellingen.Contains(bestelling)) {
                throw new KlantException("Klant : RemoveBestelling - bestelling does not exists");
            }
            else {
                if (bestelling.Klant == this)
                    bestelling.VerwijderKlant();
            }
        }

        public void VoegToeBestelling(Bestelling bestelling) {
            if (bestelling == null) throw new KlantException("Klant : VerwijderBestelling - bestelling is null");
            if (_bestellingen.Contains(bestelling)) {
                throw new KlantException("Klant : AddBestelling - bestelling already exists");
            }
            else {
                _bestellingen.Add(bestelling);
                if (bestelling.Klant != this)
                    bestelling.ZetKlant(this);
            }
        }
        public bool HeeftBestelling(Bestelling bestelling) {
            if (_bestellingen.Contains(bestelling)) return true;
            else return false;
        }
    }
}
