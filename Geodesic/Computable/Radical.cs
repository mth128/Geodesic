﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computable
{
  [Serializable]
  public class Radical: IValue
  {
    private bool allowDenesting = true; 
    private Integer integerComponent;
    private Integer divisorIntegerComponent;
    private double value = double.NaN;
    public IValue Coefficient; 
    public IValue Radicant;

    public double Value => double.IsNaN(value)? value = Math.Sqrt(Radicant.Value) * Coefficient.Value : value;

    public bool Negative => Coefficient.Negative;

    public bool Integerable => Radicant.Simple() is Integer radicant && radicant == 1 && Coefficient.Integerable;

    public bool Fractionable => Radicant.Simple() is Integer radicant && radicant == 1 && Coefficient.Fractionable;

    public bool Radicalable => true;

    public string Equation => (Coefficient is Integer coeffecient && coeffecient == 1 ? "":Coefficient.Equation+"*") + "Sqrt(" + Radicant.Equation + ")";

    public string Type => "Radical";

    public bool IsSimpleRadical => Radicant.Integerable && Coefficient.Fractionable && Coefficient.ToFraction().IsSimpleFraction;

    public int RadicalDepth
    {
      get
      {
        int depth1 = Coefficient.RadicalDepth;
        int depth2 = Radicant.RadicalDepth + 1;
        return depth1 > depth2 ? depth1 : depth2; 
      }
    }

    public Integer IntegerComponent => (integerComponent == 0)? 
      (integerComponent = Coefficient.IntegerComponent * Radicant.IntegerComponent.PerfectSquareWidth):integerComponent;

    public Integer DivisorIntegerComponent => (divisorIntegerComponent==0)?
      (divisorIntegerComponent = Coefficient.DivisorIntegerComponent * Radicant.DivisorIntegerComponent.PerfectSquareWidth):divisorIntegerComponent;

    public int Complexity => Coefficient.Complexity + Radicant.Complexity + 2;

    internal Radical(IValue radicant, bool allowDenesting):this(radicant)
    {
      allowDenesting = false; 
    }
    public Radical(IValue radicant)
    {
      radicant = radicant.Direct(); 
      if (radicant is Integer integer)
      {
        if (integer < 0)
          throw new ArgumentOutOfRangeException("Cannot calculate radical for negative integer.");
        Radicant = new Integer(integer.GetNonDuplicateFactors());
        Coefficient = new Integer(integer.GetDuplicateFactors());
        return; 
      }
      if (radicant is Fraction fraction)
      {
        if (fraction.Numerator is Integer numerator && fraction.Denominator is Integer denominator)
        {
          Integer radicantNumerator = new Integer(numerator.GetNonDuplicateFactors());
          Integer radicantDenominator = new Integer(denominator.GetNonDuplicateFactors());
          Integer coefficientNumerator = new Integer(numerator.GetDuplicateFactors());
          Integer coefficientDenominator = new Integer(denominator.GetDuplicateFactors());
          
          //simplifying
          radicantNumerator *= radicantDenominator;
          coefficientDenominator *= radicantDenominator;
          radicantDenominator = new Integer(1);

          Radicant = new Fraction(radicantNumerator, radicantDenominator);
          Coefficient = new Fraction(coefficientNumerator, coefficientDenominator);
          return; 
        }
        else
        {          
          Coefficient = new Fraction(new Integer(1),fraction.Denominator).Simple();
          Radicant = new Product(fraction.Numerator, fraction.Denominator).Simple();
          Simplify();
          return; 
        }
      }
      if (radicant is Radical radical)
      {
        Radical coefficientRadical = new Radical(radical.Coefficient);
        Coefficient = coefficientRadical.Coefficient;
        Radicant = new Radical(radical.Radicant, coefficientRadical.Radicant, true);
        return;
      }

      Coefficient = new Integer(1); 
      Radicant = radicant;
    }

    public Radical(IValue radicant, IValue coefficient, bool simplify = false)
    {
      Radicant = radicant.Direct();
      Coefficient = coefficient.Direct();
      if (simplify)
        Simplify();
    }

    public static Radical operator*(Radical a, Radical b)
    {
      return new Radical(new Product(a.Radicant, b.Radicant).Simple(), new Product(a.Coefficient, b.Coefficient).Simple(), true); 
    }

    public static Radical operator /(Radical a, Radical b)
    {
      return new Radical(
        new Product(a.Radicant, b.Radicant).Simple(), 
        new Fraction(a.Coefficient, new Product(b.Coefficient, b.Radicant).Simple()).Simple(), 
        true);
    }
    
    public static IValue operator +(Radical a, Radical b)
    {
      if (a.SameRadicant(b))
        return new Radical(a.Radicant,new Sum(a.Coefficient,b.Coefficient).Simple());

      return new Sum(a, b); 
    }

    public static Radical operator -(Radical a)
    {
      return new Radical(a.Radicant, a.Coefficient.Negate()); 
    }

    public static IValue operator -(Radical a, Radical b)
    {
      return a + -b; 
    }

    public bool SameRadicant(Radical b)
    {
      return Radicant.Is(b.Radicant); 
    }



    private void Simplify()
    {
      if (Radicant.Integerable)
      {
        Integer radicant = Radicant.ToInteger();
        if (radicant == 0)
        {
          Coefficient = new Integer(0);
          Radicant = new Integer(1);
          return; 
        }
        Integer multiplyer = new Integer(radicant.GetDuplicateFactors()); 
        if (multiplyer!=1)
        {
          Radicant = new Integer(radicant.GetNonDuplicateFactors());
          Coefficient = new Product(Coefficient, multiplyer).Simple(); 
        }
      }

      if (Coefficient is Radical coefficient)
      {
        Coefficient = coefficient.Coefficient;
        Radicant = new Product(Radicant, coefficient.Radicant).Simple(); 
      }


    }

    public IValue Negate() => -this; 

    public IValue Simple()
    {
      Radicant = Radicant.Simple(); 
      if (Radicant.Integerable)
      {
        Integer i = Radicant.ToInteger(); 
        if (i == 1)
          return Coefficient.Simple(); 
      }

      if (Radicant is Sum sum)
      {
        if (sum.First is Radical || sum.Second is Radical)
        {
          //nested radical. 
          IValue denested = TryDenest(sum);
          if (denested != null)
            return new Product(denested,Coefficient).Simple(); 
        }
      }

      if (Radicant is Product product)
      {
        IValue radical1 = new Radical(product.First, Coefficient).Simple();
        IValue radical2 = new Radical(product.Second).Simple();
        return new Product(radical1, radical2).Simple();
      }
      Coefficient = Coefficient.Simple(); 

      IValue reduced = this.ReduceDivisorAndMultiplyer();
      if (RadicalDepth >=2)
      {
        if (Radicant is Sum radicantSum)
        {
          IValue denested = TryDenest(radicantSum);
          if (denested != null)
            return new Product(denested, Coefficient).Simple();
        }
      }

      return reduced; 
    }

    /// <summary>
    /// Used to simplyfy a radical within a sum, within another radical. 
    /// </summary>
    /// <param name="radicantSum"></param>
    /// <returns></returns>
    private IValue TryDenest(Sum radicantSum)
    {
      if (!allowDenesting)
        return null; 
      try
      {
        //https://www.youtube.com/watch?v=VGl_p6aTIN4&t=35s

        //check if this might be denested.
        if (radicantSum.First.Integerable && radicantSum.Second.Radicalable)
          return TryDenest(radicantSum.First.ToInteger(), radicantSum.Second.ToRadical());

        if (radicantSum.Second.Integerable && radicantSum.First.Radicalable)
          return TryDenest(radicantSum.Second.ToInteger(), radicantSum.First.ToRadical());
      }
      catch
      {
        allowDenesting = false; 
      }

      //failed. 
      return null; 
    }

    private IValue TryDenest(Integer i, Radical r)
    {
      if (!allowDenesting)
        return null; 
      //when radical is sqrt(i+r):
      //sqrt(a^2-2ab+b^2) = sqrt((a-b)^2) = abs(a-b)
      //To get to thate:
      //2ab = -r;
      //a^2+b^2 = i; 

      //ab = -r/2;
      //b = -r/(2a);
      //a^2 + (-r/(2a))^2 = i; 
      //so:
      //a^4/a^2 +((-r/2)^2*a^2/a^2)/a^2 - i *a^2/a^2=
      //(a^4+(-r/2)^2)-i*a^2)/a^2 = 0
      //so:
      //a^4+(-r/2)^2)-i*a^2 = 0

      //t = a^2
      //t^2+(-i)t+(-r/2)^2 = 0.

      //use quadratic formula to calculate t
      //A = 1
      //B = -i
      //C = r^2/4

      //a = sqrt(quadraticformularesult)
      //b = -r/(2a)

      //result = abs(a-b);
      QuadraticFormula f = new QuadraticFormula(new Integer(1), i.Negate(), new Fraction(r.Squared(), new Integer(4)).Simple());

      IValue discriminant = f.Discriminant;
      if (!discriminant.Integerable)
        return null;

      IValue discriminantSqrt = new Radical(discriminant, false); 

      if (!discriminantSqrt.Integerable)
        //simplification not possible. 
        return null;     

      IValue a = MathE.Sqrt(f.Result1);
      IValue b = MathE.Sqrt(f.Result2);

      if (a is ComplexNumber)
      {
        if (b is ComplexNumber)
        {
          return null; 
        }
        a = new Fraction(r, new Product(new Integer(-2), b).Simple()).Simple(); 
      }
      if (b is ComplexNumber)
      {
        b = new Fraction(r, new Product(new Integer(-2), a).Simple()).Simple(); 
      }

      int radicalDepth = RadicalDepth; 
      if (a.RadicalDepth<radicalDepth && b.RadicalDepth<radicalDepth)
        return MathE.Abs(new Sum(a, b.Negate()).Simple());

      //result is not more simple than this. 
      return this; 
    }

    public Fraction ToFraction()
    {
      if (Radicant.Simple() is Integer radicant && radicant == 1)
        return Coefficient.ToFraction();
      throw new Exception("This radical is not fractionable.");
    }

    public Radical ToRadical()
    {
      return this; 
    }

    public Integer ToInteger()
    {      
      if (Radicant.Simple() is Integer radicant && radicant == 1)
        return Coefficient.ToInteger();

      throw new Exception("This radical is not integerable.");
    }

    public IValue Squared()
    {
      return MathE.Abs(new Product(Coefficient.Squared(), Radicant).Simple()).Simple();
    }

    public override string ToString() => "[" + Type + "] " + Equation + "=" + Value.ToString();

    public IValue ReduceIntegerComponent(Integer sharedComponent)
    {
      Integer coefficient = Coefficient.IntegerComponent;
      Integer coefficientPart = coefficient.CommonFactors(sharedComponent);

      Integer remainder = (sharedComponent / coefficientPart).ToInteger();

      if (remainder==1)
        return new Radical(Radicant, Coefficient.ReduceIntegerComponent(sharedComponent));

      return new Radical(Radicant.ReduceIntegerComponent(remainder*remainder), Coefficient.ReduceIntegerComponent(coefficientPart));
    }

    public IValue ReduceDivisorIntegerComponent(Integer sharedComponent)
    {
      Integer coefficient = Coefficient.DivisorIntegerComponent;
      Integer coefficientPart = coefficient.CommonFactors(sharedComponent);

      Integer remainder = (sharedComponent / coefficientPart).ToInteger();

      if (remainder == 1)
        return new Radical(Radicant, Coefficient.ReduceDivisorIntegerComponent(sharedComponent));

      return new Radical(Radicant.ReduceDivisorIntegerComponent(remainder * remainder), Coefficient.ReduceDivisorIntegerComponent(coefficientPart));
    }
  }
}
