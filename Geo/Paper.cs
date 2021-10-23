using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo
{
	public static class Paper
	{
		public static readonly double sqr34 = Math.Sqrt(3.0 / 4); 
		public static readonly Vector3D a = new Vector3D(-2 * Math.Sqrt(2 / (15 + Math.Sqrt(45))), 0,
			Math.Sqrt(1 / 3.0 + 2 / (3 * Math.Sqrt(5))));

		public static readonly Vector3D b = new Vector3D(Math.Sqrt(2 / (15 + Math.Sqrt(45))), Math.Sqrt(2 / (5 + Math.Sqrt(5))),
			Math.Sqrt(1 / 3.0 + 2 / (3 * Math.Sqrt(5))));

		public static readonly Vector3D p = new Vector3D(a.X, 0, -2 * a.Z); 

		public static readonly Vector3D m = new Vector3D(-Math.Sqrt((3 - Math.Sqrt(5)) / 24), 
			(Math.Sqrt(5) - 1) / 4,	Math.Sqrt((3 + Math.Sqrt(5)) / 6));
		
		public static readonly double s = (1 + Math.Sqrt(5)) / 4;
		public static readonly Vector3D pow = a * a.Dot(p) + Ff(a) * (Ff(a).Dot(p) / ((1 + Math.Sqrt(5)) / 4));//fow p
				
		public static Vector3D F0(Vector3D s)
		{
			Vector3D af = new Vector3D(a.Z,a.Y,-a.X);//ff
			Vector3D sxz = new Vector3D(s.X, 0, s.Z);//fxz
			Vector3D slv = (sxz - p).UnitVector;
			Vector3D slp = (p - slv * p.Dot(slv)); 
			Vector3D su = slp + slv * Math.Sqrt(1 - slp.MagnitudeSquared);//fu
			Vector3D srccw_xy = new Vector3D(-su.Y * sqr34 - su.X / 2, 0, su.Z); //fxy frccw
			Vector3D sow = a * a.Dot(srccw_xy) + af * (af.Dot(srccw_xy) / Paper.s);//fow
			slv = (sow - pow).UnitVector;
			slp = (pow - slv * pow.Dot(slv));
			su = slp + slv * Math.Sqrt(1 - slp.MagnitudeSquared);//fu
			Vector3D siw = a * a.Dot(su) + af * (af.Dot(su) * Paper.s);//fiw
			Vector3D sy = new Vector3D(siw.X,Math.Sqrt(1-siw.X*siw.X-siw.Z*siw.Z),siw.Z);
			return sy;
		}

		public static Vector3D F1(Vector3D s)
		{
			Vector3D q = F0(s);
			return new Vector3D(-q.Y * sqr34 - q.X / 2, q.X * sqr34 - q.Y / 2, q.Z);
		}


		public static Vector3D Ff(Vector3D q) => new Vector3D(q.Z, q.Y, -q.X); 

		public static List<bool> GetNSequence(double g)
		{
			List<bool> sequence = new List<bool>(); 
			if (g > 1 || g < 0)
				throw new Exception("g should be between 0 and 1"); 
			if (g == 1 || g == 0)
				return sequence;
			double x = 0.5;
			
			for (int i = 0; i < 52 && x!=g; i++)
			{
				double x0 = 0.75;
				double x1 = 0.25;

				foreach (bool b in sequence)
				{
					if (b)
					{
						x0 = x0/2;
						x1 = x1/2;
					}
					else
					{
						x0 = 1-x0/2;
						x1 = 1-x1/2;
					}
				}
				if (Math.Abs(x0 - g) < Math.Abs(x1 - g))
				{
					x = x0;
					sequence.Insert(0, false); 
				}
				else
				{
					x = x1;
					sequence.Insert(0, true); 
				}				
			} 

			return sequence;
		}

		/// <summary>
		/// Returns the cut point for a given value between 0 and 1. 
		/// 0.5 strikes through the midpoint m.
		/// 0 strikes through the base point a. 
		/// 1 strikes accross the longest part of the triangle and through the center point. 
		/// </summary>
		/// <param name="g"></param>
		/// <returns></returns>
		public static Vector3D GetCutPoint(double g)
		{
			if (g == 1)
				return b;
			if (g == 0)
				return a;

			List<bool> sequence = GetNSequence(g);
			
			Vector3D result = m;
			
			foreach (bool b in sequence)
				result = b ? F1(result): F0(result);
				
			return result; 
		}

	}
}
