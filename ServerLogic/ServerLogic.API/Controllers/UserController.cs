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
            if (!ur.Exist(user.email)) {
                user.idUser = Guid.NewGuid();
                ur.Create(user);
                return user.idUser;
            }
            throw new ArgumentException("данные не уникальны");
        }

        [HttpPost]
        [Route("api/users/login/")]
        public Users Login(Users user) {
            if (ur.Exist(user.email) && ur.Phone(user.email).Replace(" ", String.Empty) == user.contactNumber)
                return ur.GetUser(user.email);
            throw new ArgumentException("данные не верно введены");
        }

        [HttpPost]
        [Route("api/users/chnumber/{number}")]
        public void ChangeNumber(Users user, string number) {
            ur.ChangeNumber(user, number);
        }

        [HttpPut]
        [Route("api/users/delete")]
        public void Delete(Users user) {
            if(ur.Exist(user.idUser))
                ur.Delete(user);
        }
    }
}