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
            if (bestelling == null) throw new BestellingException("BestellingManager - BestaatBestelling - Bestelling is null!");
            if (bestelling.BestellingID <= 0) return false;
            else
                return false;
        }

        public List<Bestelling> GetBestellingKlant(int id) {
            try {
                if (id <= 0) throw new BestellingException("BestellingManager: GetBestellingKlant - Id is kleiner of gelijk aan 0!");
                return repo.GetBestellingKlant(id);
            }catch(Exception ex) {
                throw new BestellingException("BestellingManager: GetBestellingKlant(id) - gefaald", ex);
            }
        }

        public void UpdateBestelling(Bestelling bestelling) {
            try {
                if (bestelling == null) throw new BestellingException("BestellingManager - UpdateBestelling - Bestelling is null!");
                if (!repo.BestaatBestelling(bestelling)) throw new BestellingException("BestellingManager - UpdateBestelling - Bestelling bestaat niet");
                else
                    repo.UpdateBestelling(bestelling);
            }
            catch (Exception ex) {
                throw new BestellingException("BestellingManager: UpdateBestelling - gefaald", ex);
            }
        }

        public void VerwijderBestelling(Bestelling bestelling) {
            try {
                if (bestelling == null) throw new BestellingException("BestellingManager - VerwijderBestellingToe - Bestelling is null!");
                if (!repo.BestaatBestelling(bestelling)) throw new BestellingException("BestellingManager - VerwijderBestelling - Bestelling bestaat niet");
                else
                    repo.VerwijderBestelling(bestelling);
            }
            catch (Exception ex) {
                throw new BestellingException("BestellingManager: VerwijderBestelling - gefaald", ex);
            }
        }

        public void VoegBestellingToe(Bestelling bestelling) {
            try {
                if (bestelling == null) throw new BestellingException("BestellingManager - VoegBestellingToe - Bestelling is null!");
                if (repo.BestaatBestelling(bestelling)) throw new BestellingException("BestellingManager - VoegBestellingToe - Bestelling bestaat al");
                else
                    repo.VoegBestellingToe(bestelling);
            }
            catch (Exception ex) {
                throw new BestellingException("BestellingManager: VoegBestellingToe - gefaald", ex);
            }
        }
    }
}
