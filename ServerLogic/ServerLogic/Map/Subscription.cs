using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Map {
    [Serializable]
    public class Subscription {
        public Guid idUser { get; set; }
        public int idPlace { get; set; }
    }
}
