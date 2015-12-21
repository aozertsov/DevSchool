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

        [HttpPost]
        [Route("api/meeting/create/{idPlace}")]
        public int CreateMeet(int idPlace, [FromBody] DateTime dateMeet) {
            if (!mr.Exist(idPlace, dateMeet))
                mr.Create(new Meeting {dateMeet =  dateMeet, place = idPlace});
            return mr.GetId(idPlace, dateMeet);
        }

        [HttpPost]
        [Route("api/meeting/invites")]
        public List<Place> GetInvites([FromBody] string email) {
            return mr.Invitations(email);
        }
    }
}
