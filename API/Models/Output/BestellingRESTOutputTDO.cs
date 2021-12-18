using BusinessLayer.Enums;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Output {
    public class BestellingRESTOutputTDO {

        public string Id { get; set; }
        public string KlantId { get; set; }
        public string Product { get; set; }
        public int Aantal { get; set; }


      
        public BestellingRESTOutputTDO(string id, string klantId, string product, int aantal) {
            Id = id;
            KlantId = klantId;
            Product = product;
            Aantal = aantal;
        }
    }
}
