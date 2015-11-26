using System;

namespace ServerLogic.Repositories {
    public interface ISubscriptionRepository : IRepository {
        void Subscribe(Guid idUser, int idMeet);
        void UnSubscribe(Guid idUser, int idMeet);
    }
}
