using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo
{
  public class TriangleIndex
  {
    public GridParameters Parameters { get; }
    public int Generation => Parameters.Generation; 
    public long Index { get; }
    public long BaseTriangleIndex => Index / Parameters.TileSize;
    public long SubTriangleIndex => Index % Parameters.TileSize;

    public long SliceRowIndex => SubTriangleIndex / Parameters.Width;
    public long SliceColumnIndex => SubTriangleIndex % Parameters.Width;

    public bool Inverted => SliceColumnIndex + SliceRowIndex >= Parameters.Width;

    public bool BottomLeft => Inverted ^ ((BaseTriangleIndex & 1) == 1);
    public bool TopRight => !BottomLeft;

    public TriangleIndex[] Neighbours
    {
      get
      {
        List<TriangleIndex> neighbours = new List<TriangleIndex>(); 
        if (TopRight)
        {
          return new TriangleIndex[]
          {
            new TriangleIndex(CellIndex, false),
            CellIndex.RightCell.RibSharingTriangle(this),
            CellIndex.UpCell.RibSharingTriangle(this)
          };
        }
        else
        {
          return new TriangleIndex[]
          {
            new TriangleIndex(CellIndex, true),
            CellIndex.DownCell.RibSharingTriangle(this),
            CellIndex.LeftCell.RibSharingTriangle(this)
          };
        }
      }
    }

    public PointIndex CellIndex
    {
      get
      {
        long baseTriangleIndex = BaseTriangleIndex;        
        long subIndex = Inverted ? Parameters.TileSize - SubTriangleIndex - 1 : SubTriangleIndex;
        long tileIndex = baseTriangleIndex / 2;
        long row = subIndex % Parameters.Width;
        long column = subIndex / Parameters.Width;

        if ((baseTriangleIndex & 1) == 1)
          row = Parameters.Width - row - 1;
        else
          column = Parameters.Width - column - 1;
        
        return new PointIndex(Generation, tileIndex, row, column); 
      }
    }

    public TriangleIndex[] Children
    {
      get
      {
        PointIndex cell = CellIndex;
        PointIndex[] cellChildren = cell.Children;
        
        //first is r, c
        //second is r, c+1
        //third is r+1, c
        //fourth is r+1, c+1
        
        TriangleIndex[] children;
        if (BottomLeft)
        {
          children = new TriangleIndex[]
            {
              cellChildren[0].BottomLeftTriangleIndex,
              cellChildren[2].BottomLeftTriangleIndex,
              cellChildren[3].BottomLeftTriangleIndex,
              cellChildren[2].TopRightTriangleIndex
            };
        }
        else
          children = new TriangleIndex[]
            {
              cellChildren[0].TopRightTriangleIndex,
              cellChildren[1].TopRightTriangleIndex,
              cellChildren[3].TopRightTriangleIndex,
              cellChildren[1].BottomLeftTriangleIndex
            };

        return children;
      }
    }


    /// <summary>
    /// Getting the points in clockwise orientation with the grid diagonal first. 
    /// </summary>
    public PointIndex[] PointIndices
    {
      get
      {
        PointIndex basePoint = CellIndex;

        if (TopRight)
        {
          return new PointIndex[]
          {
            basePoint.Right,
            basePoint,
            basePoint.DownRight,
          };
        }
        else
        {
          return new PointIndex[]
          {            
            basePoint.Down,    
            basePoint.DownRight,
            basePoint,
          };
        }
      }
      
    }


    private TriangleIndex(int generation)
    {
      Parameters = new GridParameters(generation);
    }

    public TriangleIndex (int generation, long index):this(generation)
    {
      if (index < 0 || index >= Parameters.TriangleCount)
        throw new IndexOutOfRangeException("Triangle index out of range."); 

      Index = index; 
    }

    public TriangleIndex (PointIndex cellIndex, bool topRight):this(cellIndex.Generation)
    {
      if (cellIndex.IsPole)
        throw new Exception("Pole is not a cell and has no triangles.");

      long baseTriangleIndex = cellIndex.Tile * 2;

      if (topRight)
      {
        //From the triangle perspective, rows and columns are inverted. 
        long sliceColumnIndex = cellIndex.Row;
        long sliceRowIndex = Parameters.Width - cellIndex.Column-1;
        long subTriangleIndex = sliceRowIndex * Parameters.Width + sliceColumnIndex;
        if (cellIndex.Row > cellIndex.Column)
          baseTriangleIndex += 1;
        Index = baseTriangleIndex * Parameters.TileSize + subTriangleIndex;
      }
      else
      {
        //From the triangle perspective, rows and columns are inverted. 
        long sliceColumnIndex = Parameters.Width - cellIndex.Row - 1;
        long sliceRowIndex = cellIndex.Column;
        long subTriangleIndex = sliceRowIndex * Parameters.Width + sliceColumnIndex;
        if (cellIndex.Row >= cellIndex.Column)
          baseTriangleIndex += 1;
        Index = baseTriangleIndex * Parameters.TileSize + subTriangleIndex;
      }
    }

    public bool SharesOneRibWith(TriangleIndex other)
    {
      int sharedPoints = 0;

      foreach (PointIndex a in PointIndices)
        foreach (PointIndex b in other.PointIndices)
          if (a.Index == b.Index)
            sharedPoints++;
      return sharedPoints == 2; 
    }

    public static bool operator == (TriangleIndex a, TriangleIndex b) => a.Generation == b.Generation && a.Index == b.Index;
    public static bool operator != (TriangleIndex a, TriangleIndex b) => a.Generation != b.Generation || a.Index != b.Index;
    public override string ToString() => Index.ToString();

		public override bool Equals(object obj)
		{
			return obj is TriangleIndex index &&
						 Generation == index.Generation &&
						 Index == index.Index;
		}

		public override int GetHashCode()
		{
			int hashCode = 480583098;
			hashCode = hashCode * -1521134295 + Generation.GetHashCode();
			hashCode = hashCode * -1521134295 + Index.GetHashCode();
			return hashCode;
		}
	}
}
