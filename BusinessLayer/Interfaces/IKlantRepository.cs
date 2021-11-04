using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces {
    public interface IKlantRepository {
        Klant VoegKlantToe(Klant klant);
        bool BestaatKlant(Klant klant);
        bool BestaatKlantId(int id);
        void VerwijderKlant(Klant klant);
        void UpdateKlant(Klant klant);
        Klant GetKlant(int id);
        List<Klant> SelecteerKlanten();
    }
}
