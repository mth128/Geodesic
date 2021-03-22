using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class BisectGeodesic
  {
    public List<SphericalTriangle> SphericalTriangles { get; }

    public BisectGeodesic(int generation)
    {
      List<SphericalTriangle> current = new List<SphericalTriangle>();
      current.Add(new SphericalTriangle(Geodesic.ArcLeft, Geodesic.ArcLeft.RotateTop120, Geodesic.ArcLeft.RotateTop240));

      for (int i =0; i<generation;i++)
      {
        List<SphericalTriangle> next = new List<SphericalTriangle>();
        foreach (SphericalTriangle triangle in current)
          next.AddRange(triangle.Bisect());
        current = next; 
      }
      SphericalTriangles = current; 
    }

  }
  public class BisectGeodesicLowMemory
  {
    public int generation; 
    private static SphericalTriangle baseTriangle = new SphericalTriangle(Geodesic.ArcLeft, Geodesic.ArcLeft.RotateTop120, Geodesic.ArcLeft.RotateTop240);
    private int triangleCount;
    public int TriangleCount => triangleCount; 
    public SphericalTriangle GetTriangle(int index)
    {
      SphericalTriangle triangle = baseTriangle;
      for (int i = 0; i < generation; i++)
      {
        int subIndex = index & 3;
        triangle = triangle.Bisect()[subIndex];
        index >>= 2; 
      }
      return triangle; 
    }

    public BisectGeodesicLowMemory(int generation)
    {
      this.generation = generation;
      triangleCount = 1;
      for (int i = 0; i < generation; i++)
        triangleCount *= 4; 
    }

  }
}
