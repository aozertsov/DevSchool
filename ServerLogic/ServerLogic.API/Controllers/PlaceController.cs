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

    public class PlaceController : ApiController
    {
        PlaceRepository pr = new PlaceRepository();

        [HttpPost]
        [Route("api/place/add/country/{place.country}/city/{place.city}/street/{place.street}/house/{place.house}")]
        public bool AddPlace(Place place) {
            //TODO generate idPlace
            if (!pr.Exist(place.idPlace)) {
                pr.Add(new Place());
                return true;
            }
            return false;
        }
    }
}
