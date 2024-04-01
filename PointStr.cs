using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainBridges
{
    internal class PointStr
    {
        public string index {  get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<string> connections { get; set; }
    }
}
