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
    public double D { get; }
    public Vector3D NearestToOrigin => nearestToOrigin == null? nearestToOrigin = UnitVector * D : nearestToOrigin; 

    public Plane (Vector3D unitVector, double distance)
    {
      UnitVector = unitVector;
      D = distance; 
    }

    public Line Intersection (Plane other)
    {
      Vector3D sharedVector = UnitVector.Cross(other.UnitVector).UnitVector;
      Vector3D difference = other.NearestToOrigin - NearestToOrigin;
      Vector3D intersectionPoint = NearestToOrigin + sharedVector*sharedVector.Dot(difference);
      return new Line(intersectionPoint, sharedVector); 
    }

    public double PartialSphereCapArea(Vector3D pointOnPlaneA, Vector3D pointOnPlaneB)
    {
      return (1 - D) * Math.Asin((pointOnPlaneB - pointOnPlaneA).Magnitude / (2*Math.Sqrt(1 - D * D)));
    }
  }
}
