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

        public bool BestaatKlant(int id) {
            try 
            {
                if (id <= 0) throw new KlantException("KlantManager: id is 0 of onbestaand");
                return repo.BestaatKlantId(id);
            }
            catch(Exception ex) 
            {
                throw new KlantException("KlantManager: BestaatKlant(id) - gefaald", ex);
            }
        }

        public Klant GetKlant(int id) {
            try {
                if (!repo.BestaatKlantId(id)) {
                    throw new KlantException("KlantManager: GetKlant(id) - Klant bestaat niet!");
                }
                return repo.GetKlant(id);
            }catch(Exception ex) {
                throw new KlantException("KlantManager: GetKlant(id) - gefaald", ex);
            }
        }

        public Klant UpdateKlant(Klant klant) {
            if (klant == null) throw new KlantException("KlantManager: UpdateKlant - Klant is null");
            if (!repo.BestaatKlantId(klant.KlantID)) throw new KlantException("KlantManager: UpdateKlant - Klant bestaat niet!");
            Klant klantDbObject = GetKlant(klant.KlantID);
            if (klantDbObject == klant) throw new KlantException("KlantManager: UpdateKlant - Er werden geen verschillen gevonden");
            repo.UpdateKlant(klant);
            return klant;
        }

        public void VerwijderKlant(int id) {
            if (!repo.BestaatKlantId(id)) throw new KlantException("KlantManager: VerwijderKlant(id) - Klant bestaat niet");
            repo.VerwijderKlant(id);
        }

        public Klant VoegKlantToe(Klant klant) {
            try {
                if (klant == null) throw new KlantException("KlantManager: VoegKlantToe - Klant is null!");
                if (repo.BestaatKlantId(klant.KlantID)) throw new KlantException("KlantManager: VoegKlantToe - Klant bestaat reeds!");
                return repo.VoegKlantToe(klant);
            }catch(Exception ex) {
                throw new KlantException("KlantManager: VoegKlantToe - gefaald", ex);
            }
        }

    }
}
