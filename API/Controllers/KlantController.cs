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

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class KlantController : ControllerBase {
        private string url = "api/klant/";
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
                throw new ControllerException("KlantController: BestaatKlant(id) - gefaald", ex);
            }
        }

        //Post
        [HttpPost]
        public ActionResult<KlantRESTOutputTDO> PostKlant([FromBody] KlantRESTInputTDO tdo) {
            try {
                Klant klant = kManager.VoegKlantToe(MapToDomain.MapToKlantDomain(tdo));
                return CreatedAtAction(nameof(GetKlant), new { id = klant.KlantID },
                    MapFromDomain.MapFromKlantDomain(url, klant, bManager));

            }catch(Exception ex) {
                throw new ControllerException("KlantController: PostKlant - gefaald", ex);
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

        #endregion


    }
}
