using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtterTots
{
    class Songs
    {
        public string Name { get; set; }
        public string ImageName { get; set; }

        public string Lyric { get; set; }

        public string Path { get; set; }
        public Songs()
        {
            Name = " ";
            ImageName = " ";
            Lyric = " ";
            Path = " ";
        }

        public Songs(string inputName, string inputImageName,string inputLyric,string inputPath)
        {
            Name = inputName;
            ImageName = inputImageName;
            Lyric = inputLyric;
            Path = inputPath;
        }
    }
}
