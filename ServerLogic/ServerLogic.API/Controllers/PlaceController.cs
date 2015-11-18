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
    public class PlaceController : ApiController
    {
        private PlaceRepository pr = new PlaceRepository();

        [HttpPost]
        public void Add(Place place) {
            pr.Add(place);
        }
    }
}
