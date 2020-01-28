using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computable
{
  public struct Integer : IValue
  {
    public double Value => Int; 
    private static Dictionary<long, List<long>> factorStorage = new Dictionary<long, List<long>>(); 
    public long Int { get; }
    public List<long> Factors
    {
      get
      {
        if (factorStorage.TryGetValue(Int, out List<long> factors))
          return factors;
        return factorStorage[Int] = Prime.Factorize(Int);
      }
    }

    public bool Negative => Int<0;

    public bool Integerable => true;

    public bool Fractionable => true;

    public bool Radicalable => true;

    public string Equation => Int.ToString(); 

    public string Type => "Integer";

    public int RadicalDepth => 0; 

    public Integer (long value)
    {
      Int = value; 
    }

    public Integer (List<long> factors)
    {
      Int = 1;
      foreach (long factor in factors)
        Int *= factor; 
    }

    public static Integer operator +(Integer a, Integer b) => new Integer(a.Int + b.Int);
    public static Integer operator -(Integer a, Integer b) => new Integer(a.Int - b.Int);
    public static Integer operator +(Integer a, long b) => new Integer(a.Int + b);
    public static Integer operator -(Integer a, long b) => new Integer(a.Int - b);
    public static Integer operator -(Integer a) => new Integer(-a.Int);
    public static Integer operator *(Integer a, Integer b) => new Integer(a.Int * b.Int);
    public static Integer operator *(Integer a, long b) => new Integer(a.Int * b);
    public static IValue operator /(Integer a, long b) => a / new Integer(b);
    public static IValue operator /(Integer a, Integer b)
    {
      if (b.Int == 0)
        throw new DivideByZeroException("Integer devision by zero.");
      if (a % b == 0)
        return new Integer(a.Int / b.Int);
      return new Fraction(a, b); 
    }

    public static long operator %(Integer a, Integer b) => a.Int % b.Int; 
    public static long operator %(Integer a, long b) => a.Int % b;
    public static bool operator ==(Integer a, Integer b) => a.Int == b.Int; 
    public static bool operator ==(Integer a, long b) => a.Int == b;
    public static bool operator !=(Integer a, Integer b) => a.Int != b.Int;
    public static bool operator !=(Integer a, long b) => a.Int != b;
    public static bool operator <(Integer a, Integer b) => a.Int < b.Int;
    public static bool operator >(Integer a, Integer b) => a.Int > b.Int;
    public static bool operator <=(Integer a, Integer b) => a.Int <= b.Int;
    public static bool operator >=(Integer a, Integer b) => a.Int >= b.Int;
    public static bool operator <(Integer a, long b) => a.Int < b;
    public static bool operator >(Integer a, long b) => a.Int > b;
    public static bool operator <=(Integer a, long b) => a.Int <= b;
    public static bool operator >=(Integer a, long b) => a.Int >= b;
    public IValue Negate() => -this;
    public IValue Simple() => this;

    public List<long> GetDuplicateFactors()
    {
      List<long> factors = Factors;
      List<long> result = new List<long>();

      int i = 0;
      while (i+1<factors.Count)
      {
        if (factors[i] == factors[i + 1])
        {
          result.Add(factors[i]);
          i += 2;
        }
        else
        {
          i++; 
        }
      }
      return result; 
    }
    public List<long> GetNonDuplicateFactors()
    {
      List<long> factors = Factors;
      List<long> result = new List<long>();

      int i = 0;
      while (i + 1 < factors.Count)
      {
        if (factors[i] == factors[i + 1])
        {
          i += 2;
        }
        else
        {
          result.Add(factors[i]); 
          i++;
        }
      }
      if (i < factors.Count)
        result.Add(factors[i]); 
      return result;
    }

    public Fraction ToFraction()
    {
      return new Fraction(this, new Integer(1));
    }

    public Radical ToRadical()
    {
      return new Radical(new Integer(1), this); 
    }

    public Integer ToInteger()
    {
      return this;
    }
    
    public IValue Squared()
    {
      return new Integer(Int * Int); 
    }

    public override string ToString()
    {
      return "[" + Type + "]" + Int.ToString(); 
    }
  }
}
