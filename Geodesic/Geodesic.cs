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
    public static double FrontViewLength { get; } = Math.Sqrt(IcosahedronRibLength * IcosahedronRibLength - IcosahedronRibLength * IcosahedronRibLength / 4);
    public static double FrontViewLength13 { get; } = FrontViewLength / 3.0;
    public static double FrontViewLength23 { get; } = FrontViewLength13 * 2.0;

    /// <summary>
    /// Standard coordinate large component. (Arc Left)
    /// </summary>
    public static Vector3D ArcLeft { get; } = new Vector3D(-FrontViewLength23, 0, Math.Sqrt(1 - FrontViewLength23 * FrontViewLength23));
    /// <summary>
    /// Standard coordinate small component. (Arc Right) 
    /// </summary>
    public static Vector3D ArcRight { get; } = new Vector3D(FrontViewLength13, IcosahedronRibLength / 2, ArcLeft.Z);
    /// <summary>
    /// Primary projection point. 
    /// </summary>
    public static Vector3D DefaultProjectionPoint { get; } = new Vector3D(ArcLeft.X, 0, ArcRight.Z * -2);

    /// <summary>
    /// Arc top right.
    /// </summary>
    public static Vector3D ArcTopRight { get; } = ArcRight.Front.UnitVector;
    /// <summary>
    /// Arc top front. (Primary strike through point) 
    /// </summary>
    public static Vector3D ArcTopFront { get; } = ArcTopRight.RotateTop120;

    public static double EllipseSecondaryRadius = Math.Cos(0.2 * Math.PI);
    public static Vector3D MirrorPoint { get; } = ScaleEllipseOut(ArcTopFront.Front);
    public static Vector3D MirrorPerpendicular = MirrorPoint.RotateFront90;

    public Vector3D ProjectionPoint { get; }

    public List<StrikeThroughPointPair> StrikeThroughPoints { get; }

    public Geodesic(int generation = 4, Vector3D projectionPoint = null)
    {
      if (projectionPoint == null)
        projectionPoint = DefaultProjectionPoint;

      ProjectionPoint = projectionPoint;

      StrikeThroughPointPair center = new StrikeThroughPointPair(0);
      StrikeThroughPointPair bound = new StrikeThroughPointPair(ArcLeft.Dot(MirrorPerpendicular));
      StrikeThroughPointPair initialPair = new StrikeThroughPointPair(this, ArcTopFront); 

      List<StrikeThroughPointPair> strikePoints = new List<StrikeThroughPointPair>();
      List<StrikeThroughPointPair> current = new List<StrikeThroughPointPair>();
      current.Add(initialPair);
      strikePoints.Add(initialPair); 
      for (int i =0; i<generation;i++)
      {
        List<StrikeThroughPointPair> next = new List<StrikeThroughPointPair>(); 
        foreach (StrikeThroughPointPair pair in current)
        {
          StrikeThroughPointPair first = new StrikeThroughPointPair(this,pair.Primary);
          StrikeThroughPointPair second = new StrikeThroughPointPair(this, pair.Secondary);

          next.Add(first);
          next.Add(second); 
        }
        strikePoints.AddRange(next); 
        current = next;
      }
      strikePoints.Add(center);
      strikePoints.Add(bound);
      StrikeThroughPoints = strikePoints.OrderBy(o => o.DistanceToScaledCenterLine).ToList();
      
    }

    public static Vector3D ScaleEllipseOut(Vector3D vector)
    {
      Vector3D primary = ArcLeft;
      Vector3D secondary = ArcLeft.RotateFront90;
      double dPrimary = vector.Dot(primary);
      double dSecondary = vector.Dot(secondary);
      return primary * dPrimary + secondary * (dSecondary / EllipseSecondaryRadius);
    }

    public static Vector3D ScaleEllipseIn(Vector3D vector)
    {
      Vector3D primary = ArcLeft;
      Vector3D secondary = ArcLeft.RotateFront90;
      double dPrimary = vector.Dot(primary);
      double dSecondary = vector.Dot(secondary);
      return primary * dPrimary + secondary * (dSecondary * EllipseSecondaryRadius);
    }



  }
}
