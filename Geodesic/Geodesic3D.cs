using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class Geodesic3D
  {
    public int Levels {get;} 

    public Geodesic3D(int levels)
    {
      Levels = levels; 
    }

    /// <summary>
    /// The amount of points for a single level. 
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public static long PointCountOfLevel(long level)
    {
      return 2 + 10 * level * level;
    }

    /// <summary>
    /// The sum amount of points for all levels from 0 to (and including) the given level
    /// </summary>
    /// <param name="levels"></param>
    /// <returns></returns>
    public static long PointCountOfLevels(long levels)
    {
      //10 x four sided pyramids + 2 polar points for each level + center point. 
      //https://mathworld.wolfram.com/PyramidalNumber.html
      return (1 / 6) * levels * (levels + 1) * (2 * levels + 1) * 10 + 2 * levels + 1; 
    }

  }
}
