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
                Klant klant = new Klant(tdo.Naam, tdo.Adres);
                return klant;
            }catch(Exception ex) {
                throw new MapException("MapToDomain: MapToKlantDomain - gefaald!", ex);
            }
        }

        //Bestelling
        public static Bestelling MapToBestellingDomain(BestellingRESTInputTDO tdo, Klant k) {
            try {
                Bestelling bestelling = new Bestelling(tdo.Product, tdo.Aantal, k);
                return bestelling;
            }
            catch (Exception ex) {
                throw new MapException("MapToDomain: MapToBestellingDomain - gefaald!", ex);
            }
        }
    }
}
