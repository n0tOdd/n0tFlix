using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Plugin.NRK.Models
{
    internal class HeadlinersInfo
    {
        public class Self
        {
            
            public string Href { get; set; }
        }

        public class Links
        {
            
            public Self Self { get; set; }
        }

        public class image
        {
            
            public string Uri { get; set; }

            
            public int Width { get; set; }
        }

        public class Self2
        {
            
            public string Href { get; set; }
        }

        public class Series
        {
            
            public string Href { get; set; }
        }

        public class Seriespage
        {
            
            public string Href { get; set; }
        }

        public class Trailer
        {
            
            public string Name { get; set; }

            
            public string Href { get; set; }
        }

        public class Links2
        {
            
            public Self2 Self { get; set; }

            
            public Series Series { get; set; }

            
            public Seriespage Seriespage { get; set; }

            
            public Trailer Trailer { get; set; }
        }

        public class Headliner
        {
            
            public string Title { get; set; }

            
            public string Type { get; set; }

            
            public string SubTitle { get; set; }

            
            public IList<image> images { get; set; }

            
            public Links2 Links { get; set; }
        }

        public class root
        {
            
            public Links Links { get; set; }

            
            public IList<Headliner> Headliners { get; set; }
        }
    }
}