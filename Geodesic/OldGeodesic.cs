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

    /// <summary>
    /// Returns the triangle of interest as [0], and the three neighbouring triangles. 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public SphericalTriangle[] GetTriangleAndNeighbourTriangles(int index)
    {
      SphericalTriangle triangle = baseTriangle;
      List<SphericalTriangle> connectedTriangles = new List<SphericalTriangle>(); 
      
      for (int i = 0; i < generation; i++)
      {
        List<SphericalTriangle> newConnectedTriangles = new List<SphericalTriangle>(); 
        int subIndex = index & 3;
        SphericalTriangle[] subTriangles = triangle.Bisect();
        triangle = subTriangles[subIndex];
        index >>= 2;

        for (int s = 0; s < 4; s++)
          if (s != subIndex && triangle.SharesTwoPoints(subTriangles[s]))
            newConnectedTriangles.Add(subTriangles[s]);

        foreach (SphericalTriangle oldConnectedTriangle in connectedTriangles)
          foreach (SphericalTriangle subConnected in oldConnectedTriangle.Bisect())
            if (subConnected.SharesTwoPoints(triangle))
              newConnectedTriangles.Add(subConnected);

        connectedTriangles = newConnectedTriangles; 
      }

      if (connectedTriangles.Count < 3)
      {
        bool ab = false;
        bool bc = false;
        bool ca = false; 
        foreach (SphericalTriangle connect in connectedTriangles)
        {
          bool a = connect.ContainsPoint(triangle.A);
          bool b = connect.ContainsPoint(triangle.B);
          bool c = connect.ContainsPoint(triangle.C);
          if (a && b)
            ab = true;
          if (b && c)
            bc = true;
          if (c && a)
            ca = true; 
        }

        if (!ab)
          connectedTriangles.Add(triangle.MirrorAB());
        if (!bc)
          connectedTriangles.Add(triangle.MirrorBC());
        if (!ca)
          connectedTriangles.Add(triangle.MirrorCA()); 
      }

      connectedTriangles.Insert(0, triangle); 
      return connectedTriangles.ToArray();
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
