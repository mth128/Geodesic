using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computable
{
  public interface IValue
  {
    bool Negative { get; }
    double Value { get; }
    bool Integerable { get; }
    bool Fractionable { get; }
    bool Radicalable { get; }
    string Equation { get; }
    string Type { get; }

    int RadicalDepth { get; }

    IValue Negate();
    IValue Simple();

    Fraction ToFraction();
    Radical ToRadical();
    Integer ToInteger();
    IValue Squared();


  }

  public static class IValueExtension
  {
    public static IValue Direct(this IValue a)
    {
      if (a is Equation equation)
        return equation.Simple();
      return a; 
    }
    public static bool Is(this IValue a, IValue b)
    {
      a = a.Simple();
      b = b.Simple(); 
      if (a.Integerable && b.Integerable)
        return a.ToInteger() == b.ToInteger();

      if (a.Fractionable && b.Fractionable)
      {
        Fraction fa = a.ToFraction();
        Fraction fb = b.ToFraction();
        return fa.Numerator.Is(fb.Numerator) && fa.Denominator.Is(fb.Denominator);
      }

      if (a.Radicalable && b.Radicalable)
      {
        Radical ra = a.ToRadical();
        Radical rb = b.ToRadical();
        return ra.Radicant.Is(rb.Radicant) && ra.Coefficient.Is(rb.Coefficient); 
      }

      return a.Value == b.Value; 
    }
  }
}
