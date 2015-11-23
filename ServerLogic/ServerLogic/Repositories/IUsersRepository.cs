using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.Map;

namespace ServerLogic.Repositories {
    public interface IUsersRepository {
        void Create(Users user);
        void GetUser(Users user);
        void Delete(Users user);
        void ChangeNumber(Users user, string number);
    }
}
