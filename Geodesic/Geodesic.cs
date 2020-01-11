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

    /// <summary>
    /// Standard coordinate large component. (Arc Left)
    /// </summary>
    public static Vector3D a { get; } = new Vector3D(-FrontViewLength23, 0, Math.Sqrt(1 - FrontViewLength23 * FrontViewLength23));
    /// <summary>
    /// Standard coordinate small component. (Arc Right) 
    /// </summary>
    public static Vector3D b { get; } = new Vector3D(FrontViewLength13, IcosahedronRibLength / 2, a.Z);
    /// <summary>
    /// Primary projection point. 
    /// </summary>
    public static Vector3D p { get; } = new Vector3D(a.X, 0, b.Z * -2);


    /// <summary>
    /// Arc top right.
    /// </summary>
    public static Vector3D c { get; } = b.Front.UnitVector;
    /// <summary>
    /// Arc top front. (Primary strike through point) 
    /// </summary>
    public static Vector3D d { get; } = c.RotateTop120;

    public static Vector3D mirrorPoint { get; } = ScaleEllipseOut(d.Front); 

    public static double EllipseSecondaryRadius = Math.Cos(0.2*Math.PI);

    public Geodesic(int generation = 4)
    {


    }

    public static Vector3D ScaleEllipseOut(Vector3D vector)
    {
      Vector3D primary = a;
      Vector3D secondary = a.RotateFront90;
      double dPrimary = vector.Dot(primary);
      double dSecondary = vector.Dot(secondary);
      return primary * dPrimary + secondary * (dSecondary / EllipseSecondaryRadius);
    }

    public static Vector3D ScaleEllipseIn(Vector3D vector)
    {
      Vector3D primary = a;
      Vector3D secondary = a.RotateFront90;
      double dPrimary = vector.Dot(primary);
      double dSecondary = vector.Dot(secondary);
      return primary * dPrimary + secondary * (dSecondary * EllipseSecondaryRadius);
    }

    public static VectorPair NextFront(Vector3D projectionPoint, Vector3D seed)
    {
      Line line = Line.Construct(projectionPoint,seed.Front);
      Vector3D top = line.UnitSphereIntersectionPositiveY;
      Vector3D rotatedTop = top.RotateTop120Front;
      Vector3D target = ScaleEllipseOut(rotatedTop);
      Line strikeThrough = Line.Construct(ScaleEllipseOut(projectionPoint), target);
      Vector3D primaryScaled = strikeThrough.UnitSphereIntersectionPositiveY;
      Vector3D secondaryScaled = MirrorFront(primaryScaled);
      return new VectorPair(ScaleEllipseIn(primaryScaled), ScaleEllipseIn(secondaryScaled)); 
    }

    private static Vector3D MirrorFront(Vector3D unitVectorFront)
    {
      Vector3D mirrorPerpendicular = new Vector3D(-mirrorPoint.Z, 0, mirrorPoint.X);
      double distance = mirrorPoint.Dot(unitVectorFront);
      Vector3D result = unitVectorFront + mirrorPerpendicular * distance * 2;
      return result; 
    }

  }
}
