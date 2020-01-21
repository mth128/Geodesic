using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class Plane
  {
    private Vector3D nearestToOrigin; 
    public Vector3D UnitVector { get; }
    public TraceCompute D { get; }
    public Vector3D NearestToOrigin => nearestToOrigin == null? nearestToOrigin = UnitVector * D : nearestToOrigin;

    public Plane RotateTop120 => new Plane(UnitVector.RotateTop120, D);
    public Plane RotateTop240 => new Plane(UnitVector.RotateTop240, D); 
    
    public Plane (Vector3D unitVector, TraceCompute distance)
    {
      UnitVector = unitVector;
      D = distance; 
    }
       
    public Plane (Vector3D a, Vector3D b, Vector3D c)
    {
      UnitVector = (a - b).Cross(c - b).UnitVector;
      D = a.UnitVector.Dot(UnitVector) * a.Magnitude; 
    }


    public TraceCompute DistanceTo(Vector3D point)
    {
      return UnitVector.Dot(point) - D; 
    }

    public Line Intersection (Plane other)
    {
      Vector3D sharedVector = UnitVector.Cross(other.UnitVector).UnitVector;
      Line line1 = new Line(NearestToOrigin, sharedVector.Cross(UnitVector).UnitVector);
      Line line2 = new Line(other.NearestToOrigin, sharedVector.Cross(other.UnitVector).UnitVector);
      Vector3D intersectionPoint = line1.Intersect(line2); 
      return new Line(intersectionPoint, sharedVector); 
    }

    public TraceCompute PartialSphereCapArea(Vector3D pointOnPlaneA, Vector3D pointOnPlaneB)
    {
      TraceCompute height = new TraceCompute(1) - D.Abs();
      TraceCompute radius = (new TraceCompute(1) - D.Squared()).Sqrt();
      TraceCompute unitDistance = (pointOnPlaneB - pointOnPlaneA).Magnitude / radius;
      TraceCompute angle = (unitDistance / 2).Asin() * 2;
      return height * angle; 
    }

    internal bool SameSide(Vector3D a, Vector3D b)
    {
      TraceCompute da = DistanceTo(a);
      TraceCompute db = DistanceTo(b);
      if (da >= 0 && db >= 0)
        return true;
      if (da <= 0 && db <= 0)
        return true;
      return false; 
    }
  }
}
