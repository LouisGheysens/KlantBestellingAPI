using API.Models.Output;
using BusinessLayer.Models;
using BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Exceptions;
using BusinessLayer;

namespace API.Mappers {
    public static class MapFromDomain {
        //Klant
        public static KlantRESTOutputTDO MapFromKlantDomain(string url, Klant klant, BestellingManager bmanager) {
            try {
                string klantURL = $"{url}/klant/{klant.KlantID}";
                List<Bestelling> bestellingen = bmanager.GetBestellingKlant(klant.KlantID);
                KlantRESTOutputTDO dto = new KlantRESTOutputTDO(klant.KlantID, klant.Naam, klant.Adres, bestellingen.Count(), bestellingen);
                return dto;
            }
            catch (Exception ex) {

                throw new MapException("MapFromklantDomain error", ex);
            }
        }

        //Bestellling
        public static BestellingRESTOutputTDO MapFromBestellingDomain(string url, Bestelling best, KlantManager klantmanager) {
            try {
                string bestelUrl = $"{url}/klant/{best.BestellingID}";
                List<Klant> klanten = klantmanager.SelecteerKlanten(best.Klant.KlantID);
                BestellingRESTOutputTDO dto = new BestellingRESTOutputTDO(best.BestellingID, best.Klant.KlantID, best.Aantal, best.Product);
                return dto;
            }
            catch (Exception ex) {

                throw new MapException("MapFromBestellingDomain Error", ex);
            }
        }
    }
}