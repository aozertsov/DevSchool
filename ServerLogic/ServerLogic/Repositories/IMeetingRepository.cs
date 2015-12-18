using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.Map;

namespace ServerLogic.Repositories {
    public interface IMeetingRepository : IRepository {

        //bool Exist(int place, DateTime dateMeet);
        List<Place> Get(Guid idUser);
        void Create(Meeting meeting);
        void ChangeDateTime(Meeting meeting, DateTime date);
        bool Exist(int idMeet);
    }
}
