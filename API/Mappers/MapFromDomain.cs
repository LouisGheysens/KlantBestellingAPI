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
                //List<string> bestellingen = bmanager.GetBestellingKlant(klant.KlantID).Select(x => klantURL = $"/Bestelling/{x.BestellingID}").ToList(); //oud
                List<string> bestellingen = bmanager.GetBestellingKlant(klant.KlantID).Select(x =>  $"{klantURL}/Bestelling/{x.BestellingID}").ToList();
                KlantRESTOutputTDO klantREST = new(klantURL, klant.Naam, klant.Adres, bestellingen);
                return klantREST;
            }
            catch (Exception ex) {

                throw new MapException("MapFromklantDomain error", ex);
            }
        }

        //Bestellling
        public static BestellingRESTOutputTDO MapFromBestellingDomain(string url, Bestelling best) {
            try {
                string klantUrl = $"{url}/klant/{best.Klant.KlantID}";
                string bestelUrl = klantUrl + $"/Bestelling/{best.BestellingID}";
                BestellingRESTOutputTDO bestelREST = new(bestelUrl, klantUrl, best.Product.ToString(), best.Aantal);
                return bestelREST;
            }
            catch (Exception ex) {

                throw new MapException("MapFromBestellingDomain Error", ex);
            }
        }
    }
}