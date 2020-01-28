using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computable
{
  public class MathE
  {
    public static Equation Sqrt(IValue value)
    {
      if (value.Negative)
        return new Equation(new ComplexNumber(value)); 

      return new Equation(new Radical(value).Simple()); 
    }

    public static Equation Squared(IValue value)
    {
      return new Equation(value.Squared()); 
    }

    public static Equation Abs(IValue value)
    {
      if (value.Negative)
        return new Equation(value.Negate());

      return new Equation(value); 
    }

    public static double Sin(IValue value)
    {
      return Math.Sin(value.Value); 
    }

    public static double Cos(IValue value)
    {
      return Math.Cos(value.Value);
    }

    public static double Asin(IValue value)
    {
      return Math.Asin(value.Value);
    }

    public static double Acos(IValue value)
    {
      return Math.Asin(value.Value);
    }
  }
}
