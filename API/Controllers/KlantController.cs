using API.Mappers;
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
        private readonly BestellingManager tManager;
        private readonly Klant klant;
        private readonly KlantManager klmgr;

        public KlantController(IKlantRepository klantservice, IBestellingRepository brepo) {
            this.kRepo = klantservice;
            this.bRepo = brepo;
        }

        //Get aan de hand van ID
        [HttpGet("{id}")]
        public ActionResult<KlantRESTOutputTDO> BestaatKlant(int id) {
            var klant = kRepo.GetKlant(id);
            return Ok(MapFromDomain.MapFromKlantDomain(url, klant, tManager));
        }




    }
}
