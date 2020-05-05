using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class Vector2D
  {
    public double x;
    public double y;

    public double MagnitudeSquared => x * x + y * y;
    public double Magnitude => Math.Sqrt(MagnitudeSquared);
    public Vector2D UnitVector => this / Magnitude;

    public Vector2D Rotate90 => new Vector2D(y, -x);

    public static Vector2D XZOf(Vector3D point)
    {
      return new Vector2D(point.X.Value, point.Z.Value); 
    }

    public Vector2D(double x, double y)
    {
      this.x = x;
      this.y = y; 
    }

    public static Vector2D operator +(Vector2D a, Vector2D b)
    {
      return new Vector2D(a.x + b.x, a.y + b.y);
    }

    public static Vector2D operator -(Vector2D a, Vector2D b)
    {
      return new Vector2D(a.x - b.x, a.y - b.y);
    }

    public static Vector2D operator *(Vector2D a, double b)
    {
      return new Vector2D(a.x *b, a.y * b);
    }

    public static Vector2D operator /(Vector2D a, double b)
    {
      return new Vector2D(a.x / b, a.y / b);
    }

    public double Dot(Vector2D other)
    {
      return x * other.x + y * other.y;
    }
    public override string ToString()
    {
      return x.ToString() + " " + y.ToString(); 
    }

  }
}
