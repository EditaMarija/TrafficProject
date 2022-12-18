using TrafficProject;

namespace TrafficProject
{
    public class TrafficManagement
    {

        List<Map> maps = new List<Map>();
        List<string> mapData = File.ReadAllLines("map.txt").ToList();

        public TrafficManagement()
        {
            FillMap();
            AddPlanes();
            ManageTraffic();
        }

        //fill maps with data:
        private void FillMap()
        {

            foreach (var line in mapData)
            {
                string[] entries = line.Split(' ');
                Map newMap = new Map();

                if (entries[1].Contains("rectangle"))
                {
                    newMap.Type = entries[0];
                    newMap.Shape = entries[1];
                    newMap.TopLeft = entries[3];
                    newMap.BottomRight = entries[4];
                }

                if (entries[1].Contains("circle"))
                {
                    newMap.Type = entries[0];
                    newMap.Shape = entries[1];
                    newMap.CenterPoint = entries[3];
                    newMap.Radius = Convert.ToInt32(entries[4]);
                }
                maps.Add(newMap);
            }

        }

        public static List<Plane> planes = new List<Plane>();
        private static void AddPlanes()
        {

            //get plane name and coordinates:

            planes.Add(new Plane("FR664", 10, 2));
            planes.Add(new Plane("GB3265", 4, 9));
            planes.Add(new Plane("NO5521", 3, 3));
            planes.Add(new Plane("LT3266", 2, 7));
            planes.Add(new Plane("FL5522", 9, 1));
            planes.Add(new Plane("US5523", 1, 8));        
            planes.Add(new Plane("FL7522", 1, 1));
            planes.Add(new Plane("LT3115", 7, 7));
            planes.Add(new Plane("PL777", 1, 2));
            planes.Add(new Plane("IR5555", 8, 7));
            planes.Add(new Plane("US125", 4, 3));

        }

        public void ManageTraffic()
        {

            //divide maps into zones:
            var fireZone = maps.FirstOrDefault(map => map.Type == "fire");
            var safeZone = maps.FirstOrDefault(map => map.Type == "safe");
            var warnZone = maps.FirstOrDefault(map => map.Type == "warn");

            foreach (var plane in planes)
            {
                //check if plane inside fire zone and not in safe zone:
                if ((((fireZone.Center - plane.PlaneX) * (fireZone.Center - plane.PlaneX))
                        + ((fireZone.Point - plane.PlaneY) * (fireZone.Point - plane.PlaneY)))
                        < (fireZone.Radius * fireZone.Radius))
                {
                    if (!(plane.PlaneX >= safeZone.TopX && plane.PlaneX <= safeZone.BottomX
                        && plane.PlaneY >= safeZone.LeftY && plane.PlaneY <= safeZone.RightY))
                    {
                        Console.WriteLine("\nShooting " + plane.PlaneId + " at (" + plane.PlaneX + "," + plane.PlaneY + ")");
                    }

                }
                // check if plane is in warning zone and not in safe zone
                else if ((plane.PlaneX >= warnZone.TopX && plane.PlaneX <= warnZone.BottomX)
                    && (plane.PlaneY >= warnZone.LeftY && plane.PlaneY <= warnZone.RightY))
                {
                    if (!(plane.PlaneX >= safeZone.TopX && plane.PlaneX <= safeZone.BottomX
                        && plane.PlaneY >= safeZone.LeftY && plane.PlaneY <= safeZone.RightY))
                    {
                        Console.WriteLine("\nWarning " + plane.PlaneId);
                    }
                    //safe zone:
                    else Console.WriteLine("\n ");
                    Thread.Sleep(1000);
                }

            }

        }

    }
}

