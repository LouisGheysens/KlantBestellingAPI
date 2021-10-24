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

        private List<Bestelling> _bestellingen = new List<Bestelling>();

        public Klant(int id, string naam, string adres) {
            Zetid(id);
            ZetNaam(naam);
            ZetAdres(adres);
        }


        public IReadOnlyList<Bestelling> ToonBestelling() {
            return _bestellingen;
        }

        public void Zetid(int id) {
            if (id <= 0) throw new KlantException("Klant - ZetId - ID is geen 0!");
            this.KlantID = id;
        }

        public void ZetNaam(string naam) {
            if (string.IsNullOrEmpty(naam)) throw new KlantException("Klant - ZetNaam -  Naam heeft 0 karakters!");
            this.Naam = naam;
        }

        public bool HeeftBestelling(Bestelling bestelling) {
            if (_bestellingen.Contains(bestelling)) return true;
            else return false;
        }

        public void ZetAdres(string adres) {
            if (string.IsNullOrWhiteSpace(adres) || adres.Length < 10) throw new KlantException("Klant - ZetAdres - Adres heeft te weinig karakters!");
            this.Adres = adres;
        }

        public void VoegBestellingToe(Bestelling bestelling) {
            if (bestelling == null) throw new KlantException("Klant: VerwijderBestelling - bestelling is null");
            if (_bestellingen.Contains(bestelling)) {
                throw new KlantException("Klant: VoegBestellinToe - bestelling allready exists");
            }
            else {
                _bestellingen.Add(bestelling);
                if (bestelling.Klant != this) {
                    bestelling.ZetKlant(this);
                }
            }
        }

        public void VerwijderBestelling(Bestelling bestelling) {
            if (bestelling == null) throw new KlantException("Klant: BestellingVerwijderen - bestelling is null");
            else if (_bestellingen.Contains(bestelling)) throw new KlantException("Klant: BestellingVerwijderen - bestelling bestaat niet!");
            else
                _bestellingen.Remove(bestelling);
        }

        public override string ToString() {
            return $"{KlantID}\n{Naam}\n{Adres}";
        }

        public override bool Equals(object obj) {
            return obj is Klant klant &&
                   KlantID == klant.KlantID &&
                   Naam == klant.Naam &&
                   Adres == klant.Adres &&
                   EqualityComparer<List<Bestelling>>.Default.Equals(_bestellingen, klant._bestellingen);
        }

        public override int GetHashCode() {
            return HashCode.Combine(KlantID, Naam, Adres, _bestellingen);
        }
    }
}
