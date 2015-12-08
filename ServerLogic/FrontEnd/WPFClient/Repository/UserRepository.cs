using System;
using System.Net.Http;
using System.Threading.Tasks;
using ServerLogic.Map;

namespace WPFClient.Repository {
    using Newtonsoft.Json;

    class UserRepository {

        public async Task<Users> GetUser(Guid id) {
            using(var client = new HttpClient {
                BaseAddress = new Uri(@"http://localhost:57239/")
            }) {
                var response = client.GetAsync("api/users/" + id).Result;
                return await response.Content.ReadAsAsync<Users>();
            }
        }

        public async Task<Users> CreateUser(Users user) {
            string usr = JsonConvert.SerializeObject(user);
            using(var client = new HttpClient {
                BaseAddress = new Uri(@"http://localhost:57239/")
            }) {
                var response = client.PostAsJsonAsync($"api/users/create/", user).Result;
                string a = JsonConvert.SerializeObject(user);
                return await response.Content.ReadAsAsync<Users>();
            }
        }
    }
}
