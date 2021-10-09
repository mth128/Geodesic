using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  /// <summary>
  /// A spherical triangle is a triangle on a Unit Sphere that is bounded by three unit vectors. 
  /// </summary>
  public class SphericalTriangle
  {
    public Vector3D A { get; }
    public Vector3D B { get; }
    public Vector3D C { get; }

    public double Area
    {
      get
      {
        Vector3D Ab = A.Cross(B).Cross(A).UnitVector;
        Vector3D Ac = A.Cross(C).Cross(A).UnitVector;
        Vector3D Ba = B.Cross(A).Cross(B).UnitVector;
        Vector3D Bc = B.Cross(C).Cross(B).UnitVector;
        Vector3D Ca = C.Cross(A).Cross(C).UnitVector;
        Vector3D Cb = C.Cross(B).Cross(C).UnitVector;

        double a = Vector3D.AngleBetween(Ab, Ac);
        double b = Vector3D.AngleBetween(Ba, Bc);
        double c = Vector3D.AngleBetween(Ca, Cb);

        return a + b + c - Math.PI; 
      }
    }
       
    public SphericalTriangle(Vector3D unitVectorA, Vector3D unitVectorB, Vector3D unitVectorC)
    {
      A = unitVectorA;
      B = unitVectorB;
      C = unitVectorC;
    }

    public SphericalTriangle[] Bisect()
    {
      Vector3D ab = ((A + B) / 2).UnitVector;
      Vector3D bc = ((B + C) / 2).UnitVector;
      Vector3D ca = ((C + A) / 2).UnitVector;
      SphericalTriangle[] result = new SphericalTriangle[4];

      result[0] = new SphericalTriangle(A, ab, ca);
      result[1] = new SphericalTriangle(B, bc, ab);
      result[2] = new SphericalTriangle(C, ca, bc);
      result[3] = new SphericalTriangle(ab, bc, ca);
      
      return result;
    }

    public SphericalTriangle GetSubTriangle(int index, int generations)
    {
      SphericalTriangle triangle = this; 
      for (int i = 0; i < generations; i++)
      {
        int subIndex = index & 3;
        triangle = triangle.Bisect()[subIndex];
        index >>= 2;
      }
      return triangle;
    }

    public bool SharesTwoPoints(SphericalTriangle other)
    {
      int countMatch = 0;
      if (A == other.A)
        countMatch++;
      if (A == other.B)
        countMatch++;
      if (A == other.C)
        countMatch++;

      if (B == other.A)
        countMatch++;
      if (B == other.B)
        countMatch++;
      if (B == other.C)
        countMatch++;

      if (C == other.A)
        countMatch++;
      if (C == other.B)
        countMatch++;
      if (C == other.C)
        countMatch++;

      return countMatch == 2; 

    }
    public bool ContainsPoint(Vector3D p)
    {
      return A == p || B == p || C == p; 
    }

		internal SphericalTriangle MirrorAB()
		{
      Plane mirrorPlane = new Plane(A, B, new Vector3D(0, 0, 0));
      Vector3D c = mirrorPlane.Mirror(C);
      return new SphericalTriangle(B, A, c); 
		}

		internal SphericalTriangle MirrorBC()
		{
      Plane mirrorPlane = new Plane(B, C, new Vector3D(0, 0, 0));
      Vector3D a = mirrorPlane.Mirror(A);
      return new SphericalTriangle(C, B, a);
    }

		internal SphericalTriangle MirrorCA()
		{
      Plane mirrorPlane = new Plane(C, A, new Vector3D(0, 0, 0));
      Vector3D b = mirrorPlane.Mirror(B);
      return new SphericalTriangle(A, C, b);
    }
	}

}
