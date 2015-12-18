using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerLogic.API.Controllers
{
    using Map;
    using Sql;

    public class MeetingController : ApiController
    {
        MeetingRepository mr = new MeetingRepository(new PlaceRepository());
        UserRepository ur = new UserRepository();

        [HttpPost]
        [Route("api/meeting/mymeets")]
        public List<Place> MyMeets([FromBody] Guid idUser) {
            var w = mr.Get(idUser);
            return w;
        }
    }
}
