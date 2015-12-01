using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.Map;

namespace ServerLogic.Repositories {
    public interface IUsersRepository : IRepository {
        bool Exist(Guid idUser);
        void Create(Users user);
        Users GetUser(Users user);
        void Delete(Users user);
        void ChangeNumber(Users user, string number);
    }
}
