using API.Exceptions;
using API.Models.Input;
using API.Models.Output;
using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers {
    public class MapToDomain {

        //Klant
        public static Klant MapToKlantDomain(KlantRESTInputTDO tdo) {
            try {
                if (tdo.ID > 0) return new Klant(tdo.ID, tdo.Naam, tdo.Adres);
                return new Klant(tdo.Naam, tdo.Adres);
                 
            }catch(Exception ex) {
                throw new MapException("MapToDomain: MapToKlantDomain - gefaald!", ex);
            }
        }

        //Bestelling
        public static Bestelling MapToBestellingDomain(BestellingRESTInputTDO tdo, Klant k) {
            try {
                Bestelling bestelling = new(tdo.Product, tdo.Aantal, k);
                return bestelling;
            }
            catch (Exception ex) {
                throw new MapException("MapToDomain: MapToBestellingDomain - gefaald!", ex);
            }
        }
    }
}
