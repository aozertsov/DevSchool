using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.Map;

namespace ServerLogic.Repositories {
    public interface IMeetingRepository {
        void Create(Meeting meeting);
        void ChangeDateTime(Meeting meeting, DateTime date);
    }
}
