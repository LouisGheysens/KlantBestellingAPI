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
        List<Bestelling> GetBestellingKlant(int id);
        bool BestaatBestelling(int id);
    }


}
