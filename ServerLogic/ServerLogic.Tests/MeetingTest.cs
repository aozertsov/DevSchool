using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Map;
using ServerLogic.Sql;
using System;

namespace ServerLogic.Tests {
    [TestClass]
    public class MeetingTest {
        [TestMethod]
        public void CreateMeeting() {
            MeetingRepository mr = new MeetingRepository();
            Meeting meet = new Meeting() {
                idMeet = 1,
                place = 1,
                dateTime = new DateTime(),
            };
            mr.Create(meet);
        }
        [TestMethod]
        public void ChangeDataTimeTest() {
            MeetingRepository mr = new MeetingRepository();
            Meeting meet = new Meeting() {
                idMeet = 1,
                place = 1,
                dateTime = new DateTime(),
            };
            mr.ChangeDateTime(meet, new DateTime());
        }
    }
}
