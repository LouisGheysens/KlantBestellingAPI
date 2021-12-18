using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces {
    public interface IBestellingRepository {

        Bestelling VoegBestellingToe(Bestelling bestelling);
        void VerwijderBestelling(int id);
        void UpdateBestelling(Bestelling bestelling);
        IEnumerable<Bestelling> GetBestellingKlant(int BestellingId);
        bool BestaatBestellingBijKlant(int BestellingId);
        Bestelling GeefBestellingWeer(int id, int klantID);
        bool BestaatBestelling(Bestelling bestelling);
        bool BestaatBestelling(int id);
        bool HeeftBestelling(int id);
        bool HeeftBestellingenKlant(int id);
    }


}
