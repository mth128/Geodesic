using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo
{
  public class IcosahedronTriangle
  {
    public Vector3D A { get; }
    public Vector3D B { get; }
    public Vector3D C { get; }


    public Vector3D X { get; }
    public Vector3D Y { get; }
    public Vector3D Z { get; }

    public IcosahedronTriangle(Vector3D a, Vector3D b, Vector3D c)
    {
      A = a;
      B = b;
      C = c;

      X = ((b + c) / 2 - a).UnitVector;
      Y = (c - b).UnitVector;
      Z = X.Cross(Y);
    }

    public Vector3D ToStandard(Vector3D point) => new Vector3D(X.Dot(point), Y.Dot(point), Z.Dot(point));
    public FlatTriangle ToStandard(FlatTriangle triangle) => new FlatTriangle(ToStandard(triangle.A), ToStandard(triangle.B), ToStandard(triangle.C));     
    public Vector3D ToWorld(Vector3D point) => X*point.X + Y*point.Y + Z*point.Z;

  }
}
