using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerLogic.API.Controllers
{
    using Sql;

    public class SubscriptionController : ApiController
    {
        SubscriptionRepository sr = new SubscriptionRepository(new MeetingRepository(new PlaceRepository()), new UserRepository());
        UserRepository ur = new UserRepository();
        MeetingRepository mr = new MeetingRepository(new PlaceRepository());


        [HttpPost]
        [Route("api/subscription/subscribe/{idMeet}")]
        public bool Subscribe([FromBody] Guid idUser, int idMeet) {
            if (ur.Exist(idUser) && mr.Exist(idMeet)) {
                sr.Subscribe(idUser, idMeet);
                return true;
            }
            return false;
        }

        [HttpPost]
        [Route("api/subscription/unsubscribe/{idMeet}")]
        public bool Unsubscribe([FromBody] Guid idUser, int idMeet) {
            if(ur.Exist(idUser) && mr.Exist(idMeet)) {
                sr.UnSubscribe(idUser, idMeet);
                return true;
            }
            return false;
        }
    }
}
