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
    /// <summary>
    /// The first cut plane index. 
    /// </summary>
    public int A { get; }
    /// <summary>
    /// The second cut plane index.
    /// </summary>
    public int B { get; }
    /// <summary>
    /// The third cut plane index 
    /// </summary>
    public int C { get; }

    public int TileIndex { get; } = 0; 

    public bool Inverted { get; }

    public bool Invalid { get; }
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

    internal GridIndex(Geodesic geodesic, int a, int b, int c, bool inverted)
    {
      Parent = geodesic;
      Width = geodesic.MaxRibIndex;
      Inverted = inverted;
      A = a;
      B = b;
      C = c;

      if (inverted && (a == 0 || b == 0 || c == 0))
      {
        Invalid = true; 
      }
    }

    /// <summary>
    /// Gets the index of the neighbouring triangle. 
    /// </summary>
    /// <param name="n">0 flip around A, 1 flip around B or 2 flip around C</param>
    /// <returns></returns>
    public GridIndex GetNeighbour(int n)
    {
      if (Inverted)
      {
        if (n == 0)
          return new GridIndex(Parent, A, B - 1, C - 1, false);
        if (n == 1)
          return new GridIndex(Parent, A - 1, B, C - 1, false);
        if (n == 2)
          return new GridIndex(Parent, A - 1, B - 1, C, false); 
      }
      else
      {
        if (n == 0)
          return new GridIndex(Parent, A, B + 1, C + 1, true);
        if (n == 1)
          return new GridIndex(Parent, A + 1, B, C + 1, true);
        if (n == 2)
          return new GridIndex(Parent, A + 1, B + 1, C, true); 
      }
      throw new Exception("Getneighbour: n should be 0, 1, or 2"); 
    }

    public GridIndex[] GetNeighbours()
    {
      return new GridIndex[] { GetNeighbour(0), GetNeighbour(1), GetNeighbour(2) }; 
    }
  }
}
