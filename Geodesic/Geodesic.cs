using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class Geodesic
  {
    public static double IcosahedronRibLength { get; } = 4 / Math.Sqrt(10 + Math.Sqrt(20));
    public static double FrontViewLength { get; } = Math.Sqrt(1 - IcosahedronRibLength * IcosahedronRibLength / 4);
    public static double FrontViewLength13 { get; } = FrontViewLength / 3.0;
    public static double FrontViewLength23 { get; } = FrontViewLength13 * 2.0;
    public static Vector3D a { get; } = new Vector3D(-FrontViewLength23, 0, Math.Sqrt(1 - FrontViewLength23 * FrontViewLength23));
    public static Vector3D b { get; } = new Vector3D(FrontViewLength13, IcosahedronRibLength / 2, a.Z);
    public static Vector3D p { get; } = new Vector3D(a.X, 0, b.Z * -2);
    public static Vector3D c { get; } = b.Front.Normalized; 

    public Geodesic(int generation = 4)
    {


    }


  }
}
