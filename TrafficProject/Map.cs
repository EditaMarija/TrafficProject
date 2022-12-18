using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficProject
{
    internal class Map
    {
        public string Shape { get; set; }
        public string Type { get; set; }

        //Circle:
        public int Radius { get; set; }
        public int Center { get; private set; }
        public int Point { get; private set; }
        public string CenterPoint
        {

            set
            {
                var parts = value.Split(',', '(', ')');
                //var parts = value.Substring(0, 1);
                Center = Convert.ToInt32(parts[1]);
                Point = Convert.ToInt32(parts[2]);
            }
        }

        //Rectangle:
        public int TopX { get; private set; }
        public int LeftY { get; private set; }
        public string TopLeft
        {
            set
            {
                var parts = value.Split(',', '(', ')');
                TopX = Convert.ToInt32(parts[1]);
                LeftY = Convert.ToInt32(parts[2]);
            }
        }

        public int BottomX { get; private set; }
        public int RightY { get; private set; }
        public string BottomRight
        {
            
            set
            {
                var parts = value.Split(',', '(', ')');
                BottomX = Convert.ToInt32(parts[1]);
                RightY = Convert.ToInt32(parts[2]);
            }
        }

        public Map()
        {

        }
              

    }

}