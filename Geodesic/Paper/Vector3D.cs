using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic.Paper
{
  public class Vector3D
  {
    public double x;
    public double y;
    public double z;

    public Vector3D (double x, double y, double z)
    {
      this.x = x;
      this.y = y;
      this.z = z; 
    }

    public Vector3D Cross(Vector3D o)
    {
      return new Vector3D(
        y * o.z - z * o.y,
        z * o.x - x * o.z,
        x * o.y - y * o.x
        );
    }

    public double Dot(Vector3D o)
    {
      return x * o.x + y * o.y + z * o.z; 
    }



  }
}
