using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
	public class Minimum
	{
		public double min = double.PositiveInfinity;
		public void Add(double value)
		{
			if (value < min)
				min = value;
		}

		public void Add(double a, double b)
		{
			Add(a);
			Add(b);
		}
		public void Add(double a, double b, double c)
		{
			Add(a);
			Add(b);
			Add(c);

		}
		public void Add(IEnumerable<double> values)
		{
			foreach (double value in values)
				Add(value);
		}
		public override string ToString()
		{
			return min.ToString("E17");
		}
	}
}
