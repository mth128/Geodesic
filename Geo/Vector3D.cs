﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo
{
  public class Vector3D
  {
    private static readonly double R120 = Math.Sqrt(3.0 / 4.0);
    public double X { get; }
    public double Y { get; }
    public double Z { get; }
    public double MagnitudeSquared => X * X + Y * Y + Z * Z;
    public double Magnitude => Math.Sqrt(MagnitudeSquared);

    public Vector3D UnitVector => this / Magnitude;
    public Vector3D Top => new Vector3D(X, Y, 0);
    public Vector3D Front => new Vector3D(X, 0, Z);
    public Vector3D Right => new Vector3D(0, Y, Z);
    public Vector3D RotateFront90 => new Vector3D(Z, Y, -X);
    public Vector3D RotateTop120 => new Vector3D(Y * R120 - X / 2, -X * R120 - Y / 2, Z);
    public Vector3D RotateTop240 => new Vector3D(-Y * R120 - X / 2, X * R120 - Y / 2, Z);
    public Vector3D RotateTop120Front => new Vector3D(Y * R120 - X / 2, 0, Z);

    public Vector3D RotateFront120 => new Vector3D(Z * R120 - X / 2, Y, -X * R120 - Z / 2);
    public Vector3D RotateFront240 => new Vector3D(-Z * R120 - X / 2, Y, X * R120 - Z / 2);

    public Vector3D(double x = 0, double y = 0, double z = 0)
    {
      X = x;
      Y = y;
      Z = z;
    }

    public Vector3D(Vector2D vector)
    {
      X = vector.x;
      Y = 0;
      Z = vector.y;
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
        Y * other.Z - Z * other.Y,
        Z * other.X - X * other.Z,
        X * other.Y - Y * other.X
        );
    }

    public double Dot(Vector3D other)
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

    public static Vector3D operator *(Vector3D a, double b)
    {
      return new Vector3D(a.X * b, a.Y * b, a.Z * b);
    }

    public static Vector3D operator *(Vector3D a, Vector3D b)
    {
      return new Vector3D(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    }

    public static Vector3D operator /(Vector3D a, double b)
    {
      return new Vector3D(a.X / b, a.Y / b, a.Z / b);
    }

    public static Vector3D operator -(Vector3D a)
    {
      return new Vector3D(-a.X, -a.Y, -a.Z);
    }

    public static double AngleBetween(Vector3D unitVectorA, Vector3D unitVectorB)
    {
      double magnitudeSquared = (unitVectorA - unitVectorB).MagnitudeSquared;
      if (magnitudeSquared > 2)
        magnitudeSquared = (-unitVectorA - unitVectorB).MagnitudeSquared;
      return (Math.Asin(Math.Sqrt(magnitudeSquared) / 2)) * 2;
    }

    public override string ToString()
    {
      return X.ToString() + ", " + Y.ToString() + ", " + Z.ToString();
    }

    public string ToString(bool noExponent)
    {
      return X.ToString("F99").TrimEnd('0') + ", " + Y.ToString("F99").TrimEnd('0') + ", " + Z.ToString("F99").TrimEnd('0');
    }

    public bool Is(Vector3D a)
    {
      return (this - a).MagnitudeSquared < 1e-20;
    }

  }
}
