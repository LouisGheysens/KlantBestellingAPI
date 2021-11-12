using BusinessLayer;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using System;
using Xunit;

namespace Testing {
    public class Test_Bestelling {

        [Fact]
        public void Test_Id_Valid() {
            Bestelling bestelling = new Bestelling(1, (int)BusinessLayer.Enums.Bier.Orval, 2, new BusinessLayer.Models.Klant(1, "Louis", "Brouwershoek"));
            bestelling.ZetId(1);
            Assert.Equal(1, bestelling.BestellingID);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Test_Id_Invalid(int id) {
            Assert.Throws<BestellingException>(() => new Bestelling(id, (int)BusinessLayer.Enums.Bier.Orval, 2, new BusinessLayer.Models.Klant(1, "Louis", "Brouwershoek")));
        }

        [Fact]
        public void Test_Aantal_Valid() {
            Bestelling bestelling = new Bestelling(1, (int)BusinessLayer.Enums.Bier.Orval, 1, new BusinessLayer.Models.Klant(1, "Louis", "Brouwershoek"));
            bestelling.ZetAantal(1);
            Assert.Equal(1, bestelling.Aantal);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Test_Aantal_Invalid(int aantal) {
            Assert.Throws<BestellingException>(() => new Bestelling(1, (int)BusinessLayer.Enums.Bier.Orval, aantal, new BusinessLayer.Models.Klant(1, "Louis", "Brouwershoek")));
        }






    }
}
