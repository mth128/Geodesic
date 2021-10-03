using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
	public class Maximum
	{
		public double max = double.NegativeInfinity;

		public void Add(double value)
		{
			if (value > max)
				max = value; 
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
			return max.ToString(); 
		}
	}
}
