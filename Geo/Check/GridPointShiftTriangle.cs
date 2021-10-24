using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Check
{
	public class GridPointShiftTriangle
  {
    public int Generation => TriangleIndex.Generation; 
    public TriangleIndex TriangleIndex { get; }
    public ShiftP Shift { get; }
    private FlatTriangle triangle;
    

    private double? area;
    public double Area
    {
      get
      {
        if (area == null)
          area = Triangle.Area;
        return area.Value; 
      }
    }

    public FlatTriangle Triangle
    {
      get
      {
        if (triangle == null)
        {
          PointIndex[] indices = TriangleIndex.PointIndices;
          GridPointShift[] points = new GridPointShift[]
          {
          new GridPointShift(new GridPoint(indices[0]), Shift),
          new GridPointShift(new GridPoint(indices[1]), Shift),
          new GridPointShift(new GridPoint(indices[2]), Shift),
          };
          triangle = new FlatTriangle(points[0].Point, points[1].Point, points[2].Point);
        }
        return triangle; 
      }
    }

    public GridPointShiftTriangle[] Children
    {
      get
      {
        TriangleIndex[] indices = TriangleIndex.Children;
        return new GridPointShiftTriangle[]
          {
            new GridPointShiftTriangle(indices[0].Generation,indices[0].Index,Shift),
            new GridPointShiftTriangle(indices[1].Generation,indices[1].Index,Shift),
            new GridPointShiftTriangle(indices[2].Generation,indices[2].Index,Shift),
            new GridPointShiftTriangle(indices[3].Generation,indices[3].Index,Shift),
          };
      }
    }

    public GridPointShiftTriangle(int generation, long index, ShiftP shift)
    {
      TriangleIndex = new TriangleIndex(generation, index);
      Shift = shift; 
    }



		public override string ToString()
		{
      return TriangleIndex.ToString(); 
		}
	}
}
