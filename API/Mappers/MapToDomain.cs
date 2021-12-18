using API.Exceptions;
using API.Models.Input;
using API.Models.Output;
using BusinessLayer;
using BusinessLayer.Enums;
using BusinessLayer.Managers;
using BusinessLayer.Models;
using DataLayer.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers {
    public class MapToDomain {

        //Klant
        public static Klant MapToKlantDomain(KlantRESTInputTDO tdo) {
            try {
                Klant k = new Klant(tdo.Naam, tdo.Adres);
                return k;
                 
            }catch(Exception ex) {
                throw new MapException("MapToDomain: MapToKlantDomain - gefaald!", ex);
            }
        }

        //Bestelling
        public static Bestelling MapToBestellingDomain(BestellingRESTInputTDO tdo, Klant k) {
            try {
                Bestelling bes = new (tdo.Product, tdo.Aantal, k);
                return bes;
            }
            catch (Exception ex) {
                throw new MapException("MapToDomain: MapToBestellingDomain - gefaald!", ex);
            }
        }
    }
}
