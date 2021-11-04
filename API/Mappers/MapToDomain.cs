using API.Exceptions;
using API.Models.Output;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers {
    public class MapToDomain {
        //PROPERTIES VAN RESTOUTPUTTDO WORDEN HIERIN VERWERKT!!!
        public static Klant MapToKlantDomain(KlantRESTOutputTDO tdo) {
            try {
                Klant klant = new Klant(tdo.Naam, tdo.Adres);
                return klant;
            }catch(Exception ex) {
                throw new MapException("MapToDomain: MapToKlantDomain - gefaald!");
            }
        }
    }
}
