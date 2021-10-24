using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers {
    public class BestellingManager : IBestellingRepository {
        private IBestellingRepository repo;

        public BestellingManager(IBestellingRepository repo) { this.repo = repo; }

        public bool BestaatKlant(Klant klant) {
            if (klant.KlantID <= 0) {
                return false;
            }
            else {
                return true;
            }
        }

        public void GetKlant(int id) {
            if (id <= 0) throw new KlantException("Bestelling - GetKlant - Klant is null");
            else
                GetKlant(id);
        }

        public List<Klant> SelecteerKlanten() {
            return repo.SelecteerKlanten();
        }

        public void UpdateKlant(Klant klant) {
            if (klant == null) throw new KlantException("Bestelling - UpdateKlant - Klant is null");
            if (!repo.BestaatKlant(klant)) throw new KlantException("Bestelling - UpdateKlant - Klant bestaat niet");
            else
                repo.UpdateKlant(klant);
        }

        public void VerwijderKlant(Klant klant) {
            if (klant == null) throw new KlantException("Bestelling - VerwijderenKlant - Klant is null");
            if (!repo.BestaatKlant(klant)) throw new KlantException("Bestelling - VerwijderenKlant - Klant bestaat niet");
            else
                repo.VerwijderKlant(klant);
        }

        public void VoegKlantToe(Klant klant) {
            try {
                if (klant == null) throw new KlantException("Bestelling - VoegKlantToe - Klant is null");
                if (repo.BestaatKlant(klant)) throw new KlantException("Bestelling - VoegKlantToe - Klant bestaat al");
                else {
                    repo.VoegKlantToe(klant);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
