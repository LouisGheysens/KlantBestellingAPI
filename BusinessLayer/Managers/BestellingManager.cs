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
            return repo.BestaatBestelling(bestelling);
        }

        public IEnumerable<Bestelling> GetBestellingKlant(int id) {
            try {
                if (id <= 0) throw new BestellingException("BestellingManager: GetBestellingKlant - Id is kleiner of gelijk aan 0!");
                return repo.GetBestellingKlant(id);
            }
            catch (Exception ex) {
                throw new BestellingException("BestellingManager: GetBestellingKlant(id) - gefaald", ex);
            }
        }


        public Bestelling GeefBestellingWeer(int id, int klantID) {
            try {
                return repo.GeefBestellingWeer(id, klantID);
            }catch(Exception ex) {
                throw new BestellingException("BestellingManager: GeefBestellingWeer(id) - failed", ex);
            }
        }

        public bool BestaatBestellingBijKlant(int id) {
            try {
                return repo.BestaatBestellingBijKlant(id);
            }catch(Exception ex) {
                throw new BestellingException("BestellingManager: BetstaatBestellingBijKlant(id) - failed", ex);
            }
        }


        public Bestelling UpdateBestelling(int id, Bestelling bestelling) {
            if (bestelling == null) {
                throw new BestellingException("Bestelling is null.");
            }
            if (!repo.BestaatBestellingBijKlant(bestelling.BestellingID)) {
                throw new BestellingException("Bestelling bestaat niet.");
            }
            Bestelling bestellingDb = GeefBestellingWeer(id, bestelling.BestellingID);
            if (bestellingDb == bestelling) {
                throw new BestellingException("Er zijn geen verschillen met het origineel.");
            }
            repo.UpdateBestelling(bestelling);
            return bestelling;
        }

        public void VerwijderBestelling(int bestellingId) {
            try {
                if (!repo.BestaatBestellingBijKlant(bestellingId)) throw new BestellingException("BestellingManager - VerwijderBestelling - Bestelling bestaat niet");
                else
                    repo.VerwijderBestelling(bestellingId);
            }
            catch (Exception ex) {
                throw new BestellingException("BestellingManager: VerwijderBestelling - gefaald", ex);
            }
        }

        public Bestelling VoegBestellingToe(Bestelling bestelling) {
            try {
                if (bestelling == null) throw new BestellingException("BestellingManager - VoegBestellingToe - Bestelling is null!");
                else
                    return repo.VoegBestellingToe(bestelling);
            }
            catch (Exception ex) {
                throw new BestellingException("BestellingManager: VoegBestellingToe - gefaald", ex);
            }
        }

        public bool HeeftBestelling(int id) {
            try {
                return repo.HeeftBestelling(id);
            }catch(Exception ex) {
                throw new BestellingException("BestellingManager: HeeftBestelling - gefaald", ex);
            }
        }
    }
}
