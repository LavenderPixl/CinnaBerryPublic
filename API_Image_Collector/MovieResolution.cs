using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Image_Collector
{
    public class MovieResolution
    {
        public int width;
        public int height;
        public string name;

        public MovieResolution(int width, int height, string name)
        {
            this.width = width;
            this.height = height;
            this.name = name;
        }
    }
}
