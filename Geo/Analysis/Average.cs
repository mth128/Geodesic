using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Analysis
{
	public class Average
	{
		public double total = 0;
		public int amount = 0;

		public double Avg => amount == 0 ? 0 : total / amount;
		public void Add(double value)
		{
			total += value;
			amount++;
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
			return Avg.ToString();
		}

	}
}
