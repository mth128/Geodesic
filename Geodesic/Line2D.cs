using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  class Line2D
  {
    public Vector2D Point { get; }
    public Vector2D UnitVector { get; }

    public Vector2D Reach1 => Point + UnitVector * Math.Sqrt(1 - Point.MagnitudeSquared);
    public Line2D(Vector2D point, Vector2D unitVector)
    {
      UnitVector = unitVector;
      double distance = unitVector.Dot(point);
      Vector2D shift = unitVector * distance;
      Point = point - shift;
    }
    public static Line2D Construct(Vector2D from, Vector2D to)
    {
      return new Line2D(from, (to - from).UnitVector);
    }
  }
}
