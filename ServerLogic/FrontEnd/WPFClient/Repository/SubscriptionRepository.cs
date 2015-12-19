using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace WPFClient.Repository {

    class SubscriptionRepository {
        public async Task<bool> Subscribe(Guid idUser, int idMeet) {
            using(var client = new HttpClient {
                BaseAddress = new Uri(@"http://localhost:57239/")
            }) {
                var response = client.PostAsJsonAsync($"api/subscription/subscribe/{idMeet}", idUser).Result;
                return await response.Content.ReadAsAsync<bool>();
            }
        }
    }
}
