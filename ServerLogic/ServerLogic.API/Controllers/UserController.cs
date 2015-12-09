using ServerLogic.Map;
using ServerLogic.Sql;
using System;
using System.Web.Http;
using System.Web.Http.Results;

namespace ServerLogic.API.Controllers {
    public class UsersController : ApiController 
    {
        private UserRepository ur = new UserRepository();

        [HttpPost]
        [Route("api/users/create/")]
        public Guid Create(Users user) {
            if(ur.Exist(user.email))
                return ur.GetUser(user).idUser;
            else {
                user.idUser = Guid.NewGuid();
                ur.Create(user);
                return user.idUser;
            }
        }

        [HttpPost]
        [Route("api/users/chnumber/{number}")]
        public void ChangeNumber(Users user, string number) {
            ur.ChangeNumber(user, number);
        }

        [HttpPut]
        public void DeleteMeet(Meeting m) {
            //ur.Delete(user);
        }
    }
}