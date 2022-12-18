using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TrafficProject
{
    public class Plane
    {
        public string PlaneId { get; set; }
        public int PlaneX { get; set; }
        public int PlaneY { get; set; }


        public Plane()
        {

        }
        public Plane(string planeId, int x, int y)
        {
            PlaneId = planeId;
            PlaneX = x;
            PlaneY = y;

        }

        public static void ShowPlanes(List<Plane> planes)
        {
            foreach (var plane in planes)
            {
                Console.WriteLine($"{plane.PlaneId} ({plane.PlaneX},{plane.PlaneY}) ");
            }

        }
    }
}

