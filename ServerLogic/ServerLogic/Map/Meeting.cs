using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Map {
    [Serializable]
    public class Meeting {
        public int idMeet                              { get; set; }
        public int place                               { get; set; }
        public DateTime dateMeet                       { get; set; }
        public IEnumerable<Subscription> subscriptions { get; set; }
    }
}
