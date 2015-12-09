using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Map {
    public class Place {
        public int idPlace                  { get; set; }
        public string country               { get; set; }
        public string city                  { get; set; }
        public string street                { get; set; }
        public int house                    { get; set; }
        public IEnumerable<Meeting> meeting { get; set; }
    }
}
