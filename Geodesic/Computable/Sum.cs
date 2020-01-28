using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computable
{
  public class Sum : IValue
  {
    private double value = double.NaN;
    private bool firstItteration;

    public IValue First { get; private set; }
    public IValue Second { get; private set; }

    private bool SecondIsZero => Second is Integer integer && integer == 0;

    public bool Negative => value<0;

    public double Value => double.IsNaN(value) ? value = First.Value+Second.Value : value;

    public bool Integerable => SecondIsZero && First.Integerable; 

    public bool Fractionable => SecondIsZero && First.Fractionable;

    public bool Radicalable => SecondIsZero && Second.Radicalable;

    public string Equation => "(" + First.Equation + "+" + Second.Equation + ")";

    public string Type => "Sum";

    public int RadicalDepth
    {
      get
      {
        int depth1 = First.RadicalDepth;
        int depth2 = Second.RadicalDepth;
        return depth1 > depth2 ? depth1 : depth2;
      }
    }


    public Sum(IValue first, IValue second)
    {
      Set(first, second); 
    }

    private void Set(IValue first, IValue second, bool firstItteration = true)
    {
      this.firstItteration = firstItteration; 
      first = first.Direct();
      second = second.Direct();

      if (first.Integerable && second.Integerable)
      {
        First = first.ToInteger() + second.ToInteger();
        Second = new Integer(0);
        return;
      }
      if (first.Fractionable && second.Fractionable)
      {
        Fraction fraction1 = first.ToFraction();
        Fraction fraction2 = second.ToFraction();
        if (fraction1.Numerator.Integerable && fraction1.Denominator.Integerable && fraction2.Numerator.Integerable && fraction2.Denominator.Integerable)
        {
          Integer numerator1 = fraction1.Numerator.ToInteger();
          Integer numerator2 = fraction2.Numerator.ToInteger();
          Integer denominator1 = fraction1.Denominator.ToInteger();
          Integer denominator2 = fraction2.Denominator.ToInteger();

          First = new Fraction(numerator1 * denominator2 + numerator2 * denominator1, denominator1 * denominator2);
          Second = new Integer(0);
          return;
        }
      }
      if (first.Radicalable && second.Radicalable && firstItteration)
      {
        Radical radical1 = first.ToRadical();
        Radical radical2 = second.ToRadical();
        if (radical1.Radicant.Is(radical2.Radicant))
        {
          Sum coeffecient = new Sum(new Integer(0), new Integer(0));
          coeffecient.Set(radical1.Coefficient, radical2.Coefficient, false); 
          First = new Radical(radical1.Radicant, coeffecient.Simple());
          Second = new Integer(0);
          return;
        }
      }

      List<IValue> sumComponents = GetSumComponents(first);
      sumComponents.AddRange(GetSumComponents(second));

      if (firstItteration)
      {
        List<IValue> eliminated = TryEliminateComponents(sumComponents, firstItteration);
        if (eliminated.Count == sumComponents.Count)
        {
          First = first;
          Second = second;
          return;
        }

        if (eliminated.Count == 0)
        {
          First = new Integer(0);
          Second = new Integer(0);
          return;
        }

        First = eliminated[0];
        if (eliminated.Count == 1)
        {
          Second = new Integer(0);
          return;
        }
        if (eliminated.Count == 2)
        {
          Second = eliminated[1];
          return;
        }
        eliminated.RemoveAt(0);
        Second = new Sum(eliminated);
      }
      else
      {
        First = first;
        Second = second; 
      }
    }

    public Sum(List<IValue> values)
    {
      if (values.Count < 2)
        throw new Exception("Too few values for sum.");

      First = values[0];

      if (values.Count == 2)
      {
        Second = values[1];
        return;
      }

      values.RemoveAt(0);
      Second = new Sum(values);
    }

    private List<IValue> TryEliminateComponents(List<IValue> components, bool firstItteration)
    {
      Integer integer = new Integer(0);
      Fraction fraction = new Fraction(new Integer(0), new Integer (1)); 

      List<Fraction> fractions = new List<Fraction>();
      List<Radical> radicals = new List<Radical>(); 
      List<IValue> result = new List<IValue>();

      foreach (IValue value in components)
      {
        if (value.Integerable)
          integer += value.ToInteger();
        else if (value.Fractionable)
        {
          Fraction valueFraction = value.ToFraction();
          if (valueFraction.IsSimpleFraction)
            fraction += valueFraction;
          else
            fractions.Add(value.ToFraction());
        }
        else if (value.Radicalable)
        {
          Radical radical = value.ToRadical();
          bool added = false; 
          for (int i =0; i<radicals.Count;i++)
          {
            Radical current = radicals[i];
            Sum sum = new Sum(new Integer(0),new Integer(0));
            sum.Set(radical, current, !firstItteration); 
            if (sum.Simple() is Radical combination)
            {
              radicals[i] = combination;
              added = true; 
              break;
            }
          }
          if (!added)
            radicals.Add(radical);
        }
        else
          result.Add(value.Simple()); 
      }

      Fraction totalSimple = fraction + integer.ToFraction();

      result.Add(totalSimple.Simple());

      Fraction totalComplex = new Fraction(new Integer(0), new Integer(1));

      foreach(Fraction complexFraction in fractions)
        totalComplex += complexFraction;

      if (totalComplex.Value != 0)
        result.Add(totalComplex);

      foreach (Radical radical in radicals)
        result.Add(radical);

      return result;
    }

    private List<IValue> GetSumComponents(IValue value)
    {
      List<IValue> components = new List<IValue>();
      if (value is Sum sum)
      {
        components.AddRange(GetSumComponents(sum.First));
        components.AddRange(GetSumComponents(sum.Second));
        return components; 
      }
      components.Add(value);
      return components; 
    }

    public IValue Negate()
    {
      return new Sum(First.Negate(), Second.Negate());
    }

    public IValue Simple()
    {
      if (Second.Integerable)
      {
        Integer integer = Second.ToInteger(); 
        if (integer == 0)
          return First;
        if (First.Integerable)
          return integer + First.ToInteger(); 
      }
      if (First.Integerable && First.ToInteger() == 0)
        return Second;

      return this; 
    }

    public Fraction ToFraction()
    {
      if (!SecondIsZero)
        throw new Exception("Sum is not fractionable!");
      return First.ToFraction(); 
    }

    public Radical ToRadical()
    {
      if (!SecondIsZero)
        throw new Exception("Sum is not radicalable!");
      return First.ToRadical(); 
    }

    public Integer ToInteger()
    {
      if (!SecondIsZero)
        throw new Exception("Sum is not integerable!");
      return First.ToInteger(); 
    }

    public IValue Squared()
    {
      return new Sum(new Sum(First.Squared(), Second.Squared()).Simple(), new Product(new Product(First, Second).Simple(), new Integer(2)).Simple()).Simple();  
    }

    public override string ToString() => "[" + Type + "] " + Equation + "=" + Value.ToString();
  }
}
