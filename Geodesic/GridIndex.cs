using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class GridIndex
  {
    private GeodesicGridTriangle geodesicGridTriangle; 
    public int Width { get; }
    public int A { get; }
    public int B { get; }
    public int C { get; }
    public bool Inverted { get; }
    public Geodesic Parent { get; }
    public GeodesicGridTriangle GeodesicGridTriangle =>
      geodesicGridTriangle == null ? geodesicGridTriangle =
      new GeodesicGridTriangle(Parent.GetPlane(A), Parent.GetPlane(B).RotateTop120, Parent.GetPlane(C).RotateTop240, Inverted) :
      geodesicGridTriangle; 

    public GridIndex(Geodesic geodesic, int index)
    {
      Parent = geodesic; 
      Width = geodesic.MaxRibIndex; 
      A = index % Width;
      B = index / Width;
      C = Width - A - B - 1; 
      Inverted = A >= Width - B;
      if (Inverted)
      {
        A = Width - A;
        B = Width - B;
        C = Width - A - B + 1; 
      }
    }

  }
}
