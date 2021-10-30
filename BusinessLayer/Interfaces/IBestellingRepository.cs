using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces {
    public interface IBestellingRepository {

        void VoegBestellingToe(Bestelling bestelling);
        bool BestaatBestelling(Bestelling bestelling);
        void VerwijderBestelling(Bestelling bestelling);
        void UpdateBestelling(Bestelling bestelling);
        void GetBestelling(int id);
        List<Bestelling> SelecteerBestellingen(int klantId);
        Klant GetBestellingFromSpecificKlant(Klant klant);
    }


}
