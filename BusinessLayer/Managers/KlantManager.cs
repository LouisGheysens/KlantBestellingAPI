using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers {
    public class KlantManager {

        private IKlantRepository repo;

        public KlantManager(IKlantRepository repo) { this.repo = repo; }

        public bool BestaatKlant(Klant klant) {
            switch (klant.KlantID) {
                case <= 0:
                    return false;
                default:
                    return true;
            }
        }

        public bool BestaatKlant(int id) {
            switch (id) {
                case <= 0:
                    return false;
                default:
                    return true;
            }
        }

        public Klant GetKlant(Klant klant) {
            try {
                if (klant.KlantID <= 0) throw new KlantException("KlantManager: GetKlant(id) - Klant is null");
                return repo.GetKlant(klant.KlantID);
            }catch(Exception ex) {
                throw new KlantException("KlantManager: GetKlant - gefaald", ex);
            }
        }

        public List<Klant> SelecteerKlanten() {
            try {
                return repo.SelecteerKlanten();
            }
            catch(Exception ex) {
                throw new KlantException("KlantManager: SelecteerKlanten - gefaald", ex);
            }
        }

        public void UpdateKlant(Klant klant) {
            try {
                if (klant == null) throw new KlantException("KlantManager: UpdateKlant - Klant is null");
                if (!repo.BestaatKlant(klant)) throw new KlantException("KlantManager: UpdateKlant - Klant bestaat niet");
                repo.UpdateKlant(klant);
            }
            catch(Exception ex) {
                throw new KlantException("KlantManager: UpdateKlant - gefaald", ex);
            }
        }

        public void VerwijderKlant(Klant klant) {
            try {
                if (klant == null) throw new KlantException("KlantManager: VerwijderenKlant - Klant is null");
                if (!repo.BestaatKlant(klant)) throw new KlantException("KlantManager: VerwijderenKlant - Klant bestaat niet");
                repo.VerwijderKlant(klant);
            }
            catch(Exception ex) {
                throw new KlantException("KlantManager: VerwijderKlant - gefaald", ex);
            }
        }

        public Klant VoegKlantToe(Klant klant) {
            try {
                if (klant == null) throw new KlantException("KlantManager: VoegKlantToe - Klant is null");
                if (repo.BestaatKlant(klant)) throw new KlantException("KlantManager: VoegKlantToe - Klant bestaat al");
                repo.VoegKlantToe(klant);
                return klant;
            }
            catch(Exception ex) {
                throw new KlantException("KlantManager: VoegKlantToe - gefaald", ex);
            }
        }

    }
}
