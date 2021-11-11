using BusinessLayer.Enums;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Output {
    public class BestellingRESTOutputTDO {
        public string BestellingID { get; set; }

        public string KlantId { get; private set; }

        public string Product { get; private set; }

        public int Aantal { get; private set; }


        public BestellingRESTOutputTDO(string bestellingID, string klant, string product, int aantal) {
            this.BestellingID = bestellingID;
            this.KlantId = klant;
            this.Product = product;
            this.Aantal = aantal;
        }
    }
}
