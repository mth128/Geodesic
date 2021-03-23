using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
	public class HybridGrid
	{
		private Geodesic geodesic;
		private int geodesicGeneration;
		private int bisectGeneration;
		public int TriangleCount { get; }
		public HybridGrid(int geodesicGeneration, int bisectGeneration)
		{
			this.geodesicGeneration = geodesicGeneration;
			this.bisectGeneration = bisectGeneration;
			geodesic = new Geodesic(geodesicGeneration - 2);
			TriangleCount = 1;
			for (int i = 0; i < bisectGeneration; i++)
				TriangleCount *= 4; 
		}

		public SphericalTriangle GetTriangle(int index)
		{
			int geodesicIndex = 0;
			int remainderIndex = index;
			int mask = 0;
			int masker = 3; 
			for (int i = 0; i < geodesicGeneration; i++)
			{
				mask |= masker;
				masker *= 4;
				remainderIndex /= 4;
			}
			geodesicIndex = index & mask;
			GeodesicGridTriangle triangle = geodesic.GetGridIndex(geodesicIndex).GeodesicGridTriangle;
			SphericalTriangle sphericalTriangle = new SphericalTriangle(triangle.PointAB, triangle.PointBC, triangle.PointCA);
			return sphericalTriangle.GetSubTriangle(remainderIndex, bisectGeneration - geodesicGeneration); 
		}
	}
}
