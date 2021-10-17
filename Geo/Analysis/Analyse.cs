using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Analysis
{
	public class Analyze
	{
		public Maximum Maximum { get; } = new Maximum();
		public Minimum Minimum { get; } = new Minimum();
		public Average Average { get; } = new Average();


		public double Deviation
		{
			get
			{
				double dpos = Maximum.max / Average.Avg - 1;
				double dmin = 1 - Minimum.min / Average.Avg;
				if (dpos > dmin)
					return dpos;
				return dmin;
			}
		}

		public void Add(double value)
		{
			Maximum.Add(value);
			Minimum.Add(value);
			Average.Add(value);
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

		public string ToCSVString()
    {
			return Maximum.ToString() + ";" + Minimum.ToString() + ";" + Average.ToString() + ";"; 
    }

		public override string ToString()
		{
			return "Max:" + Maximum.ToString() + ", Min:" + Minimum.ToString() + ", Average:" + Average.ToString() + ", Deviation:" + (Deviation * 100).ToString() + "%";
		}

	}
}
