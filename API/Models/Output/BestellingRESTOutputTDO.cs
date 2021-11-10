using BusinessLayer.Enums;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Output {
    public class BestellingRESTOutputTDO {
        public int BestellingID { get; set; }

        public Klant Klant { get; private set; }

        public Bier Product { get; private set; }

        public int Aantal { get; private set; }


        public BestellingRESTOutputTDO(int bestellingID, Klant klant, Bier product, int aantal) {
            this.BestellingID = bestellingID;
            this.Klant = klant;
            this.Product = product;
            this.Aantal = aantal;
        }
    }
}
