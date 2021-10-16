using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo
{
  public class GridParameters
  {
    public int Generation { get; }

    public const int tileCount = 10;

    public int TileCount => tileCount;

    /// <summary>
    /// The amount of cell rows or cell columns per tile. 
    /// </summary>
    public long Width => 1l << Generation;
    /// <summary>
    /// The amount of cells per tile (and the amount of subtriangles per base triangle). 
    /// </summary>
    public long TileSize => 1l << (2 * Generation);

    /// <summary>
    /// The amount of cells in the entire grid. 
    /// </summary>
    public long CellCount => TileCount * TileSize;
    public long TriangleCount => 2*TileCount * TileSize;
    public long PointCount => CellCount + 2;

    public long Pole1PointIndex => CellCount;
    public long Pole2PointIndex => CellCount + 1;

    public GridParameters(int generation)
    {
      Generation = generation; 
    }
  }
}
