using API.Models.Output;
using BusinessLayer.Models;
using BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Exceptions;
using BusinessLayer;
using DataLayer.Repos;

namespace API.Mappers {
    public static class MapFromDomain {
        //Klant
        public static KlantRESTOutputTDO MapFromKlantDomain(string url, Klant klant, BestellingManager bmanager) {
            try {
                string klantURL = $"{url}/klant/{klant.KlantID}";
                List<string> bestellingen = bmanager.GetBestellingKlant(klant.KlantID).Select(x => klantURL + $"/Bestelling/{x.BestellingID}").ToList(); ;
                KlantRESTOutputTDO klantREST = new(klantURL, klant.Naam, klant.Adres, bestellingen);
                return klantREST;
            }
            catch (Exception ex) {

                throw new MapException("MapFromklantDomain error", ex);
            }
        }


        public static BestellingRESTOutputTDO MapFromBestellingDomain(string url, Bestelling beste) {
            try {
                string Klanturl = $"{ url}/klant/{beste.Klant.KlantID}";
                string bestellingUrl = Klanturl + $"/Bestelling/{beste.BestellingID}";
                BestellingRESTOutputTDO bestellingRESTOutputDTO = new(bestellingUrl, Klanturl, beste.Product.ToString(), beste.Aantal);
                return bestellingRESTOutputDTO;
            }
            catch (Exception ex) {

                throw new MapException("MapFromBestellingDomain Error", ex);
            }
        }
    }
}