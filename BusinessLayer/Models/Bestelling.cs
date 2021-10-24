using BusinessLayer.Enums;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer {
    public class Bestelling {
        public int BestellingID { get; private set; }

        public Bier Product { get; set; }

        public int Aantal { get; set; }

        public Klant Klant { get; set; }

        public Bestelling(int id, Bier product, int aantal, Klant k) {
            ZetId(id);
            this.Product = product;
            ZetAantal(aantal);
            ZetKlant(k);
        }
        public void ZetId(int id) {
            if (id <= 0) throw new BestellingException("Bestelling - ZetId - ID is 0!");
            this.BestellingID = id;
        }

        public void VerwijderKlant() {
            this.Klant = null;
        }

        public void ZetKlant(Klant newKlant) {
            if (newKlant == null) throw new KlantException("Bestelling - invalid klant");
            if (newKlant == Klant) throw new KlantException("Bestelling- ZetKlant - not new");
            if (Klant != null) {
                if (Klant.HeeftBestelling(this))
                    Klant.VerwijderBestelling(this);
                if (!newKlant.HeeftBestelling(this))
                    newKlant.VoegBestellingToe(this);
                Klant = newKlant;
            }
        }

        public void ZetAantal(int aantal) {
            if (aantal <= 0) throw new BestellingException("Bestelling - ZetAantal - Aantal is kleiner of gelijk aan 0");
            this.Aantal = aantal;
        }

        public override string ToString() {
            return $"ID: {BestellingID}\nProduct: {Product}\nKlant: {Klant}\n Drank: {Product}\nAantal: {Aantal}";
        }

        public override bool Equals(object obj) {
            return obj is Bestelling bestelling &&
                   BestellingID == bestelling.BestellingID &&
                   Product == bestelling.Product &&
                   Aantal == bestelling.Aantal &&
                   EqualityComparer<Klant>.Default.Equals(Klant, bestelling.Klant);
        }

        public override int GetHashCode() {
            return HashCode.Combine(BestellingID, Product, Aantal, Klant);
        }
    }
}
