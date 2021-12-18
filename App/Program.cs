using System;
using BusinessLayer.Models;
using BusinessLayer.Interfaces;
using DataLayer.Repos;
using BusinessLayer;

namespace App {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            string conn = @"Data Source=.\SQLEXPRESS;Initial Catalog=TestDb;Integrated Security=True";
            #region SQLQUERIES

            //Klanten
            Klant k = new Klant(1, "Luc", "Sint-HubertusWegel-78-Sint-Niklaas");
            KlantRepository kr = new KlantRepository(conn);
            //kr.VoegKlantToe(k); //Klopt
            //kr.VerwijderKlant(13); //Klopt
            //kr.UpdateKlant(k);
            //kr.BestaatKlant(k.KlantId);
            //var x = kr.GetKlant(1);
            //Console.WriteLine(x.ToString());

            //Bestellingen
            BestellingRepository br = new BestellingRepository(conn);
            Bestelling b = new Bestelling(BusinessLayer.Enums.Bier.Orval, 4, new Klant("Luigi", "Patershol-3-Gent"));
            Bestelling bt = new Bestelling(BusinessLayer.Enums.Bier.Orval, 4, new Klant("Patje", "Korenlei-56-Zottegem"));
            br.VoegBestellingToe(b);
            ////br.BestaatBestelling(bt);
            ////br.VoegBestellingToe(bt);
            ////br.VerwijderBestelling(b);
            ////br.UpdateBestelling();
            ////br.GetBestelling();
            ////br.SelecteerBestellingen()


            #endregion
        }
    }
}
