using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Check
{
	public class ShiftP
	{
		public Vector3D P { get; } 
		public ShiftP(double value)
		{
			P = Paper.p* value; 
		}

		/// <summary>
		/// Returns the cut point for a given value between 0 and 1. 
		/// 0.5 strikes through the midpoint m.
		/// 0 strikes through the base point a. 
		/// 1 strikes accross the longest part of the triangle and through the center point. 
		/// </summary>
		/// <param name="g"></param>
		/// <returns></returns>
		public Vector3D GetCutPoint(double g)
		{
			if (g == 1)
				return Paper.b;
			if (g == 0)
				return Paper.a;

			List<bool> sequence = Paper.GetNSequence(g);

			Vector3D result = Paper.m;

			foreach (bool b in sequence)
				result = b ? F1(result) : F0(result);

			return result;
		}


		public Vector3D F0(Vector3D s)
		{
			Vector3D af = new Vector3D(Paper.a.Z, Paper.a.Y, -Paper.a.X);//ff
			Vector3D sxz = new Vector3D(s.X, 0, s.Z);//fxz
			Vector3D slv = (sxz - P).UnitVector;
			Vector3D slp = (P - slv * P.Dot(slv));
			Vector3D su = slp + slv * Math.Sqrt(1 - slp.MagnitudeSquared);//fu
			Vector3D srccw_xy = new Vector3D(-su.Y * Paper.sqr34 - su.X / 2, 0, su.Z); //fxy frccw
			Vector3D sow = Paper.a * Paper.a.Dot(srccw_xy) + af * (af.Dot(srccw_xy) / Paper.s);//fow
			slv = (sow - Paper.pow).UnitVector;
			slp = (Paper.pow - slv * Paper.pow.Dot(slv));
			su = slp + slv * Math.Sqrt(1 - slp.MagnitudeSquared);//fu
			Vector3D siw = Paper.a * Paper.a.Dot(su) + af * (af.Dot(su) * Paper.s);//fiw
			Vector3D sy = new Vector3D(siw.X, Math.Sqrt(1 - siw.X * siw.X - siw.Z * siw.Z), siw.Z);
			return sy;
		}

		public Vector3D F1(Vector3D s)
		{
			Vector3D q = F0(s);
			return new Vector3D(-q.Y * Paper.sqr34 - q.X / 2, q.X * Paper.sqr34 - q.Y / 2, q.Z);
		}
	}
}
