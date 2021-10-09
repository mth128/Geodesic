//using Computable;
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
    //public Equation D { get; }
    public double D { get; }

    public Vector3D NearestToOrigin => nearestToOrigin == null? nearestToOrigin = UnitVector * D : nearestToOrigin;

    public Plane RotateTop120 => new Plane(UnitVector.RotateTop120, D);
    public Plane RotateTop240 => new Plane(UnitVector.RotateTop240, D); 
    
    public Plane (Vector3D unitVector, double distance)
    {
      UnitVector = unitVector;
      D = distance; 
    }
       
    public Plane (Vector3D a, Vector3D b, Vector3D c)
    {
      UnitVector = (a - b).Cross(c - b).UnitVector;
      D = a.UnitVector.Dot(UnitVector) * a.Magnitude; 
    }

    /*
    public Equation DistanceTo(Vector3D point)
    {
      return UnitVector.Dot(point) - D; 
    }*/
    public double DistanceTo(Vector3D point)
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

    /*
    public double PartialSphereCapArea(Vector3D pointOnPlaneA, Vector3D pointOnPlaneB)
    {
      Equation height = new Equation(1) - MathE.Abs(D);
      Equation radius = MathE.Sqrt(new Equation(1) - MathE.Squared(D));
      Equation unitDistance = (pointOnPlaneB - pointOnPlaneA).Magnitude / radius;
      double angle = MathE.Asin(unitDistance / 2) * 2;
      return height * angle; 
    }*/
    public double PartialSphereCapArea(Vector3D pointOnPlaneA, Vector3D pointOnPlaneB)
    {
      double height = 1 - Math.Abs(D);
      double radius = Math.Sqrt(1 - D*D);
      double unitDistance = (pointOnPlaneB - pointOnPlaneA).Magnitude / radius;
      double angle = Math.Asin(unitDistance / 2) * 2;
      return height * angle;
    }

    /*
    internal bool SameSide(Vector3D a, Vector3D b)
    {
      Equation da = DistanceTo(a);
      Equation db = DistanceTo(b);
      if (da >= 0 && db >= 0)
        return true;
      if (da <= 0 && db <= 0)
        return true;
      return false; 
    }*/
    internal bool SameSide(Vector3D a, Vector3D b)
    {
      double da = DistanceTo(a);
      double db = DistanceTo(b);
      if (da >= 0 && db >= 0)
        return true;
      if (da <= 0 && db <= 0)
        return true;
      return false;
    }

		public Vector3D Mirror(Vector3D v)
		{
      return v - UnitVector * 2 * DistanceTo(v);
		}
	}
}
