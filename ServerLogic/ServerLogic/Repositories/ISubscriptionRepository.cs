using System;

namespace ServerLogic.Repositories {
    public interface ISubscriptionRepository {
        void Subscribe(Guid idUser, int idMeet);
        void UnSubscribe(Guid idUser, int idMeet);
    }
}
