using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo
{
  /// <summary>
  /// Index of an icosahedral geodesic grid point.
  /// Can also be used as CellIndex, except for the 2 polar points. 
  /// </summary>
  public class PointIndex
  {
    public int Generation => Parameters.Generation;
    public long Index { get; }
    public long Tile => Index / Parameters.TileSize;
    public long Row => Index % Parameters.TileSize / Parameters.Width;
    public long Column => Index % Parameters.Width;

    public GridParameters Parameters { get; }

    public bool IsPole1 => Index == Parameters.Pole1PointIndex;
    public bool IsPole2 => Index == Parameters.Pole2PointIndex;
    public bool IsPole => IsPole1 || IsPole2;

    public TriangleIndex TopRightTriangleIndex => new TriangleIndex(this, true);
    public TriangleIndex BottomLeftTriangleIndex => new TriangleIndex(this, false);
    public PointIndex DownRight => (Tile & 1) == 1 ? Right.Down : Down.Right;

    /// <summary>
    /// Moving a row up (-1), and adjust accordingly on a tile crossing. 
    /// </summary>
    public PointIndex Up
    {
      get
      {
        if (IsPole)
          throw new Exception("Pole does not have an up."); 

        long row = Row - 1;
        long column = Column;
        long tile = Tile; 

        if (row == -1)
        {
          if ((tile&1)==1)
          {
            tile = tile - 1;
            row = Parameters.Width - 1; 
          }
          else
          {
            if (column == 0)
            {
              row = 0;
              tile = tile == 0 ? 9 : tile - 1;
              column = Parameters.Width - 1;
            }
            else
            {
              tile = tile == 0 ? 8 : tile - 2;
              row = Parameters.Width - column;
              column = Parameters.Width - 1;
            }
          }
        }
        return new PointIndex(Generation, tile, row, column); 
      }
    }

    /// <summary>
    /// Moving a row down (+1), and adjust accordingly on a tile crossing. 
    /// </summary>
    public PointIndex Down
    {
      get
      {
        if (IsPole)
          throw new Exception("Pole does not have a down.");

        long row = Row + 1;
        long column = Column;
        long tile = Tile;

        if (row == Parameters.Width)
        {
          if ((tile & 1) == 1)
          {
            if (column == 0)
              return new PointIndex(Generation, false);
            tile = tile == 9 ? 1 : tile + 2;
            row = Parameters.Width - column; 
            column = 0; 
          }
          else
          {
            tile = tile + 1;
            row = 0;
          }
        }
        return new PointIndex(Generation, tile, row, column);
      }
    }

    /// <summary>
    /// Moving a column right (+1), and adjust accordingly on a tile crossing. 
    /// </summary>
    public PointIndex Right
    {
      get
      {
        if (IsPole)
          throw new Exception("Pole does not have a right.");
        long row = Row;
        long column = Column + 1;
        long tile = Tile; 

        if (column == Parameters.Width)
        {
          if ((tile&1)==1)
          {
            tile = tile == 9 ? 0 : tile + 1;
            column = 0; 
          }
          else
          {
            if (row == 0)
              return new PointIndex(Generation, true);
            tile = tile == 8 ? 0 : tile + 2; 
            column = Parameters.Width - row;
            row = 0; 
          }
        }
        return new PointIndex(Generation, tile, row, column); 
      }
    }

    /// <summary>
    /// Moving a column left (-1), and adjust accordingly on a tile crossing. 
    /// </summary>
    public PointIndex Left
    {
      get
      {
        if (IsPole)
          throw new Exception("Pole does not have a left.");
        long row = Row;
        long column = Column - 1;
        long tile = Tile; 
       
        if (column==-1)
        {
          if ((tile&1)==1)
          {
            if (row == 0)
            {
              tile = tile - 1;
              row = Parameters.Width;
              column = 0; 
            }
            else
            {
              tile = tile == 1 ? 9 : tile - 2;
              column = Parameters.Width - row; 
              row = Parameters.Width - 1; 
            }
          }
          else
          {
            tile = tile == 0 ? 9 : tile - 1;
            column = Parameters.Width - 1; 
          }
        }
        return new PointIndex(Generation, tile, row, column); 
      }
    }


    /// <summary>
    /// Moving a row up (-1), and adjust accordingly on a tile crossing. 
    /// </summary>
    public PointIndex UpCell
    {
      get
      {
        if (IsPole)
          throw new Exception("Pole does not have an up.");

        long row = Row - 1;
        long column = Column;
        long tile = Tile;

        if (row == -1)
        {
          if ((tile & 1) == 1)
          {
            tile = tile - 1;
            row = Parameters.Width - 1;
          }
          else
          {
            tile = tile == 0 ? 8 : tile - 2;
            row = Parameters.Width - column - 1;
            column = Parameters.Width - 1;
          }
        }
        return new PointIndex(Generation, tile, row, column);
      }
    }

    /// <summary>
    /// Moving a row down (+1), and adjust accordingly on a tile crossing. 
    /// </summary>
    public PointIndex DownCell
    {
      get
      {
        if (IsPole)
          throw new Exception("Pole does not have a down.");

        long row = Row + 1;
        long column = Column;
        long tile = Tile;

        if (row == Parameters.Width)
        {
          if ((tile & 1) == 1)
          {
            tile = tile == 9 ? 1 : tile + 2;
            row = Parameters.Width - column - 1;
            column = 0;
          }
          else
          {
            tile = tile + 1;
            row = 0;
          }
        }
        return new PointIndex(Generation, tile, row, column);
      }
    }

    /// <summary>
    /// Moving a column right (+1), and adjust accordingly on a tile crossing. 
    /// </summary>
    public PointIndex RightCell
    {
      get
      {
        if (IsPole)
          throw new Exception("Pole does not have a right.");
        long row = Row;
        long column = Column + 1;
        long tile = Tile;

        if (column == Parameters.Width)
        {
          if ((tile & 1) == 1)
          {
            tile = tile == 9 ? 0 : tile + 1;
            column = 0;
          }
          else
          {
            tile = tile == 8 ? 0 : tile + 2;
            column = Parameters.Width - row - 1;
            row = 0;
          }
        }
        return new PointIndex(Generation, tile, row, column);
      }
    }

    /// <summary>
    /// Moving a column left (-1), and adjust accordingly on a tile crossing. 
    /// </summary>
    public PointIndex LeftCell
    {
      get
      {
        if (IsPole)
          throw new Exception("Pole does not have a left.");
        long row = Row;
        long column = Column - 1;
        long tile = Tile;

        if (column == -1)
        {
          if ((tile & 1) == 1)
          {

            tile = tile == 1 ? 9 : tile - 2;
            column = Parameters.Width - row - 1;
            row = Parameters.Width-1;            
          }
          else
          {
            tile = tile == 0 ? 9 : tile - 1;
            column = Parameters.Width-1;
          }
        }
        return new PointIndex(Generation, tile, row, column);
      }
    }



    /// <summary>
    /// Moving a generation up.
    /// </summary>
    public PointIndex Parent 
    {
      get
      {
        if (Generation == 0)
          throw new Exception("Generation 0 point does not have a parent."); 
        if (IsPole1)
          return new PointIndex(Generation - 1, true);
        if (IsPole2)
          return new PointIndex(Generation - 1, false);
        return new PointIndex(Generation - 1, Tile, Row >> 1, Column >> 1); 
      }
    }

    /// <summary>
    /// Moving a generation down. 
    /// </summary>
    public PointIndex[] Children
    {
      get
      {
        if (IsPole1)
          return new PointIndex[] { new PointIndex(Generation + 1, true) };
        if (IsPole2)
          return new PointIndex[] { new PointIndex(Generation + 1, false) };

        long r = Row * 2;
        long c = Column * 2;
        long t = Tile;
        int g = Generation+1;

        return new PointIndex[]
        {
          new PointIndex(g, t, r, c),
          new PointIndex(g, t, r, c+1),
          new PointIndex(g, t, r+1, c),
          new PointIndex(g, t, r+1, c+1),
        };
      }
    }

    public PointIndex(int generation)
    {
      if (generation < 0)
        throw new IndexOutOfRangeException("Generation should not be negative."); 
      Parameters = new GridParameters(generation);
    }

    /// <summary>
    /// Use this constructor to get the polar point. 
    /// </summary>
    /// <param name="generation">The generation of the grid</param>
    /// <param name="pole1">true: first pole, false: second pole</param>
    public PointIndex(int generation, bool pole1) : this(generation)
    {
      Index = pole1 ? Parameters.Pole1PointIndex : Parameters.Pole2PointIndex;
    }

    /// <summary>
    /// Standard constructor
    /// </summary>
    /// <param name="generation">The generation of the grid</param>
    /// <param name="tile">The tile index</param>
    /// <param name="row">The row index</param>
    /// <param name="column">The column index</param>
    public PointIndex(int generation, long tile, long row, long column) : this(generation)
    {
      if (row < 0 || row >= Parameters.Width || column < 0 || column >= Parameters.Width || tile > 9 || tile < 0)
        throw new IndexOutOfRangeException();

      Index = tile * Parameters.TileSize + row * Parameters.Width + column;
    }

    public PointIndex(int generation, long index):this(generation)
    {
      if (index < 0 || index >= Parameters.PointCount)
        throw new IndexOutOfRangeException("Geodesic Point Index out of range.");
      Index = index; 
    }

    public TriangleIndex RibSharingTriangle(TriangleIndex triangleIndex)
    {
      if (BottomLeftTriangleIndex.SharesOneRibWith(triangleIndex))
        return BottomLeftTriangleIndex;
      else if (!TopRightTriangleIndex.SharesOneRibWith(triangleIndex))
        throw new Exception("Cell is not sharing a rib with the given triangle.");
      return TopRightTriangleIndex; 
    }

    public static bool operator ==(PointIndex a, PointIndex b) => a.Generation == b.Generation && a.Index == b.Index;
    public static bool operator !=(PointIndex a, PointIndex b) => a.Generation != b.Generation || a.Index != b.Index;

    public override string ToString() => Index.ToString();

		public override bool Equals(object obj)
		{
			return obj is PointIndex index &&
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
