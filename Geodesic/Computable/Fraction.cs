using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computable
{
  [Serializable]

  public class Fraction :IValue
  {
    private double value = double.NaN; 
    public IValue Numerator { get; private set; }
    public IValue Denominator { get; private set; }

    public bool Negative => Numerator.Negative ^ Denominator.Negative;
    public double Value => double.IsNaN(value) ? value = Numerator.Value/Denominator.Value : value;

    public bool Integerable => Denominator is Integer integer && integer == 1 && Numerator.Simple() is Integer;

    public bool Fractionable => true; 

    public bool Radicalable => true;

    public bool IsSimpleFraction => Numerator is Integer && Denominator is Integer;

    public string Equation
    {
      get
      {
        string numerator = Numerator is Integer ? Numerator.Equation : "(" + Numerator.Equation + ")";
        string denominator = Denominator is Integer ? Denominator.Equation : "(" + Denominator.Equation + ")";
        return numerator + "/" + denominator; 
      }
    }

    public string Type => "Fraction";


    public int RadicalDepth
    {
      get
      {
        int depth1 = Numerator.RadicalDepth;
        int depth2 = Denominator.RadicalDepth;
        return depth1 > depth2 ? depth1 : depth2;
      }
    }

    public Integer IntegerComponent => Numerator.IntegerComponent;
    public Integer DivisorIntegerComponent => Denominator.IntegerComponent;

    public int Complexity => Numerator.Complexity+Denominator.Complexity + 2;

    public Fraction(long numerator, long denominator): this (new Integer(numerator), new Integer(denominator))
    {
    }

    public Fraction(IValue numerator, long denominator) : this(numerator, new Integer(denominator))
    {
    }

    public Fraction(long numerator, IValue denominator) : this(new Integer(numerator), denominator)
    {
    }

    public Fraction(IValue numerator, IValue denominator)
    {
      numerator = numerator.Direct().Simple();
      denominator = denominator.Direct().Simple(); 
      if (numerator == null)
        numerator = new Integer(0); 
      if (denominator == null)
        denominator = new Integer(1);
      Numerator = numerator;
      Denominator = denominator;
      Simplify();

    }

    private void Simplify()
    {
      if (Numerator.Value == 0)
      {
        Numerator = new Integer(0);
        Denominator = new Integer(1); 
      }

      if (Denominator.Negative)
      {
        Denominator = Denominator.Negate();
        Numerator = Numerator.Negate(); 
      }



      if (Numerator.Integerable && Denominator.Integerable)
      {
        Integer numerator = Numerator.ToInteger();
        Integer denominator = Denominator.ToInteger(); 

        if (denominator == 0)
          throw new Exception("Fraction devision by zero.");
        if (numerator == 0)
        {
          Denominator = new Integer(1);
          return; 
        }
        if (numerator % denominator == 0)
        {
          Numerator = numerator / denominator;
          Denominator = new Integer(1); 
          return; 
        }
        if (denominator % numerator == 0 && numerator!=-1)
        {
          Denominator = denominator / numerator;
          Numerator = new Integer(1);
          return; 
        }

        List<long> dFactors = denominator.Factors;
        List<long> nFactors = numerator.Factors;
        int d = 0;
        int n = 0;
        long newD = 1;
        long newN = numerator < 0 ? -1 : 1;
        long currentD;
        long currentN; 
        while (d<dFactors.Count && n<nFactors.Count)
        {
          while (d < dFactors.Count && (currentD = dFactors[d]) < nFactors[n])
          {
            newD *= currentD;
            d++;
          }
          if (d >= dFactors.Count)
            break; 
          while (n < nFactors.Count && (currentN = nFactors[n]) < dFactors[d])
          {
            newN *= currentN;
            n++;
          }
          if (n >= nFactors.Count)
            break; 
          while (n < nFactors.Count && d < dFactors.Count && nFactors[n] == dFactors[d])
          {
            n++;
            d++;
          }
        }

        while (d < dFactors.Count)
        {
          newD *= dFactors[d];
          d++;
        }

        while (n < nFactors.Count)
        {
          newN *= nFactors[n];
          n++;
        }

        Numerator = new Integer(newN);
        Denominator = new Integer(newD);
        return; 
      }

      if (Numerator is Radical radicalNumerator && radicalNumerator.Coefficient.Fractionable && Denominator.Integerable)
      {
        Fraction numeratorCoefficient = radicalNumerator.Coefficient.ToFraction();
        if (numeratorCoefficient.IsSimpleFraction)
        {
          Fraction sharedFraction = numeratorCoefficient * new Fraction(new Integer(1), Denominator.ToInteger());
          {
            Numerator = new Radical(radicalNumerator.Radicant, sharedFraction.Numerator);
            Denominator = sharedFraction.Denominator;
            return; 
          }
        }
      }

      if (Denominator is Sum sum)
      {
        if (sum.First is Radical ||sum.Second is Radical)
        {
          Sum newNumerator = new Sum(new Product(Numerator, sum.First).Simple(), new Product(Numerator, sum.Second).Simple());
          Sum newDenominator = new Sum(sum.First.Squared().Simple(), sum.Second.Squared().Simple());
          //extra check, since this seems to go wrong sometimes. 
          double value1 = Numerator.Value / Denominator.Value;
          double value2 = newNumerator.Value / newDenominator.Value;
          if (Math.Abs(value1 - value2) < 1e-11)
          {
            Numerator = newNumerator.Simple();
            Denominator = newDenominator.Simple();
          }
        }
      }
    }


    public IValue Negate()
    {
      if (Denominator.Negative)
        return new Fraction(Numerator, Denominator.Negate());
      return new Fraction(Numerator.Negate(), Denominator);
    }

    public IValue Simple()
    {
      if (Denominator.Negative)
        return new Fraction(Numerator.Negate(), Denominator.Negate()).Simple(); 
      
      if (Denominator is Integer denominator && denominator == 1)
        return Numerator;

      if (Denominator is Fraction denominatorFraction)
      {
        return new Fraction(new Product(Numerator, denominatorFraction.Denominator).Simple(), denominatorFraction.Numerator).Simple(); 
      }

      if (Numerator is Fraction numeratorFraction)
      {
        return new Fraction(numeratorFraction.Numerator, new Product(numeratorFraction.Denominator, Denominator).Simple()).Simple(); 
      }

      Integer sharedComponent = Denominator.IntegerComponent.CommonFactors(Numerator.IntegerComponent);
      if (sharedComponent!=1)
      {
        Numerator = Numerator.ReduceIntegerComponent(sharedComponent);
        Denominator = Denominator.ReduceIntegerComponent(sharedComponent); 
      }

      return this; 
    }

    public Fraction ToFraction()
    {
      return this; 
    }

    public Radical ToRadical()
    {
      return new Radical(new Integer(1), this); 
    }

    public Integer ToInteger()
    {
      if (Denominator is Integer integer && integer == 1 && Numerator.Simple() is Integer numerator)
        return numerator;
      throw new Exception("This fraction is not integrable!");
    }

    public static Fraction operator+(Fraction a, Fraction b)
    {
      if (a.IsSimpleFraction && b.IsSimpleFraction)
      {
        Integer aNumerator = a.Numerator.ToInteger();
        Integer aDenominator = a.Denominator.ToInteger();
        Integer bNumerator = b.Numerator.ToInteger();
        Integer bDenominator = b.Denominator.ToInteger();
        return new Fraction(aNumerator * bDenominator + bNumerator * aDenominator, aDenominator * bDenominator);
      }
      return new Fraction(new Sum(new Product(a.Numerator, b.Denominator), new Product(b.Numerator, a.Denominator)).Simple(), new Product(a.Denominator, b.Denominator));
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
      return a + -b; 
    }

    public static Fraction operator -(Fraction a)
    {
      return a.Negate() as Fraction;
    }

    public static Fraction operator *(Fraction a, Fraction b)
    {
      if (a.IsSimpleFraction && b.IsSimpleFraction)
      {
        Integer aNumerator = a.Numerator.ToInteger();
        Integer aDenominator = a.Denominator.ToInteger();
        Integer bNumerator = b.Numerator.ToInteger();
        Integer bDenominator = b.Denominator.ToInteger();
        return new Fraction(aNumerator * bNumerator, aDenominator * bDenominator);
      }
      return new Fraction(new Product(a.Numerator,b.Numerator), new Product(a.Denominator, b.Denominator));
    }
    public static Fraction operator /(Fraction a, Fraction b)
    {
      return a * new Fraction(b.Denominator, b.Numerator); 
    }

    public IValue Squared()
    {
      return new Fraction(Numerator.Squared(), Denominator.Squared()).Simple(); 
    }
    
    public override string ToString() => "[" + Type + "] " + Equation + "=" + Value.ToString();

    public IValue ReduceIntegerComponent(Integer sharedComponent)
    {
      return new Fraction(Numerator.ReduceIntegerComponent(sharedComponent), Denominator); 
    }

    public IValue ReduceDivisorIntegerComponent(Integer sharedComponent)
    {
      return new Fraction(Numerator, Denominator.ReduceIntegerComponent(sharedComponent));
    }
  }
}
