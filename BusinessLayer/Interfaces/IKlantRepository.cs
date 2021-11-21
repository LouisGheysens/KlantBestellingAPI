using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces {
    public interface IKlantRepository {
        Klant VoegKlantToe(Klant klant);
        bool BestaatKlantId(int id);
        void VerwijderKlant(int id);
        Klant UpdateKlant(Klant klant);
        Klant GetKlant(int id);
    }
}
