using Computable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geodesic
{
  public class Geodesic
  {
    public static Equation IcosahedronRibLength { get; } = new Equation(4) / MathE.Sqrt(new Equation(10) + MathE.Sqrt(new Equation(20)));
    public static Equation FrontViewLength { get; } = MathE.Sqrt(new Fraction(new Product(MathE.Squared(IcosahedronRibLength), 3), 4));
    public static Equation FrontViewLength13 { get; } = FrontViewLength / 3;
    public static Equation FrontViewLength23 { get; } = FrontViewLength13 * 2;

    /// <summary>
    /// Standard coordinate large component. (Arc Left)
    /// </summary>
    public static Vector3D ArcLeft { get; } = new Vector3D(-FrontViewLength23, 0, MathE.Sqrt(new Equation(1) - FrontViewLength23 * FrontViewLength23));
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

    public static Equation EllipseSecondaryRadius = (MathE.Sqrt(new Equation(5))+ 1) / 4; 
    public static Vector3D MirrorPoint { get; } = ScaleEllipseOut(ArcTopFront.Front);
    public static Vector3D MirrorPerpendicular = MirrorPoint.RotateFront90;

    public Vector3D ProjectionPoint { get; }
    public Vector3D ProjectionPointScaledOut { get; }

    public List<StrikeThroughPointPair> StrikeThroughPoints { get; }

    public int Generation { get; }

    public int MaxRibIndex { get; }
    public int MaxGridIndex => MaxRibIndex * MaxRibIndex; 


    public Geodesic(int generation = 4, Vector3D projectionPoint = null)
    {
      Generation = generation; 
      if (projectionPoint == null)
        projectionPoint = DefaultProjectionPoint;

      ProjectionPoint = projectionPoint;
      ProjectionPointScaledOut = ScaleEllipseOut(ProjectionPoint); 

      StrikeThroughPointPair center = new StrikeThroughPointPair(0);
      StrikeThroughPointPair bound = new StrikeThroughPointPair(ArcLeft.Dot(MirrorPerpendicular));

      StrikeThroughPointPair.minimalSigma = center.Sigma;
      StrikeThroughPointPair.maximalSigma = bound.Sigma;

      List<StrikeThroughPointPair> strikePoints = new List<StrikeThroughPointPair>();
      if (generation > -1)
      {
        StrikeThroughPointPair initialPair = new StrikeThroughPointPair(this, ArcTopFront,1);

        List<StrikeThroughPointPair> current = new List<StrikeThroughPointPair>();
        current.Add(initialPair);
        strikePoints.Add(initialPair);
        for (int i = 0; i < generation; i++)
        {
          List<StrikeThroughPointPair> next = new List<StrikeThroughPointPair>();
          foreach (StrikeThroughPointPair pair in current)
          {
            StrikeThroughPointPair first = new StrikeThroughPointPair(this, pair.Right, pair.RightIndex);
            StrikeThroughPointPair second = new StrikeThroughPointPair(this, pair.Left, pair.LeftIndex);
            next.Add(first);
            next.Add(second);
          }
          strikePoints.AddRange(next);
          current = next;
        }
      }
      strikePoints.Add(center);
      MaxRibIndex = strikePoints.Count * 2; 
      strikePoints.Add(bound);
      StrikeThroughPoints = strikePoints.OrderBy(o => o.DistanceToScaledCenterLine.Value).ToList();

      Equation count = new Equation(StrikeThroughPoints.Count); 

      for (int i = 0; i<StrikeThroughPoints.Count;i++)
        StrikeThroughPoints[i].onArcValue = new Equation(i) / (count - 1);
    }

    public Vector3D GetStrikePoint(int index)
    {
      if (index<StrikeThroughPoints.Count-1)
        return StrikeThroughPoints[StrikeThroughPoints.Count - index - 1].Right;

      return StrikeThroughPoints[index - StrikeThroughPoints.Count+1].Left; 
    }

    public Plane GetPlane(int index)
    {
      return GetPlane(GetStrikePoint(index)); 
    }

    public Plane GetPlane(Vector3D strikePoint)
    {
      return new Plane(ProjectionPoint, ProjectionPoint + new Vector3D(0, 1, 0), strikePoint); 
    }

    public GridIndex GetGridIndex(int index)
    {
      return new GridIndex(this, index); 
    }

    public double Sigma(Vector3D point)
    {
      Line line = Line.Construct(ScaleEllipseOut(point), ProjectionPointScaledOut);
      Vector3D scaledOutPoint = line.UnitSphereIntersectionPositiveZ;
      Equation distanceToScaledCenterLine = MirrorPerpendicular.Dot(scaledOutPoint);
      return MathE.Asin(distanceToScaledCenterLine);
    }

    public static Vector3D ScaleEllipseOut(Vector3D vector)
    {
      Vector3D primary = ArcLeft;
      Vector3D secondary = ArcLeft.RotateFront90;
      Equation dPrimary = vector.Dot(primary);
      Equation dSecondary = vector.Dot(secondary);
      return primary * dPrimary + secondary * (dSecondary / EllipseSecondaryRadius);
    }



    public static Vector3D ScaleEllipseIn(Vector3D vector)
    {
      Vector3D primary = ArcLeft;
      Vector3D secondary = ArcLeft.RotateFront90;
      Equation dPrimary = vector.Dot(primary);
      Equation dSecondary = vector.Dot(secondary);
      return primary * dPrimary + secondary * (dSecondary * EllipseSecondaryRadius);
    }
  }
}
