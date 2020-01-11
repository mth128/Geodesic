using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class GeodesicGridTriangle
  {
    private double area = 0; 

    public Plane PlaneA { get; }
    public Plane PlaneB { get; }
    public Plane PlaneC { get; }

    public Vector3D PointAB { get; }
    public Vector3D PointBC { get; }
    public Vector3D PointCA { get; }

    public double Area
    {
      get
      {
        if (area != 0)
          return area;

        double sphericalTriangleArea = new SphericalTriangle(PointAB, PointBC, PointCA).Area;

        double sphericalTriangleAreaA = new SphericalTriangle(PointAB, PointCA, PlaneA.UnitVector).Area;
        double sphericalTriangleAreaB = new SphericalTriangle(PointBC, PointAB, PlaneB.UnitVector).Area;
        double sphericalTriangleAreaC = new SphericalTriangle(PointCA, PointBC, PlaneC.UnitVector).Area;

        double capAreaA = PlaneA.PartialSphereCapArea(PointAB, PointCA);
        double capAreaB = PlaneB.PartialSphereCapArea(PointBC, PointAB);
        double capAreaC = PlaneC.PartialSphereCapArea(PointCA, PointBC);

        area = sphericalTriangleArea - 
          (capAreaA - sphericalTriangleAreaA) - 
          (capAreaB - sphericalTriangleAreaB) - 
          (capAreaC - sphericalTriangleAreaC);

        return area; 
      }
    }

    public GeodesicGridTriangle(Plane a, Plane b, Plane c)
    {
      PlaneA = a;
      PlaneB = b;
      PlaneC = c;

      Line ab = a.Intersection(b);
      Line bc = b.Intersection(c);
      Line ca = c.Intersection(a);

      PointAB = ab.UnitSphereIntersectionPositiveY;
      PointBC = bc.UnitSphereIntersectionPositiveY;
      PointCA = ca.UnitSphereIntersectionPositiveY;
    }
  }
}
