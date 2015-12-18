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
    }
}
