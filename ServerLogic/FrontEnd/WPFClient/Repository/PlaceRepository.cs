using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.Map;
using System.Net.Http;

namespace WPFClient.Repository {
    

    public class PlaceRepository {

        public async Task<int> GetId(Place place) {
            using(var client = new HttpClient {
                BaseAddress = new Uri(@"http://localhost:57239/")
            }) {
                var response = client.GetAsync($"api/place/id/country/{place.country}/city/{place.city}/street/{place.street}/house/{place.house}/flat/{place.flat}").Result;
                return await response.Content.ReadAsAsync<int>();
            }
        }
    }
}
