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

		public FlatTriangle(Vector3D a, Vector3D b, Vector3D c)
		{
			A = a;
			B = b;
			C = c;
			AB = Line.Construct(A, B);
			BC = Line.Construct(B, C);
			CA = Line.Construct(C, A); 
		}

		


	}
}
