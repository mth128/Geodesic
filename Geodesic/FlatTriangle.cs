﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
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

		public FlatTriangle(SphericalTriangle triangle) : this(triangle.A, triangle.B, triangle.C)
		{

		}

		public FlatTriangle(GridIndex index) : this(index.GeodesicGridTriangle.PointAB, 
			index.GeodesicGridTriangle.PointBC, index.GeodesicGridTriangle.PointCA) 
		{
			if (index.Invalid)
			{
				if (!index.Inverted)
					throw new Exception("Cannot uninvalidate this triangle");
				Vector3D a;
				Vector3D b;
				Vector3D c; 

				//uninvalidating
				if (index.A == 0)
				{
					GridIndex originalIndex = new GridIndex(index.Parent, index.A, index.B - 1, index.C - 1, false);
					GeodesicGridTriangle mirrored = originalIndex.GeodesicGridTriangle;
					a = mirrored.PointAB;
					b = mirrored.PointCA;
					c = mirrored.PointBC;
				}
				else if (index.B == 0)
				{
					GridIndex originalIndex = new GridIndex(index.Parent, index.A - 1, index.B, index.C - 1, false);
					GeodesicGridTriangle mirrored = originalIndex.GeodesicGridTriangle;
					a = mirrored.PointBC;
					b = mirrored.PointAB;
					c = mirrored.PointCA;
				}
				else if (index.C == 0)
				{
					GridIndex originalIndex = new GridIndex(index.Parent, index.A - 1, index.B - 1, index.C, false);
					GeodesicGridTriangle mirrored = originalIndex.GeodesicGridTriangle;
					a = mirrored.PointCA;
					b = mirrored.PointBC;
					c = mirrored.PointAB;
				}
				else
				{
					throw new Exception("Cannot uninvalidate this triangle"); 
				}
				A = a;
				B = b;
				C = new Plane(a,b,new Vector3D(0,0,0)).Mirror(c);
				AB = Line.Construct(A, B);
				BC = Line.Construct(B, C);
				CA = Line.Construct(C, A);
			}
		}


		public double Orthogonality(FlatTriangle other)
		{
			List<Vector3D> shared = new List<Vector3D>();
			if (A.Is( other.A) || A.Is(other.B) || A.Is(other.C))
				shared.Add(A);
			if (B.Is(other.A) || B.Is(other.B) || B.Is(other.C))
				shared.Add(B);
			if (C.Is(other.A) || C.Is( other.B) || C.Is(other.C))
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

	}
}
