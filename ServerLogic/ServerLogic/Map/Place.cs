using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Map {
    [Serializable]
    public class Place {
        public int idPlace                  { get; set; }
        public string country               { get; set; }
        public string city                  { get; set; }
        public string street                { get; set; }
        public int house                    { get; set; }
        public IEnumerable<Meeting> meeting { get; set; }
    }
}
