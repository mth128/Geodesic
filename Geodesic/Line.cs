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
      Point = point;
      UnitVector = unitVector;
      Point -= unitVector * unitVector.Dot(point);
      unitSphereIntersection1 = null;
      unitSphereIntersection2 = null; 
    }
    public static Line Construct(Vector3D from, Vector3D to)
    {
      return new Line(from, (to - from).UnitVector); 
    }

    public Vector3D IntersectFront(Line other)
    {
      double halfAngle = Vector3D.AngleBetween(UnitVector, -other.UnitVector)/2;

      double halfDistance = (Point - other.Point).Magnitude / 2;

      Vector3D otherVector = other.UnitVector; 

      double vectorMagnitudeSquared = (UnitVector - otherVector).MagnitudeSquared;
      if (vectorMagnitudeSquared > 2)
      {
        otherVector = -otherVector; 
        vectorMagnitudeSquared = (UnitVector - otherVector).MagnitudeSquared;
      }

      double halfVectorDistance = Math.Sqrt(vectorMagnitudeSquared) / 2; 


      double distance = halfDistance / halfVectorDistance; 

      return Point - UnitVector * distance; 



    }
  }
}
