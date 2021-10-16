using Geo.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo
{
	public class FlatTriangle
	{
		public Vector3D A { get; }
		public Vector3D B { get; }
		public Vector3D C { get; }

		public Line AB;
		public Line BC;
		public Line CA;

		public double Area => (B - A).Magnitude * (AB.DistanceTo(C)) / 2;

		public Vector3D Center => (A + B + C) / 2;

		public Vector3D Normal
		{
			get
			{
				Vector3D v = AB.UnitVector.Cross(BC.UnitVector).UnitVector;
				if (v.Dot(A) < 0)
					v = -v;
				return v;
			}
		}

		public FlatTriangle(Vector3D a, Vector3D b, Vector3D c)
		{
			A = a;
			B = b;
			C = c;
			AB = Line.Construct(A, B);
			BC = Line.Construct(B, C);
			CA = Line.Construct(C, A);
		}
			
		public double Orthogonality(FlatTriangle other)
		{
			List<Vector3D> shared = new List<Vector3D>();
			if (A.Is(other.A) || A.Is(other.B) || A.Is(other.C))
				shared.Add(A);
			if (B.Is(other.A) || B.Is(other.B) || B.Is(other.C))
				shared.Add(B);
			if (C.Is(other.A) || C.Is(other.B) || C.Is(other.C))
				shared.Add(C);
			if (shared.Count != 2)
				throw new Exception("Cannot calculate orthogonality, because triangles do not share 2 points.");

			if (shared[0] == shared[1])
				throw new Exception("Triangle has rib length of 0");
			Line lineA = Line.Construct(shared[0], shared[1]);
			Line lineB = Line.Construct(Center, other.Center);
			Minimum minimum = new Minimum();
			minimum.Add((lineA.UnitVector + lineB.UnitVector).MagnitudeSquared);
			minimum.Add((lineA.UnitVector - lineB.UnitVector).MagnitudeSquared);
			return minimum.min;
		}

		public static FlatTriangle GetTriangle(int projectionPointGeneration, int bisectGeneration, long triangleIndex)
    {
			if (projectionPointGeneration < 0 || bisectGeneration < 0 || bisectGeneration < projectionPointGeneration)
				throw new Exception("Invalid generation.");

			TriangleIndex index = new TriangleIndex(bisectGeneration, triangleIndex);
			PointIndex[] pointIndices = index.PointIndices;

			GridPoint[] gridPoints = new GridPoint[]
			{
					new GridPoint(projectionPointGeneration, bisectGeneration, pointIndices[0].Index),
					new GridPoint(projectionPointGeneration, bisectGeneration, pointIndices[1].Index),
					new GridPoint(projectionPointGeneration, bisectGeneration, pointIndices[2].Index)
			};

			return new FlatTriangle(gridPoints[0].Point, gridPoints[1].Point, gridPoints[2].Point);
		}

		public static FlatTriangle GetTriangle(int projectionPointGeneration, int index)
    {
			return GetTriangle(projectionPointGeneration, projectionPointGeneration, index); 
    }

	}
}
