using API.Exceptions;
using API.Mappers;
using API.Models.Input;
using API.Models.Output;
using BusinessLayer.Interfaces;
using BusinessLayer.Managers;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Repos;
using BusinessLayer;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class KlantController : ControllerBase {
        private string url = "api/klant/";
        KlantRepository repo = new KlantRepository(@"Data Source=DESKTOP-3CJB43N\SQLEXPRESS;Initial Catalog=WebAPI;Integrated Security=True");
        BestellingRepository repob = new BestellingRepository(@"Data Source=DESKTOP-3CJB43N\SQLEXPRESS;Initial Catalog=WebAPI;Integrated Security=True");
        private readonly IKlantRepository kRepo;
        private readonly IBestellingRepository bRepo;
        private readonly BestellingManager bManager;
        private readonly Klant klant;
        private readonly KlantManager kManager;

        public KlantController(IKlantRepository klantservice, IBestellingRepository brepo) {
            this.kRepo = klantservice;
            this.bRepo = brepo;
        }

        #region Klant

        //Get
        [HttpGet("{id}")]
        public ActionResult<KlantRESTOutputTDO> GetKlant(int id) {
            try {
                var klant = kRepo.GetKlant(id);
                return Ok(MapFromDomain.MapFromKlantDomain(url, klant, bManager));
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        //Post
        [HttpPost]
        public ActionResult<KlantRESTOutputTDO> PostKlant([FromBody] KlantRESTInputTDO tdo) {
            try {
                if(tdo == null) {
                    throw new ControllerException("KlantController: PostKlant - geen klant ter beschikking");
                }

                Klant k = new Klant(tdo.KlantID, tdo.Naam, tdo.Adres); ;
                repo.VoegKlantToe(k);
                return CreatedAtAction(nameof(GetKlant), new { KlantId = k.KlantID }, k);

            }catch(Exception ex) {

                return BadRequest(ex.Message);
            }
        }


        //Delete
        [HttpDelete("{id}")]
        public ActionResult DeleteKlant(int id) {
            try {
                if (!kRepo.BestaatKlant(klant)){
                    return NotFound();
                }

                kRepo.VerwijderKlant(kRepo.GetKlant(id));

                return NoContent();
            }catch(Exception ex) {
                throw new ControllerException("KlantController: DeleteKlant - gefaald", ex);
            }
        }

        //Put
        [HttpPut("{id}")]
        public ActionResult PutKlant(int id, [FromBody] Klant klant) {
            try {
                if(klant == null || klant.KlantID != id) {
                    return BadRequest();
                }

                if (!kRepo.BestaatKlantId(id)){
                    kRepo.VoegKlantToe(klant);
                    return CreatedAtAction(nameof(GetKlant), new { id = klant.KlantID }, klant);
                }

                var klantDB = kRepo.GetKlant(id);
                kRepo.UpdateKlant(klant);
                return new NoContentResult();
            }catch(Exception ex) {
                throw new ControllerException("KlantController: PutKlant - gefaald", ex);
            }
        }
        #endregion

        #region Bestelling
        //Get
        [HttpGet("{id}/Bestelling/{BestellingID}")]
        public ActionResult<BestellingRESTOutputTDO> GetBestelling(int id, int BestellingID) {
            try {
                var bestelling = bRepo.GetBestellingKlant(id);
                return Ok(MapFromDomain.MapFromBestellingDomain(url, klant, bManager));
            }
            catch (Exception ex) {
                throw new ControllerException("KlantController: GetBestelling(id) - gefaald", ex);
            }
        }

        //Post
        [HttpPost]
        public ActionResult<BestellingRESTOutputTDO> PostBestelling([FromBody] BestellingRESTInputTDO tdo) {
            try {
                Klant k = new Klant(tdo.KlantID, tdo.Naam, tdo.Adres);
                Bestelling b = new Bestelling(tdo.Product, tdo.Aantal, k);
                bRepo.VoegBestellingToe(b);
                return CreatedAtAction(nameof(PostBestelling), new { KlantId = k.KlantID }, k);

            }
            catch (Exception ex) {

                throw new ControllerException("KlantController: PostBestelling - gefaald", ex);
            }
        }


        //Delete
        [HttpDelete("{id}")]
        public ActionResult DeleteBestelling(int id) {
            try {
                if (!bRepo.BestaatBestellingBijKlant(id)) {
                    return NotFound();
                }

                bRepo.VerwijderBestelling(bRepo.GetBestellingKlant(id));

                return NoContent();
            }
            catch (Exception ex) {
                throw new ControllerException("KlantController: DeleteBestelling - gefaald", ex);
            }
        }


        //Put
        [HttpPut("{id}")]
        public ActionResult PutBestelling(int id, [FromBody] Bestelling  bestelling) {
            try {
                if (bestelling == null || bestelling.BestellingID != id) {
                    return BadRequest();
                }

                if (!bRepo.BestaatBestellingBijKlant(id)) {
                    bRepo.VoegBestellingToe(bestelling);
                    return CreatedAtAction(nameof(GetBestelling), new { id = bestelling.BestellingID }, bestelling);
                }

                var besteldb = bRepo.GetBestellingKlant(id);
                bRepo.UpdateBestelling(bestelling);
                return new NoContentResult();
            }
            catch (Exception ex) {
                throw new ControllerException("KlantController: PutBestelling - gefaald", ex);
            }
            #endregion


        }
}
