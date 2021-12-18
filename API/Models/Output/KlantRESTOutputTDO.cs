using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Output {
    public class KlantRESTOutputTDO {

 
        public string Id { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        public List<string> Bestellingen { get; set; }
    
    
        public KlantRESTOutputTDO(string id, string naam, string adres, List<string> bestellingen) {
            Id = id;
            Naam = naam;
            Adres = adres;
            Bestellingen = bestellingen;
        }
 
    }
}
