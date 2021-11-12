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
        private string url = "http://localhost:5000";
        private readonly BestellingManager _bm;
        private readonly KlantManager _km;

        public KlantController(BestellingManager bm, KlantManager km) {
            _bm = bm;
            _km = km;
        }

        #region Klant
        //Get
        [HttpGet("{id}")]
        public ActionResult<KlantRESTOutputTDO> GetKlant(int id) {
            try {
                Klant k = _km.GetKlant(id);
                return Ok(MapFromDomain.MapFromKlantDomain(url, k, _bm));
            }catch(Exception ex) {
                return NotFound(ex);
            }
        }

        //Post
        [HttpPost]
        public ActionResult<KlantRESTOutputTDO> PostKlant([FromBody] KlantRESTInputTDO tdo) {
            try {
                Klant k = _km.VoegKlantToe(MapToDomain.MapToKlantDomain(tdo));
                return CreatedAtAction(nameof(GetKlant), new { id = k.KlantID }, MapFromDomain.MapFromKlantDomain(url, k, _bm));
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        //Put
        [HttpPut("{id}")]
        public ActionResult<KlantRESTOutputTDO> PutKlant(int id, [FromBody] KlantRESTInputTDO tdo) {
            try {
                if(!_km.BestaatKlant(id) || tdo == null || string.IsNullOrWhiteSpace(tdo.Naam) || 
                    string.IsNullOrWhiteSpace(tdo.Adres)) {
                    return BadRequest();
                }
                Klant k = MapToDomain.MapToKlantDomain(tdo);
                k.Zetid(id);
                Klant klantDBObject = _km.UpdateKlant(k);
                return CreatedAtAction(nameof(GetKlant), new { id = klantDBObject.KlantID }, MapFromDomain.MapFromKlantDomain(url, klantDBObject, _bm));
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult<KlantRESTOutputTDO> DeleteKlant(int id) {
            try {
                if (_bm.HeeftBestelling(id)) {
                    return BadRequest("Klant heeft nog een achterliggende bestelling");
                }
                _km.VerwijderKlant(id);
                return NoContent();
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Bestelling
        //Get
        [HttpGet]
        [Route("{id/Bestelling/{bestellingId")]
        public ActionResult<BestellingRESTOutputTDO> GetBestelling(int id, int bestellingId) {
            try {
                Bestelling b = _bm.GeefBestellingWeer(bestellingId);
                if(b.Klant.KlantID != id) {
                    return BadRequest("KlantId komt niet overeen!");
                }
                return Ok(MapFromDomain.MapFromBestellingDomain(url, b));
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }

        //Post
        [HttpPost]
        [Route("{id}/Bestelling/")]
        public ActionResult<BestellingRESTOutputTDO> PostBestelling(int id, [FromBody] BestellingRESTInputTDO besteltdo) {
            try {
                Klant k = _km.GetKlant(besteltdo.KlantId);
                if(k.KlantID != id) {
                    return BadRequest("KlantId komt niet overeen");
                }
                Bestelling b = _bm.VoegBestellingToe(MapToDomain.MapToBestellingDomain(besteltdo, k));
                return CreatedAtAction(nameof(GetBestelling), new { id = b.Klant.KlantID, bestellingId = b.BestellingID }, MapFromDomain.MapFromBestellingDomain(url, b));
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        //Put
        [HttpPut]
        [Route("{id}/Bestelling/{bestellingId")]
        public ActionResult<BestellingRESTOutputTDO> PutBestelling(int id, int bestellingId, [FromBody] BestellingRESTInputTDO tdo) {
            try {
                if(!_bm.BestaatBestelling(id) || tdo == null) {
                    return BadRequest();
                }
                Klant k = _km.GetKlant(id);
                if(k.KlantID != tdo.KlantId) {
                    return BadRequest("Id komt niet overeen!");
                }
                Bestelling b = MapToDomain.MapToBestellingDomain(tdo, k);
                b.ZetId(id);
                Bestelling bestellingDB = _bm.UpdateBestelling(b);
                return CreatedAtAction(nameof(GetBestelling), new { id = bestellingDB.Klant.KlantID, bestellingId = bestellingDB.BestellingID }, MapFromDomain.MapFromBestellingDomain(url, b));
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        //Delete
        [HttpDelete]
        [Route("{id}/Bestelling/{bestellingId")]
        public ActionResult<BestellingRESTOutputTDO> DeleteBestelling(int id, int bestellingId) {
            try {
                if (!_km.BestaatKlant(id)) {
                    return BadRequest("Klant bestaat niet!");
                }
                _bm.VerwijderBestelling(bestellingId);
                return NoContent();
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
