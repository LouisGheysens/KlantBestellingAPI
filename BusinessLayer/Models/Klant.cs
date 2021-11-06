﻿using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models {
    public class Klant {
        public int KlantID { get; private set; }

        public string Naam { get; private set; }

        public string Adres { get; private set; }

        private List<Bestelling> _bestellingen = new List<Bestelling>();

        public Klant(int id, string naam, string adres) {
            Zetid(id);
            ZetNaam(naam);
            ZetAdres(adres);
        }

        public Klant(string naam, string adres) {
            ZetNaam(naam);
            ZetAdres(adres);
        }
        public IReadOnlyList<Bestelling> ToonBestelling() {
            return _bestellingen;
        }

        public void Zetid(int id) {
            //if (id <= 0) throw new KlantException("Klant - ZetId - ID klopt niet!");
            this.KlantID = id;
        }

        public void ZetNaam(string naam) {
            //if (string.IsNullOrWhiteSpace(naam)) throw new KlantException("Klant - ZetNaam -  Naam heeft 0 karakters!");
            this.Naam = naam;
        }

        public void ZetAdres(string adres) {
            //if (string.IsNullOrWhiteSpace(adres) || adres.Length < 10) throw new KlantException("Klant - ZetAdres - Adres heeft te weinig karakters!");
            this.Adres = adres;
        }

        public override string ToString() {
            return $"{KlantID}\n{Naam}\n{Adres}";
        }

        public override bool Equals(object obj) {
            return obj is Klant klant &&
                   KlantID == klant.KlantID &&
                   Naam == klant.Naam &&
                   Adres == klant.Adres &&
                   EqualityComparer<List<Bestelling>>.Default.Equals(_bestellingen, klant._bestellingen);
        }

        public override int GetHashCode() {
            return HashCode.Combine(KlantID, Naam, Adres, _bestellingen);
        }
    }
}
