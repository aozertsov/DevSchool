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
    public class UsersController : ApiController
    {
        private UserRepository ur = new UserRepository();

        [HttpPost]
        public void Create(Users user) {
            user.idUser = Guid.NewGuid();
            ur.Create(user);
        }

        [HttpPost]
        public void ChangeNumber(Users user, string number) {
            ur.ChangeNumber(user, number);
        }

        [HttpPut]
        public void Delete(Users user) {
            ur.Delete(user);
        }
    }
}
