using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo
{
  public class IcosahedronTile
  {
    public IcosahedronTriangle A { get; }
    public IcosahedronTriangle B { get; }
    public IcosahedronTile (IcosahedronTriangle a, IcosahedronTriangle b)
    {
      A = a;
      B = b;
    }
  }
}
