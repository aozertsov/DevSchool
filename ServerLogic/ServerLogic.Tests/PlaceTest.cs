using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Map;
using ServerLogic.Sql;

namespace ServerLogic.Tests {
    [TestClass]
    public class PlaceTest {

        [TestMethod]
        public void AddPlace() {
            PlaceRepository rep = new PlaceRepository();
            Place place = new Place() {
                idPlace = 1,
                country = "Russia",
                city = "Ulyanovsk",
                house = 23,
                street = "Gagarina"
            };
            rep.Add(place);
        }
    }
}
