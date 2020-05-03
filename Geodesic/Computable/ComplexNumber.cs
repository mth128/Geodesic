using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computable
{
  [Serializable]
  public class ComplexNumber : IValue
  {
    private IValue source; 
    public bool Negative { get; }

    public double Value => double.NaN;

    public bool Integerable => false;

    public bool Fractionable => false;

    public bool Radicalable => false;

    public string Equation => "Sqrt("+source.Equation+")";

    public string Type => "Complex Number";

    public int RadicalDepth => source.RadicalDepth + 1;

    public Integer IntegerComponent => new Integer(1);

    public Integer DivisorIntegerComponent => new Integer(1);

    public int Complexity => source.Complexity + 2;

    public ComplexNumber(IValue valueToSqrt, bool negative = false)
    {
      source = valueToSqrt.Direct();
      Negative = negative; 
    }

    public IValue Negate()
    {
      return new ComplexNumber(source, !Negative);  
    }

    public IValue Simple()
    {
      return this; 
    }

    public IValue Squared()
    {
      if (Negative)
        return source.Negate();
      return source; 
    }

    public Fraction ToFraction()
    {
      throw new NotImplementedException();
    }

    public Integer ToInteger()
    {
      throw new NotImplementedException();
    }

    public Radical ToRadical()
    {
      throw new NotImplementedException();
    }

    public IValue ReduceIntegerComponent(Integer sharedComponent)
    {
      throw new NotImplementedException();
    }

    public IValue ReduceDivisorIntegerComponent(Integer sharedComponent)
    {
      throw new NotImplementedException();
    }
  }
}
