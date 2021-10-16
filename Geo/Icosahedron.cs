using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo
{
  public class Icosahedron
  {
    private static readonly double a = Math.Sqrt(2 / (5 + Math.Sqrt(5)));
    private static readonly double b = Math.Sqrt(1 - 2 / (5 + Math.Sqrt(5)));

    public static Vector3D[] Points { get; } = new Vector3D[]
    {
      new Vector3D(0,-b,a),
      new Vector3D(a,0,b),
      new Vector3D(0,b,a),
      new Vector3D(-b,a,0),
      new Vector3D(-b,-a,0),
      new Vector3D(b,-a,0),
      new Vector3D(b,a,0),
      new Vector3D(0,b,-a),
      new Vector3D(-a,0,-b),
      new Vector3D(0,-b,-a),
      new Vector3D(-a,0,b),
      new Vector3D(a,0,-b)
    };

    public static IcosahedronTriangle[] Triangles = new IcosahedronTriangle[]
    {
      new IcosahedronTriangle(Points[10],Points[0],Points[1]),
      new IcosahedronTriangle(Points[5],Points[1],Points[0]),
      new IcosahedronTriangle(Points[1],Points[5],Points[6]),
      new IcosahedronTriangle(Points[11],Points[6],Points[5]),
      new IcosahedronTriangle(Points[10],Points[1],Points[2]),
      new IcosahedronTriangle(Points[6],Points[2],Points[1]),
      new IcosahedronTriangle(Points[2],Points[6],Points[7]),
      new IcosahedronTriangle(Points[11],Points[7],Points[6]),
      new IcosahedronTriangle(Points[10],Points[2],Points[3]),
      new IcosahedronTriangle(Points[7],Points[3],Points[2]),
      new IcosahedronTriangle(Points[3],Points[7],Points[8]),
      new IcosahedronTriangle(Points[11],Points[8],Points[7]),
      new IcosahedronTriangle(Points[10],Points[3],Points[4]),
      new IcosahedronTriangle(Points[8],Points[4],Points[3]),
      new IcosahedronTriangle(Points[4],Points[8],Points[9]),
      new IcosahedronTriangle(Points[11],Points[9],Points[8]),
      new IcosahedronTriangle(Points[10],Points[4],Points[0]),
      new IcosahedronTriangle(Points[9],Points[0],Points[4]),
      new IcosahedronTriangle(Points[0],Points[9],Points[5]),
      new IcosahedronTriangle(Points[11],Points[5],Points[9]),
    };

    public static IcosahedronTile[] Tiles = new IcosahedronTile[]
    {
      new IcosahedronTile(Triangles[0],Triangles[1]),
      new IcosahedronTile(Triangles[2],Triangles[3]),
      new IcosahedronTile(Triangles[4],Triangles[5]),
      new IcosahedronTile(Triangles[6],Triangles[7]),
      new IcosahedronTile(Triangles[8],Triangles[9]),
      new IcosahedronTile(Triangles[10],Triangles[11]),
      new IcosahedronTile(Triangles[12],Triangles[13]),
      new IcosahedronTile(Triangles[14],Triangles[15]),
      new IcosahedronTile(Triangles[16],Triangles[17]),
      new IcosahedronTile(Triangles[18],Triangles[19]),
    };

    public static Vector3D Pole1 => Points[10];
    public static Vector3D Pole2 => Points[11];


  }
}
