using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computable
{
  [Serializable]
  public class Equation : IValue
  {
    public bool Negative => Content.Negative;

    public double Value => Content.Value;

    public bool Integerable => Content.Integerable;

    public bool Fractionable => Content.Fractionable;

    public bool Radicalable => Content.Radicalable;

    string IValue.Equation => "["+Type+"] "+ Value.ToString() + "=" + Content.Equation;

    public string Type => Content.Type;

    public int RadicalDepth => Content.RadicalDepth;

    public Integer IntegerComponent => Content.IntegerComponent;

    public IValue Content { get; }

    public Integer DivisorIntegerComponent => Content.DivisorIntegerComponent;

    public int Complexity => Content.Complexity;

    public Equation(IValue value)
    {
      if (value == null)
        Content = new Integer(0);

      Content = value.Direct();
      if (Content.Complexity >5 && SimplifyForm.Instances < 1)
      {
        Content = Geodesic.Computable.CustomSimplify.CustomSimplifyStorage.CustomSimplify(this); 
      }
    }

    public Equation(long value)
    {
      Content = new Integer(value); 
    }
           




    public static Equation operator +(Equation a, long b)
    {
      return a + new Integer(b);
    }


    public static Equation operator+(Equation a, IValue b)
    {
      if (a.Integerable && b.Integerable)
        return new Equation(a.ToInteger() + b.ToInteger());
      if (a.Fractionable && b.Fractionable)
        return new Equation(a.ToFraction() + b.ToFraction());
      if (a.Radicalable && b.Radicalable)
        return new Equation(a.ToRadical() + b.ToRadical());
      return new Equation(new Sum(a.Simple(), b.Simple()));
    }

    public static Equation operator -(Equation a, long b)
    {
      return a + new Integer(-b);
    }

    public static Equation operator-(Equation a)
    {
      return new Equation(a.Negate()); 
    }

    public static Equation operator-(Equation a, IValue b)
    {
      return a + b.Negate(); 
    }

    public static Equation operator *(Equation a, long b)
    {
      return a * new Integer(b);
    }
    public static Equation operator +(long a, Equation b)
    {
      return new Equation(a) + b;
    }
    public static Equation operator -(long a, Equation b)
    {
      return new Equation(a) - b;
    }
    public static Equation operator *(long a, Equation b)
    {
      return new Equation(a) * b;
    }

    public static Equation operator /(long a, Equation b)
    {
      return new Equation(a) / b;
    }

    public static Equation operator*(Equation a, IValue b)
    {
      if (a.Integerable && b.Integerable)
        return new Equation(a.ToInteger() * b.ToInteger());
      if (a.Fractionable && b.Fractionable)
        return new Equation(a.ToFraction() * b.ToFraction());
      if (a.Radicalable && b.Radicalable)
        return new Equation(a.ToRadical() * b.ToRadical());
      return new Equation(new Product(a.Simple(), b.Simple()));
    }

    public static Equation operator /(Equation a, long b)
    {
      return new Equation(new Fraction(a, new Integer(b)).Simple());
    }

    public static Equation operator /(Equation a, IValue b)
    {
      return new Equation(new Fraction(a, b).Simple());
    }

    public static double operator *(Equation a, double b)
    {
      return a.Value * b;
    }

    public static double operator /(Equation a, double b)
    {
      return a.Value / b;
    }


    public static double operator +(Equation a, double b)
    {
      return a.Value + b;
    }

    public static double operator -(Equation a, double b)
    {
      return a.Value - b;
    }

    public static bool operator >(Equation a, IValue b)
    {
      return a.Value > b.Value;
    }

    public static bool operator <(Equation a, IValue b)
    {
      return a.Value < b.Value;
    }

    public static bool operator >=(Equation a, IValue b)
    {
      return a.Value >= b.Value;
    }

    public static bool operator <=(Equation a, IValue b)
    {
      return a.Value <= b.Value;
    }


    public static bool operator >(Equation a, double b)
    {
      return a.Value > b;
    }

    public static bool operator <(Equation a, double b)
    {
      return a.Value < b;
    }

    public static bool operator >=(Equation a, double b)
    {
      return a.Value >= b;
    }

    public static bool operator <=(Equation a, double b)
    {
      return a.Value <= b;
    }

    public static bool operator ==(Equation a, double b)
    {
      return a.Value == b;
    }

    public static bool operator !=(Equation a, double b)
    {
      return a.Value != b;
    }

    public IValue Negate()
    {
      return Content.Negate(); 
    }

    public IValue Simple()
    {
      return Content.Simple(); 
    }

    public Fraction ToFraction()
    {
      return Content.ToFraction();
    }

    public Integer ToInteger()
    {
      return Content.ToInteger(); 
    }

    public Radical ToRadical()
    {
      return Content.ToRadical();
    }

    public IValue Squared()
    {
      return Content.Squared(); 
    }

    public override string ToString()
    {
      return (this as IValue).Equation; 
    }

    public IValue ReduceIntegerComponent(Integer sharedComponent)
    {
      return Content.ReduceIntegerComponent(sharedComponent); 
    }

    public IValue ReduceDivisorIntegerComponent(Integer sharedComponent)
    {
      return Content.ReduceDivisorIntegerComponent(sharedComponent);
    }

    public bool EqualsClosely(Equation other)
    {
      return Math.Abs(Value - other.Value) < 1e-11;
    }



  }
}
