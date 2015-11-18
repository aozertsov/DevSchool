using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Map;
using ServerLogic.Sql;
using System;

namespace ServerLogic.Tests {
    [TestClass]
    public class SubscriptionTest {

        [TestMethod]
        public void SubscribeTest() {
            SubscriptionRepository sr = new SubscriptionRepository();
            Subscription subscription = new Subscription() {
                idUser = Guid.NewGuid(),
                idPlace = 1,
            };
            Users user = new Users() {
                idUser = Guid.NewGuid(),
                email = "string",
                firstName = "string",
                lastName = "string",
                contactNumber = "se",
            };
            sr.Subscribe(user.idUser, 1);
        }

        [TestMethod]
        public void UnsubscribeTest() {
            SubscriptionRepository sr = new SubscriptionRepository();
            Subscription subscription = new Subscription() {
                idUser = Guid.NewGuid(),
                idPlace = 1,
            };
            Users user = new Users() {
                idUser = Guid.NewGuid(),
                email = "string",
                firstName = "string",
                lastName = "string",
                contactNumber = "se",
            };
            sr.UnSubscribe(user.idUser, 1);
        }
    }
}
