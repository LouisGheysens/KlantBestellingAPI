using BusinessLayer.Enums;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Input {
    public class BestellingRESTInputTDO {

        public int Aantal { get; set; }

        public int KlantId { get; set; }

        public int Product { get; set; }
    }
}
