using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers {
    public class BestellingManager {
        private IBestellingRepository repo;

        public BestellingManager(IBestellingRepository repo) { this.repo = repo; }

        public bool BestaatBestelling(Bestelling bestelling) {
            if (bestelling == null) throw new BestellingException("Klant - BestaatBestelling - Bestelling is null!");
            if (bestelling.BestellingID <= 0) return false;
            else
                return false;
        }

        public void GetBestelling(int id) {
            if (id <= 0) throw new BestellingException("Klant - GetBestelling - Bestelling is null");
            else
                repo.GetBestelling(id);
        }

        public Klant GetBestellingFromSpecificKlant(Klant klant) {
            return repo.GetBestellingFromSpecificKlant(klant);
        }

        public List<Bestelling> SelecteerBestellingen() {
            return repo.SelecteerBestellingen();
        }

        public void UpdateBestelling(Bestelling bestelling) {
            try {
                if (bestelling == null) throw new BestellingException("Klant - UpdateBestelling - Bestelling is null!");
                if (!repo.BestaatBestelling(bestelling)) throw new BestellingException("Klant - UpdateBestelling - Bestelling bestaat niet");
                else
                    repo.UpdateBestelling(bestelling);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public void VerwijderBestelling(Bestelling bestelling) {
            try {
                if (bestelling == null) throw new BestellingException("Klant - VerwijderBestellingToe - Bestelling is null!");
                if (!repo.BestaatBestelling(bestelling)) throw new BestellingException("Klant - VerwijderBestelling - Bestelling bestaat niet");
                else
                    repo.VerwijderBestelling(bestelling);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public void VoegBestellingToe(Bestelling bestelling) {
            try {
                if (bestelling == null) throw new BestellingException("Klant - VoegBestellingToe - Bestelling is null!");
                if (repo.BestaatBestelling(bestelling)) throw new BestellingException("Klant - VoegBestellingToe - Bestelling bestaat al");
                else
                    repo.VoegBestellingToe(bestelling);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
