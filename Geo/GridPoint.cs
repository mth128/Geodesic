using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo
{
  public class GridPoint
  {
    private Vector3D point; 
    public int ProjectionPointGeneration { get; }
    public GridParameters Parameters => Index.Parameters;
    public PointIndex Index { get; }
    public Vector3D Point
    {
      get
      {
        if (point == null)
        {
          CalculatePoint();
        }
        return point; 
      }
    }

    public GridPoint(int projectionPointGeneration, int bisectGeneration, long index)
    {
      ProjectionPointGeneration = projectionPointGeneration;
      Index = new PointIndex(projectionPointGeneration > bisectGeneration ? projectionPointGeneration : bisectGeneration, index); 
    }

    public GridPoint(int generation, long index) : this(generation, generation, index) 
    {

    }

    public GridPoint(PointIndex index): this(index.Generation,index.Index)
    {

    }
   
    private void CalculatePoint()
    {
      if (Index.IsPole1)
        point = Icosahedron.Pole1;
      else if (Index.IsPole2)
        point = Icosahedron.Pole2;
      else
      {        
        PointIndex index = Index;

        if (Index.Generation == ProjectionPointGeneration)
        {
          //projection point method. 
          double width = Parameters.Width;
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

          Vector3D fromB = Paper.GetCutPoint(sliceFromB);
          Vector3D fromC = Paper.GetCutPoint(sliceFromC);

          Plane bPlane = new Plane(Paper.p, Paper.p + new Vector3D(0, 1, 0), fromB);
          Plane cPlane = new Plane(Paper.p, Paper.p + new Vector3D(0, 1, 0), fromC);

          bPlane = bPlane.RotateTop240;
          cPlane = cPlane.RotateTop120;

          Vector3D baseGridPoint = bPlane.Intersection(cPlane).UnitSphereIntersectionPositiveZ;
          Vector3D projectionPointGridPoint = Icosahedron.Triangles[baseTriangle].ToWorld(baseGridPoint);
          point = projectionPointGridPoint;
        }
        else 
        {
          //bisect method. 
          while (index.Generation > ProjectionPointGeneration)
            index = index.Parent;

          long remainingWidth = 1L << (Index.Generation - index.Generation);
          long remainingColumns = Index.Column % remainingWidth;
          long remainingRows = Index.Row % remainingWidth;

          if (remainingColumns == 0 && remainingRows == 0)
          {
            point = new GridPoint(index).Point;
            return;
          }
          Vector3D topLeft = new GridPoint(index).Point; 
          Vector3D bottomRight = new GridPoint(index.DownRight).Point;
          Vector3D topRight = new GridPoint(index.Right).Point;
          Vector3D bottomLeft = new GridPoint(index.Down).Point;

          for (int g = index.Generation; g<Index.Generation; g++)
          {
            remainingWidth >>= 1; 

            if (remainingRows < remainingWidth)
            {
              if (remainingColumns < remainingWidth)
              {
                Vector3D center = Bisect(topLeft, bottomRight);
                Vector3D top = Bisect(topLeft, topRight);
                Vector3D left = Bisect(topLeft, bottomLeft);

                topRight = top;
                bottomLeft = left;
                bottomRight = center;
              }
              else
              {
                Vector3D center = Bisect(topLeft, bottomRight);
                Vector3D top = Bisect(topLeft, topRight);
                Vector3D right = Bisect(topRight, bottomRight);

                topLeft = top;
                bottomLeft = center;
                bottomRight = right;
              }
            }
            else if (remainingColumns < remainingWidth)
            {
              Vector3D center = Bisect(topLeft, bottomRight);
              Vector3D left = Bisect(topLeft, bottomLeft);
              Vector3D bottom = Bisect(bottomLeft, bottomRight);

              topLeft = left;
              topRight = center;
              bottomRight = bottom;
            }
            else
            {
              Vector3D center = Bisect(topLeft, bottomRight);
              Vector3D bottom = Bisect(bottomLeft, bottomRight);
              Vector3D right = Bisect(topRight, bottomRight);

              topLeft = center;
              topRight = right;
              bottomLeft = bottom;
            }

            remainingColumns &= ~remainingWidth;
            remainingRows &= ~remainingWidth;

            if (remainingColumns == 0 && remainingRows == 0)
              break; 
          }
          point = topLeft;
        }
      }
    }

    public Vector3D Bisect(Vector3D a, Vector3D b) => ((a + b) / 2).UnitVector; 

  }
}
