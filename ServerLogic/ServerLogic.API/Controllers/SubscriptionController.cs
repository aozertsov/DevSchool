using ServerLogic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerLogic.API.Controllers
{
    public class SubscriptionController : ApiController
    {
        //SubscriptionRepository sr = new SubscriptionRepository();

        [HttpPut]
        public void Subscribe(Guid idUser, int idMeet) {
          //  sr.Subscribe(idUser, idMeet);
        }

        [HttpPut]
        public void UnSubscribe(Guid idUser, int idMeet) {
            //sr.UnSubscribe(idUser, idMeet);
        }
    }
}
