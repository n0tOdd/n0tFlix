using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Manifest.Creator
{
    public class Manifestdata
    {
        public string guid { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string overview { get; set; }
        
        public string owner { get; set; }

        public string category { get; set; }

        public string imageUrl { get; set; }

        public List<Version> versions { get; set; }

        public class Version
        {
            public string version { get; set; }
            public string changelog { get; set; }
            public string targetAbi { get; set; }

            public string sourceUrl { get; set; }

            public string checksum { get; set; }

            public string timestamp { get; set; }
        }
    }
}
