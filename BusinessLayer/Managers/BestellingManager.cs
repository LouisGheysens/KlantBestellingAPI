using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers {
    public class BestellingManager : IBestellingRepository {
        private IBestellingRepository repo;

        public BestellingManager(IBestellingRepository repo) { this.repo = repo; }

        public bool BestaatBestelling(Bestelling bestelling) {
            if (bestelling == null) throw new BestellingException("Bestelling - BestaatBestelling - Bestelling is null!");
            if (bestelling.BestellingID <= 0) return false;
            else
                return false;
        }

        public void GetBestelling(int id) {
            if (id <= 0) throw new BestellingException("Bestelling - GetBestelling - Bestelling is null");
            else
                repo.GetBestelling(id);
        }

        public List<Bestelling> SelecteerBestellingen() {
            return repo.SelecteerBestellingen();
        }

        public void UpdateBestelling(Bestelling bestelling) {
            try {
                if (bestelling == null) throw new BestellingException("Bestelling - UpdateBestelling - Bestelling is null!");
                if (!repo.BestaatBestelling(bestelling)) throw new BestellingException("Bestelling - UpdateBestelling - Bestelling bestaat niet");
                else
                    repo.UpdateBestelling(bestelling);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public void VerwijderBestelling(Bestelling bestelling) {
            try {
                if (bestelling == null) throw new BestellingException("Bestelling - VerwijderBestellingToe - Bestelling is null!");
                if (!repo.BestaatBestelling(bestelling)) throw new BestellingException("Bestelling - VerwijderBestelling - Bestelling bestaat niet");
                else
                    repo.VerwijderBestelling(bestelling);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public void VoegBestellingToe(Bestelling bestelling) {
            try {
                if (bestelling == null) throw new BestellingException("Bestelling - VoegBestellingToe - Bestelling is null!");
                if (repo.BestaatBestelling(bestelling)) throw new BestellingException("Bestelling - VoegBestellingToe - Bestelling bestaat al");
                else
                    repo.VoegBestellingToe(bestelling);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
