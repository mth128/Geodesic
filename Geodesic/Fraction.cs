using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class Fraction
  {
    private long numerator = 0;
    private long denominator = 1;

    public double Value => ((double)numerator) / denominator;
    public long Numerator => numerator;
    public long Denominator => denominator;
    public long Remainder => numerator % denominator;
    public long IntValue => numerator / denominator;
    public Fraction Squared => this * this;

    public Fraction(long numerator = 0, long denominator = 1)
    {
      this.numerator = numerator;
      this.denominator = denominator; 
    }

    public static Fraction operator+(Fraction a, Fraction b)
    {
      Fraction variable = new Fraction();
      variable.denominator = a.denominator * b.denominator;
      variable.numerator = a.numerator * b.denominator + b.numerator * a.denominator;
      variable.Simplify();
      return variable; 
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
      Fraction variable = new Fraction();
      variable.denominator = a.denominator * b.denominator;
      variable.numerator = a.numerator * b.denominator - b.numerator * a.denominator;
      variable.Simplify(); 
      return variable;
    }

    public static Fraction operator *(Fraction a, Fraction b)
    {
      Fraction variable = new Fraction();
      variable.denominator = a.denominator * b.denominator;
      variable.numerator = a.numerator*b.numerator;
      variable.Simplify();
      return variable;
    }

    public static Fraction operator /(Fraction a, Fraction b)
    {
      Fraction variable = new Fraction();
      variable.denominator = a.denominator * b.numerator;
      variable.numerator = a.numerator * b.denominator;
      variable.Simplify();
      return variable;
    }

    private void Simplify()
    {
      if (denominator == 0)
        throw new DivideByZeroException(); 

      if (numerator == 0)
      {
        denominator = 1;
        return; 
      }
      if (numerator == denominator)
      {
        numerator = denominator = 1;
        return;
      }
      if (numerator%denominator == 0)
      {
        numerator /= denominator;
        denominator = 1;
        return; 
      }
      if (denominator<0)
      {
        denominator = -denominator;
        numerator = -numerator; 
      }

      long smallest = Math.Abs(denominator) < Math.Abs(numerator) ? denominator : numerator;
      if (smallest < 0)
        smallest = -smallest;

      int sqrtSmallest = (int)Math.Sqrt(smallest)+1;

      foreach (int prime in Prime.PrimesBelow(sqrtSmallest))
      {
        while (numerator%prime == 0 && denominator%prime ==0)
        {
          numerator /= prime;
          denominator /= prime;
        }
        if (numerator <= prime || denominator <= prime)
          break; 
      }
    }
    public override string ToString()
    {
      return numerator.ToString() + "/" + denominator.ToString() + "="+ Value.ToString(); 
    }
  }
}
