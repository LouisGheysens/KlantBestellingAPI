using System;
using BusinessLayer.Models;
using BusinessLayer.Interfaces;
using DataLayer.Repos;
using BusinessLayer;

namespace App {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            #region SQLQUERIES

            //Klanten
            Klant k = new Klant("Luc", "Sint-HubertusWegel-78-Sint-Niklaas");
            KlantRepository kr = new KlantRepository();
            //kr.VoegKlantToe(k);
            //kr.VerwijderKlant(k);
            //kr.UpdateKlant(k);
            //kr.SelecteerKlanten();
            //kr.BestaatKlant(k);

            //Bestellingen
            BestellingRepository br = new BestellingRepository();
            Bestelling b = new Bestelling(BusinessLayer.Enums.Bier.Orval, 4, new Klant("Luigi", "Patershol-3-Gent"));
            Bestelling bt = new Bestelling(BusinessLayer.Enums.Bier.Orval, 4, new Klant("Patje", "Korenlei-56-Zottegem"));
            //br.VoegBestellingToe(b);
            br.BestaatBestelling(bt);
            //br.VoegBestellingToe(bt);
            //br.VerwijderBestelling(b);
            //br.UpdateBestelling();
            //br.GetBestelling();
            //br.SelecteerBestellingen()


            #endregion
        }
    }
}
