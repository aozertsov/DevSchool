using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Map;
using ServerLogic.Sql;
using System;

namespace ServerLogic.Tests {
    [TestClass]
    public class SubscriptionTest {

        [TestMethod]
        public void SubscribeTest() {
            SubscriptionRepository sr = new SubscriptionRepository(DataGenerator.GetMeetingRepositoryMock(), DataGenerator.GetUsersRepositoryMock() );
            Subscription subscription = DataGenerator.GenerateSubscription();
            Users user = DataGenerator.GenerateUser();
            sr.Subscribe(user.idUser, 1);
        }

        [TestMethod]
        public void UnsubscribeTest() {
            SubscriptionRepository sr = new SubscriptionRepository(DataGenerator.GetMeetingRepositoryMock(), DataGenerator.GetUsersRepositoryMock());
            Subscription subscription = DataGenerator.GenerateSubscription();
            Users user = DataGenerator.GenerateUser();
            sr.UnSubscribe(user.idUser, 1);
        }
    }
}
