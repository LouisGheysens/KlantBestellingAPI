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
        public static KlantRESTOutputTDO MapFromKlantDomain(string url, Klant klant, BestellingManager bmanager) {
            try {
                string klantUrl = $"{url}/klant/{klant.KlantID}";
                List<Bestelling> bestellingen = bmanager.GetBestellingKlant(klant.KlantID);
                KlantRESTOutputTDO dto = new KlantRESTOutputTDO(klant.KlantID, klant.Naam, klant.Adres, bestellingen);
                return dto;
            }
            catch (Exception ex) {
                throw new MapException("MapFromDomain: MapFromKlantDomain - Gefaald!", ex);
            }
        }
    }
}