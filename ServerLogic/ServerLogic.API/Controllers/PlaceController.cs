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

        [HttpGet]
        [Route("api/place/id/country/{country}/city/{city}/street/{street}/house/{house}/flat/{flat}")]
        public int GetID(string country, string city, string street, int house, int flat) {
            Place place = new Place {city = city, country = country, house = house, street = street, flat = flat};
            if (!pr.Exist(place.idPlace)) {
                pr.Add(place);
            }
            return pr.GetId(place);
        }
    }
}
