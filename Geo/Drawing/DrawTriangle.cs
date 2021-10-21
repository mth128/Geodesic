using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Drawing
{
  public class DrawTriangle
  {
    public Vector3D A => Triangle.A;
    public Vector3D B => Triangle.B;
    public Vector3D C => Triangle.C; 

    public FlatTriangle Triangle { get; } 
    public Color lineColor = Color.Black;
    public Color fillColor = Color.Red; 

    public DrawTriangle(FlatTriangle triangle)
    {
      Triangle = triangle; 
    }
  }
}
