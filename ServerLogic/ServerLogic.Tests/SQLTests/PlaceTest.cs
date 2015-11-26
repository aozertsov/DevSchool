using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Map;
using ServerLogic.Sql;

namespace ServerLogic.Tests {
    [TestClass]
    public class PlaceTest {

        [TestMethod]
        public void AddPlace() {
            PlaceRepository rep = new PlaceRepository();
            Place place = DataGenerator.GeneratePlace();
            rep.Add(place);
        }

        [TestMethod]
        public void ExistPlace() {
            PlaceRepository rep = new PlaceRepository();
            Place place = DataGenerator.GeneratePlace();
            rep.Exist(place.idPlace);
        }
    }
}
