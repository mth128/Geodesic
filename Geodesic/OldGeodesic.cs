using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class OldGeodesic
  {
    public List<SphericalTriangle> SphericalTriangles { get; }

    public OldGeodesic(int generation)
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
}
