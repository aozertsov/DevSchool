using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.Map;
using System.Net.Http;

namespace WPFClient.Repository {
    using Newtonsoft.Json;

    class MeetRepository {
        public async Task<List<Place>> GetMeets(Guid id) {
            var w = JsonConvert.SerializeObject(id);
            using(var client = new HttpClient {
                BaseAddress = new Uri(@"http://localhost:57239/")
            }) {
                var response = client.PostAsJsonAsync("api/meeting/mymeets", id).Result;
                return await response.Content.ReadAsAsync<List<Place>>();
            }
        }

        public async Task<int> CreateMeet(Place place, DateTime dateMeet, Guid idUser, int idPlace) {
            using(var client = new HttpClient {
                BaseAddress = new Uri(@"http://localhost:57239/")
            }) {
                var response = client.PostAsJsonAsync($"api/meeting/create/{idPlace}", dateMeet).Result;
                return await response.Content.ReadAsAsync<int>();
//                response = client.PostAsJsonAsync($"api/subscription/subscribe/{idMeet}", idUser).Result;
//                return response.Content.ReadAsAsync<bool>().Result;
            }
        }

    }
}
