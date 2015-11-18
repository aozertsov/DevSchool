using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Sql;
using ServerLogic.Map;

namespace ServerLogic.Tests {
    [TestClass]
    public class UserTest {
        [TestMethod]
        public void AddUser() {
            var userRepository = new UserRepository();
            var user = new Users() {
                idUser = System.Guid.NewGuid(),
                email = "asdgmail",
                firstName = "we",
                lastName = "sdf",
                contactNumber = "sdf"
            };
            userRepository.Greate(user);
        }
    }
}
