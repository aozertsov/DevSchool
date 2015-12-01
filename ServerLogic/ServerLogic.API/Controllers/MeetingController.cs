using ServerLogic.Map;
using ServerLogic.Sql;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerLogic.API.Controllers
{
    public class MeetingController : ApiController
    {
        //private PlaceRepository placeRep = new PlaceRepository();
        private MeetingRepository mr = new MeetingRepository(new PlaceRepository());

        [HttpPost]
        public void Create(Meeting meet) {
            mr.Create(meet);
        }

        [HttpGet]
        //[Route("api/Meeting/Create")]
        
        public int Get(int i) {
            return i;
        }

        [HttpPost]
        public void ChangeDate(Meeting meet, DateTime date) {
            mr.ChangeDateTime(meet, date);
        }
    }
}
