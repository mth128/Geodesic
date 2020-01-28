using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computable
{
  public class QuadraticFormula
  {
    private IValue discriminant = null;
    private IValue discriminantSqrt = null; 
    private IValue result1 = null;
    private IValue result2 = null; 
    public IValue A { get; }
    public IValue B { get; }
    public IValue C { get; }

    public IValue DiscriminantSqrt => discriminantSqrt ?? (discriminantSqrt = MathE.Sqrt(Discriminant));

    public IValue Discriminant => discriminant ?? (discriminant = new Sum(B.Squared().Simple(), new Product(new Product(new Integer(-4), A).Simple(), C).Simple()).Simple());
    public IValue Result1 => result1 ?? (result1 = 
      new Fraction(new Sum(B.Negate(), DiscriminantSqrt).Simple(), 
      new Product(A,new Integer(2)).Simple()).Simple());
    public IValue Result2 => result2 ?? (result2 = 
      new Fraction(new Sum(B.Negate(), DiscriminantSqrt.Negate()).Simple(), 
      new Product(A, new Integer(2)).Simple()).Simple());

    public QuadraticFormula(IValue a, IValue b, IValue c)
    {
      A = a;
      B = b;
      C = c; 
    }

  }
}
