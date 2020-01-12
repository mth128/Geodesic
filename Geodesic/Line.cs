using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class Line
  {
    private Vector3D unitSphereIntersection1;
    private Vector3D unitSphereIntersection2;
    public Vector3D Point { get; }
    public Vector3D UnitVector { get; }

    public Vector3D UnitSphereIntersection1 =>
      unitSphereIntersection1 == null ?
      unitSphereIntersection1 = Point + UnitVector * Math.Sqrt(1 - Point.MagnitudeSquared)
      : unitSphereIntersection1; 

    public Vector3D UnitSphereIntersection2 =>
      unitSphereIntersection2 == null ?
      unitSphereIntersection2 = Point - UnitVector * Math.Sqrt(1 - Point.MagnitudeSquared)
      : unitSphereIntersection2;

    public Vector3D UnitSphereIntersectionPositiveZ => UnitSphereIntersection1.Z >= 0 ? UnitSphereIntersection1 : UnitSphereIntersection2; 

    public Line(Vector3D point, Vector3D unitVector)
    {
      UnitVector = unitVector;
      double distance = unitVector.Dot(point);
      Vector3D shift = unitVector * distance;
      Point = point - shift;
    }
    public static Line Construct(Vector3D from, Vector3D to)
    {
      return new Line(from, (to - from).UnitVector); 
    }

    /// <summary>
    /// Calculates the intersection between 2 lines. 
    /// There is no check for coplanarness and parallelness. That is your responsibility. 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Vector3D Intersect(Line other)
    {
      Vector3D q = other.Point - Point;
      double partA = UnitVector.Dot(q);
      double between = q.Magnitude;
      double perpendicular = Math.Sqrt(between * between - partA * partA);
      Vector3D r = Point - UnitVector * partA;
      double partD = (r - other.Point).Dot(other.UnitVector);
      double partC = Math.Sqrt(perpendicular * perpendicular - partD * partD);
      double partB = partC / partD * perpendicular;
      Vector3D i = r - UnitVector * partB;
      return i; 
    }
  }
}
