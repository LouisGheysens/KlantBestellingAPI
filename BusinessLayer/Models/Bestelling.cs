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
        }


        public Bestelling(Bier product, int aantal, Klant k) {
            this.Product = product;
            ZetAantal(aantal);
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
