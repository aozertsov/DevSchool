using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Map;
using ServerLogic.Repositories;
using ServerLogic.Sql;
using System;

namespace ServerLogic.Tests {
    [TestClass]
    public class MeetingTest {
        [TestMethod]
        public void CreateMeeting() {
            IPlaceRepository plRep = DataGenerator.GetPlaceRepositoryMock();
            MeetingRepository mr = new MeetingRepository(plRep);
            Meeting meet = DataGenerator.GenerateMeet();
            mr.Create(meet);
        }

        [TestMethod]
        public void ExistTest() {
            IPlaceRepository plRep = DataGenerator.GetPlaceRepositoryMock();
            MeetingRepository mr = new MeetingRepository(plRep);
            Meeting meet = DataGenerator.GenerateMeet();
            mr.Exist(meet.idMeet);
        }

        [TestMethod]
        public void ChangeDataTimeTest() {
            IPlaceRepository plRep = DataGenerator.GetPlaceRepositoryMock();
            MeetingRepository mr = new MeetingRepository(plRep);
            Meeting meet = new Meeting() {
                idMeet = 1,
                place = 1,
                dateMeet = new DateTime(),
            };
            mr.ChangeDateTime(meet, new DateTime());
        }
        
    }
}
