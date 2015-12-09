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
            var user = DataGenerator.GenerateUser();
            userRepository.Create(user);
        }

        [TestMethod]
        public void Exist() {
            var userRepository = new UserRepository();
            var user = DataGenerator.GenerateUser();
            Assert.AreEqual(true, userRepository.Exist(user.idUser));
        }

        [TestMethod]
        public void GetUser() {
            var userRepository = new UserRepository();
            var user = DataGenerator.GenerateUser();
            Assert.AreEqual(user.idUser, userRepository.GetUser(user).idUser);
        }

        [TestMethod]
        public void ChangeNumber() {
            var userRepository = new UserRepository();
            var user = DataGenerator.GenerateUser();
            userRepository.ChangeNumber(user, "987");
        }
        
        [TestMethod]
        [ClassCleanup]
        public void DeleteUser() {
            var userRepository = new UserRepository();
            var user = DataGenerator.GenerateUser();
            userRepository.Delete(user);
        }
    }
}
