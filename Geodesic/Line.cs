//using Computable;
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

    /*
    public Vector3D UnitSphereIntersection1 =>
      unitSphereIntersection1 == null ?
      unitSphereIntersection1 = Point + UnitVector * MathE.Sqrt(new Equation(1) - Point.MagnitudeSquared)
      : unitSphereIntersection1;
      */
    public Vector3D UnitSphereIntersection1 =>
    unitSphereIntersection1 == null ?
    unitSphereIntersection1 = Point + UnitVector * Math.Sqrt(1 - Point.MagnitudeSquared)
    : unitSphereIntersection1;

    public Vector3D UnitSphereIntersection2 =>
      unitSphereIntersection2 == null ?
      unitSphereIntersection2 = Point - UnitVector * Math.Sqrt(1 - Point.MagnitudeSquared)
      : unitSphereIntersection2;

    /*
    public Vector3D UnitSphereIntersection2 =>
      unitSphereIntersection2 == null ?
      unitSphereIntersection2 = Point - UnitVector * MathE.Sqrt(new Equation(1) - Point.MagnitudeSquared)
      : unitSphereIntersection2;
      */
    public Vector3D UnitSphereIntersectionPositiveZ => UnitSphereIntersection1.Z >= 0 ? UnitSphereIntersection1 : UnitSphereIntersection2;

    /*
    public Line(Vector3D point, Vector3D unitVector)
    {
      UnitVector = unitVector;
      Equation distance = unitVector.Dot(point);
      Vector3D shift = unitVector * distance;
      Point = point - shift;
    }*/
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

    public double DistanceTo(Vector3D point)
    {
      if (point == Point)
        return 0;
      Vector3D dif = Point - point;
      double dot = Math.Abs(dif.UnitVector.Dot(UnitVector));

      if (dot > 1)
        if (dot > 1.01)
          throw new Exception("Error in calculating distance.");
        else
          return 0;

      return dif.Magnitude * Math.Sqrt(1 - dot*dot);
    }

    /*
    public Equation DistanceTo(Vector3D point)
    {
      if (point == Point)
        return new Equation(0);
      Vector3D dif = Point - point;
      Equation dot = MathE.Abs(dif.UnitVector.Dot(UnitVector));

      if (dot > 1)
        if (dot > 1.01)
          throw new Exception("Error in calculating distance.");
        else
          return new Equation(0); 

      return dif.Magnitude * MathE.Sqrt(1 - MathE.Squared(dot)); 
    }*/

    public double AngleBetween(Line other)
    {
      return 2 * Math.Asin((UnitVector + other.UnitVector).Magnitude / 2); 
    }

    public double AngleBetweenDegree(Line other)
    {
      return AngleBetween(other) / Math.PI * 180; 
    }

    /// <summary>
    /// Calculates the intersection between 2 lines. 
    /// There is no check for coplanarness and parallelness. That is your responsibility. 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Vector3D Intersect(Line other)
    {
      //https://math.stackexchange.com/questions/270767/find-intersection-of-two-3d-lines/271366

      if (DistanceTo(other.Point) == 0)
        return other.Point;
      if (other.DistanceTo(Point) == 0)
        return Point; 

      Vector3D e = UnitVector;
      Vector3D f = other.UnitVector;
      Vector3D c = Point;
      Vector3D d = other.Point; 

      Vector3D offset = e * f.Cross(d-c).Magnitude / f.Cross(e).Magnitude;

      Vector3D a = c + offset;
      Vector3D b = c - offset;

      //selecting a or b. 
      //Equation aDist = DistanceTo(a) + other.DistanceTo(a);
      //Equation bDist = DistanceTo(b) + other.DistanceTo(b);

      double aDist = DistanceTo(a) + other.DistanceTo(a);
      double bDist = DistanceTo(b) + other.DistanceTo(b);

      return aDist < bDist ? a : b;

      /*
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
      */
    }
  }
}
