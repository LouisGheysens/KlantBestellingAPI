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
        public int BestellingID { get; set; }

        public Bier Product { get; set; }

        public int Aantal { get; set; }

        public Klant Klant { get; set; }

        public Bestelling(int id, int product, int aantal, Klant klant) : this(product, aantal, klant) {
            ZetId(id);
        }
        public Bestelling(int product, int aantal, Klant klant) {
            ZetProduct(product);
            ZetAantal(aantal);
            ZetKlant(klant);
        }


        public void VerwijderKlant() {
            Klant = null;
        }
        public void ZetKlant(Klant newKlant) {

            if (newKlant == null) throw new BestellingException("Bestelling - invalid klant");
            if (newKlant == Klant) throw new BestellingException("Bestelling - ZetKlant - not new");
            if (Klant != null)
                if (Klant.HeeftBestelling(this))
                    Klant.VerwijderBestelling(this);
            if (!newKlant.HeeftBestelling(this))
                newKlant.VoegToeBestelling(this);
            Klant = newKlant;
        }

        public void ZetProduct(int product) {
            if(!Enum.IsDefined(typeof(Bier), (Bier)product)) {
                throw new BestellingException("Bestelling: ZetProduct - gefaald");
            }
            Product = (Bier)product;
        }
        public void ZetId(int id) {
            if (id <= 0) throw new BestellingException("Bestelling - ZetId - ID is 0!");
            this.BestellingID = id;
        }

        public void ZetAantal(int aantal) {
            if (aantal <= 0) throw new BestellingException("Bestelling - ZetAantal - Aantal is kleiner of gelijk aan 0");
            this.Aantal = aantal;
        }

        public override string ToString() {
            return $"ID: {BestellingID}\nKlant: {Klant}\n Drank: {Product}\nAantal: {Aantal}";
        }
    }
}
