using System.IO;
using TrafficProject;


List<Map> maps = new List<Map>();
List<string> mapData = File.ReadAllLines("map.txt").ToList();


//fill maps with data:
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

//print map coordinates:
foreach (var map in maps)
{
    if (map.Shape == "rectangle")
    {
        Console.WriteLine($"{map.Type} {map.Shape} ({map.TopX}, {map.LeftY}) ({map.BottomX}, {map.RightY}) ");
    }
    else
    {
        Console.WriteLine($"{map.Type} {map.Shape} ({map.Center},{map.Point}) {map.Radius}");
    }
}


//get plane name and coordinates:
List<Plane> planes = new List<Plane>();
planes.Add(new Plane("FR664", 10, 2)); 
planes.Add(new Plane("GB3265", 4, 9));
planes.Add(new Plane("NO5521", 3, 3)); 

planes.Add(new Plane("NL666", 2, 2)); 
planes.Add(new Plane("NZ5524", 1, 1)); 
planes.Add(new Plane("LT3266", 2, 7));
planes.Add(new Plane("FL5522", 9, 1));
planes.Add(new Plane("US5523", 1, 8));
planes.Add(new Plane("NZ5524", 1, 1));
planes.Add(new Plane("IR5555", 8, 7)); 

//Plane.ShowPlanes(planes);

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
            Console.WriteLine("\nWarning " + plane.PlaneId + " at (" + plane.PlaneX + "," + plane.PlaneY + ")");
        }
        //safe zone:
        else Console.WriteLine("\n ");
    }
   
}


//    planeX > top  &&  planeX < bottom  && planeY > left && planeY <  right
//        x > x1 && x < x2 && y > y1 && y < y2
