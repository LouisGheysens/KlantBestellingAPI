using API.Models.Output;
using BusinessLayer.Models;
using BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Exceptions;

namespace API.Mappers {
    public static class MapFromDomain {
        public static KlantRESTOutputTDO MapFromKlantDomain(string url, Klant klant, KlantManager klantManager) {
            try {
                string klantUrl = $"{url}/api/klant/{klant.KlantID}";
                List<string> bestellingen = klantManager.GetBestellingFromSpecificKlant(klant).Select(x => x.klantUrl + $"/Bestelling/{x.BestellingId}");
                KlantRESTOutputTDO tdokl = new KlantRESTOutputTDO(klantUrl, klant.Naam, klant.Adres, bestellingen);
                return tdokl;
            }catch(Exception ex) {
                throw new MapException("MapException: MapFromKlantDomain - task failed", ex);
            }
        }
    }
}
