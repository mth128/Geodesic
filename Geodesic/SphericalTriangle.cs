﻿using System;
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

    public TraceCompute Area
    {
      get
      {
        Vector3D Ab = A.Cross(B).Cross(A).UnitVector;
        Vector3D Ac = A.Cross(C).Cross(A).UnitVector;
        Vector3D Ba = B.Cross(A).Cross(B).UnitVector;
        Vector3D Bc = B.Cross(C).Cross(B).UnitVector;
        Vector3D Ca = C.Cross(A).Cross(C).UnitVector;
        Vector3D Cb = C.Cross(B).Cross(C).UnitVector;

        TraceCompute a = Vector3D.AngleBetween(Ab, Ac);
        TraceCompute b = Vector3D.AngleBetween(Ba, Bc);
        TraceCompute c = Vector3D.AngleBetween(Ca, Cb);

        return a + b + c - TraceCompute.PI(); 
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

  }

}
