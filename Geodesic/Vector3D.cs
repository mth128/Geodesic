using Computable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class Vector3D
  {
    private static readonly Equation R120 = MathE.Sqrt((new Equation(3)/4));
    public Equation X { get; }
    public Equation Y { get; }
    public Equation Z { get; }
    public Equation MagnitudeSquared => X * X + Y * Y + Z * Z;
    public Equation Magnitude => MathE.Sqrt(MagnitudeSquared);
    public Vector3D UnitVector => this / Magnitude;
    public Vector3D Top => new Vector3D(X, Y, 0);
    public Vector3D Front => new Vector3D(X, 0, Z);
    public Vector3D Right => new Vector3D(0, Y, Z);
    public Vector3D RotateFront90 => new Vector3D(Z, Y, -X);
    public Vector3D RotateTop120 => new Vector3D(Y * R120 - X / 2, -X * R120 - Y / 2, Z);
    public Vector3D RotateTop240 => new Vector3D(-Y * R120 - X / 2, X * R120 - Y / 2, Z);    
    public Vector3D RotateTop120Front => new Vector3D(Y * R120 - X / 2, 0, Z);

    public Vector3D(Equation x, Equation y, int z)
      : this(x, y, new Equation(z)) { }
    public Vector3D(Equation x, int y, Equation z)
      : this(x, new Equation(y), z) { }
    public Vector3D(Equation x, int y, int z)
      : this(x, new Equation(y), new Equation(z)) { }
    public Vector3D(int x, Equation y, Equation z)
      : this(new Equation(x), y, z) { }
    public Vector3D(int x, Equation y, int z)
      : this(new Equation(x), y, new Equation(z)) { }
    public Vector3D(int x, int y, Equation z)
      : this(new Equation(x), new Equation(y), z) { }
    public Vector3D(int x, int y, int z)
      : this(new Equation(x), new Equation(y), new Equation(z)) { }

    public Vector3D(Equation x=null, Equation y=null, Equation z=null)
    {
      this.X = x ?? new Equation(0);
      this.Y = y ?? new Equation(0);
      this.Z = z ?? new Equation(0); 
    }


    public static Vector3D operator +(Vector3D a, Vector3D b)
    {
      return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    public static Vector3D operator -(Vector3D a, Vector3D b)
    {
      return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    public Vector3D Cross(Vector3D other)
    {
      return new Vector3D(
        Y*other.Z-Z*other.Y,
        Z*other.X-X*other.Z,
        X*other.Y-Y*other.X
        );
    }

    public Equation Dot(Vector3D other)
    {
      return X * other.X + Y * other.Y + Z * other.Z; 
    }


    public static bool operator ==(Vector3D a, Vector3D b)
    {
      if (object.ReferenceEquals(a, null))
        return (object.ReferenceEquals(b, null));
      if (object.ReferenceEquals(b, null))
        return false; 
      return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
    }

    public static bool operator !=(Vector3D a, Vector3D b)
    {
      if (object.ReferenceEquals(a, null))
        return (!object.ReferenceEquals(b, null));
      if (object.ReferenceEquals(b, null))
        return true;
      return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
    }

    public static Vector3D operator *(Vector3D a, IValue b)
    {
      return new Vector3D(a.X * b, a.Y * b, a.Z * b);
    }

    public static Vector3D operator /(Vector3D a, IValue b)
    {
      return new Vector3D(a.X/b,a.Y/b,a.Z/b); 
    }
    public static Vector3D operator *(Vector3D a, int b)
    {
      Equation i = new Equation(b);
      return new Vector3D(a.X * i, a.Y * i, a.Z * i);
    }

    public static Vector3D operator /(Vector3D a, int b)
    {
      Equation i = new Equation(b);
      return new Vector3D(a.X / i, a.Y / i, a.Z / i);
    }



    public static Vector3D operator-(Vector3D a)
    {
      return new Vector3D(-a.X, -a.Y, -a.Z);
    }

    public static double AngleBetween(Vector3D unitVectorA, Vector3D unitVectorB)
    {
      Equation magnitudeSquared = (unitVectorA - unitVectorB).MagnitudeSquared;
      if (magnitudeSquared>2)
        magnitudeSquared = (-unitVectorA - unitVectorB).MagnitudeSquared;
      return (MathE.Asin(MathE.Sqrt(magnitudeSquared)/2)) * 2;
    }

    public override string ToString()
    {
      return X.ToString() + ", " + Y.ToString() + ", " + Z.ToString();
    }
  }

}
