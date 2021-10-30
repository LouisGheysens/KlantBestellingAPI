using BusinessLayer.Enums;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Input {
    public class BestellingRESTInputTDO {
        public int BestellingID { get; set; }

        public Klant Klant { get; private set; }

        public int Aantal { get; private set; }

        public Bier Product { get; private set; }
    }
}
