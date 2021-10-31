using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Testing {
    public class Test_Klant {
        [Fact]
        public void Test_Naam_Valid() {
            Klant kl = new Klant("Louis", "Brouwershoek-Groot-Bijgaarden");
            kl.ZetNaam("Louis");
            Assert.Equal("Louis", kl.Naam);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Test_Naam_Invalid(string naam) {
            Assert.Throws<KlantException>(() => new Klant(naam, "Brouwershoek-Groot-Bijgaarden"));
        }

        [Fact]
        public void Test_Adres_Valid() {
            Klant kl = new Klant("Louis", "Brouwershoek-Groot-Bijgaarden");
            kl.ZetNaam("Brouwershoek-Groot-Bijgaarden");
            Assert.Equal("Brouwershoek-Groot-Bijgaarden", kl.Adres);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Gekstraat")]
        public void Test_Adres_Invalid(string adres) {
            Assert.Throws<KlantException>(() => new Klant("Louis", adres));
        }
    }
}
