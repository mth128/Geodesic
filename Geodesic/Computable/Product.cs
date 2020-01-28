using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computable
{
  public class Product: IValue
  {
    private double value = double.NaN;
    public IValue First { get; }
    public IValue Second { get; }

    public bool Negative => First.Negative ^ Second.Negative; 

    public double Value => double.IsNaN(value) ? value = First.Value * Second.Value : value;

    public bool Integerable => First.Integerable && Second.Integerable;

    public bool Fractionable => Second.Integerable && Second.ToInteger() == 1 && First.Fractionable ||
      (First.Integerable && First.ToInteger() == 1 && Second.Fractionable);

    public bool Radicalable => (Second.Integerable && Second.ToInteger() == 1 && First.Radicalable) || 
      (First.Integerable && First.ToInteger() == 1 && Second.Radicalable);

    public string Equation => First.Equation + "*" + Second.Equation; 

    public string Type => "Product";

    public int RadicalDepth
    {
      get
      {
        int depth1 = First.RadicalDepth;
        int depth2 = Second.RadicalDepth;
        return depth1 > depth2 ? depth1 : depth2; 
      }
    }

    public Product(IValue first, IValue second)
    {
      first = first.Direct();
      second = second.Direct(); 

      if (first.Integerable && second.Integerable)
      {
        First = first.ToInteger() * second.ToInteger();
        Second = new Integer(1);
        return; 
      }
      if (first.Fractionable && second.Fractionable)
      {
        Fraction a = first.ToFraction();
        Fraction b = second.ToFraction();
        First = new Fraction(new Product(a.Numerator, b.Numerator).Simple(), new Product(a.Denominator,b.Denominator).Simple());
        Second = new Integer(1);
        return;
      }
      if (first.Radicalable && second.Radicalable)
      {
        Radical a = first.ToRadical();
        Radical b = second.ToRadical(); 
        
        First = new Radical(new Product(a.Radicant, b.Radicant).Simple(), new Product(a.Coefficient, b.Coefficient).Simple(),true);
        Second = new Integer(1);
        return;
      }
      if (First is Sum firstSum)
      {
        First = new Sum(new Product(firstSum.First, Second).Simple(), new Product(firstSum.Second, Second));
        Second = new Integer(1);
        return; 
      }
      if (Second is Sum secondSum)
      {
        First = new Sum(new Product(secondSum.First, First).Simple(), new Product(secondSum.Second, First));
        Second = new Integer(1);
        return;
      }

      First = first;
      Second = second;
    }

    public IValue Negate()
    {
      return new Product(First, Second.Negate());
    }

    public IValue Simple()
    {
      if (Second.Integerable && Second.ToInteger() == 1)
        return First.Simple();
      if (First.Integerable && First.ToInteger() == 1)
        return Second.Simple(); 

      if (Integerable)
        return ToInteger();
      if (Fractionable)
        return ToFraction();
      if (Radicalable)
        return ToRadical();
      if (First is Product firstProduct)
      {
        IValue simple = TrySimplify(firstProduct,Second);
        if (simple != null)
          return simple; 
      }
      if (Second is Product secondProduct)
      {
        IValue simple = TrySimplify(secondProduct, First);
        if (simple != null)
          return simple;
      }

      if (First is Sum firstSum)
        return new Sum(new Product(firstSum.First, Second).Simple(), new Product(firstSum.Second, Second).Simple()).Simple();

      if (Second is Sum secondSum)
        return new Sum(new Product(secondSum.First, First).Simple(), new Product(secondSum.Second, First).Simple()).Simple();


      return this; 
    }

    private IValue TrySimplify(Product product, IValue other)
    {
      IValue a = new Product(product.First, other).Simple();
      if (!(a is Product))
        return new Product(a, product.Second).Simple();
      IValue b = new Product(product.Second, other).Simple();
      if (!(b is Product))
        return new Product(b, product.First).Simple(); 
      return null; 
    }

    public Fraction ToFraction()
    {
      IValue simple = Simple();
      if (simple is Fraction fraction)
        return fraction;
      
      return new Fraction(simple, new Integer(1)); 
    }

    public Radical ToRadical()
    {
      IValue simple = Simple();
      if (simple is Radical radical)
        return radical;

      return new Radical(new Integer(1), simple);
    }

    public Integer ToInteger()
    {
      return First.ToInteger() * Second.ToInteger();
    }

    public IValue Squared()
    {
      return new Product(First.Squared(), Second.Squared()).Simple(); 
    }

    public override string ToString() => "[" + Type + "] " + Equation + "=" + Value.ToString();  

  }
}
