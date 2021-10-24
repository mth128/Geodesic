using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Check
{
	public class GridPointShift
	{
    private Vector3D point;
    public Vector3D Point
    {
      get
      {
        if (point == null)
          CalculatePoint();
        return point;
      }
    }
    public GridPoint GridPoint { get; }
		public ShiftP ShiftP { get; }
		public GridPointShift(GridPoint gridPoint, ShiftP shiftP)
		{
			GridPoint = gridPoint;
			ShiftP = shiftP;
		}
    private void CalculatePoint()
    {
      if (GridPoint.Index.IsPole1)
        point = Icosahedron.Pole1;
      else if (GridPoint.Index.IsPole2)
        point = Icosahedron.Pole2;
      else
      {
        PointIndex index = GridPoint.Index;

        //projection point method. 
        double width = GridPoint.Parameters.Width;
        double sliceColumn = index.Column / width;
        double sliceRow = index.Row / width;

        double sliceFromB;
        double sliceFromC;
        long baseTriangle = index.Tile * 2;

        if (index.Row > index.Column)
        {
          baseTriangle += 1;
          sliceFromB = 1 - sliceColumn;
          sliceFromC = sliceRow;
        }
        else
        {
          sliceFromB = sliceColumn;
          sliceFromC = 1 - sliceRow;
        }

        Vector3D fromB = ShiftP.GetCutPoint(sliceFromB);
        Vector3D fromC = ShiftP.GetCutPoint(sliceFromC);

        Plane bPlane = new Plane(ShiftP.P, ShiftP.P + new Vector3D(0, 1, 0), fromB);
        Plane cPlane = new Plane(ShiftP.P, ShiftP.P + new Vector3D(0, 1, 0), fromC);

        bPlane = bPlane.RotateTop240;
        cPlane = cPlane.RotateTop120;

        Vector3D baseGridPoint = bPlane.Intersection(cPlane).UnitSphereIntersectionPositiveZ;
        Vector3D projectionPointGridPoint = Icosahedron.Triangles[baseTriangle].ToWorld(baseGridPoint);
        point = projectionPointGridPoint;        
      }
		}
	}
}
