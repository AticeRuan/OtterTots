using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtterTots
{
    class cityLocations
    {
        public string name { get; set; }
        public double minLatitude { get; set; }
        public double maxLatitude { get; set; }
        public double minLongitude { get; set; }
        public double maxLongitude { get; set; }

        public cityLocations(string cityName,double minLat,double maxLat,double minLong,double maxLong)
        {
            name = cityName;
            minLatitude = minLat;
            maxLatitude = maxLat;
            minLongitude = minLong;
            maxLongitude = maxLong;
        }
    }
}
