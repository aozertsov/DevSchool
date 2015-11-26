using ServerLogic.Map;
using ServerLogic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerLogic.API.Controllers
{
    public class MeetingController : ApiController
    {
       // private MeetingRepository mr = new MeetingRepository();

        [HttpPost]
        public void Create(Meeting meet) {
           // mr.Create(meet);
        }
    }
}
