using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class TraceCompute
  {
    public TraceCompute First { get; }
    public TraceCompute Second { get; }
    public string Action { get; }

    public string Pre { get; } = "";
    public string Post { get; } = "";
    public string Between { get; } = "";
    public int Input { get; }
    public double Value { get; }
    public TraceCompute(int value)
    {
      Input = value; 
      Value = value; 
    }

    public TraceCompute(TraceCompute first, TraceCompute second, string action, double value, string pre = "", string between = "", string post ="")
    {
      First = first;
      Second = second;
      Action = action;
      Value = value;
      Pre = pre; 
      Between = between;
      Post = post; 
    }

    public static TraceCompute operator -(TraceCompute value)
    {
      return new TraceCompute(value, null, "Negative", -value.Value,"-");

    }

    public static TraceCompute operator +(TraceCompute a, TraceCompute b)
    {
      return new TraceCompute(a, b, "Add", a.Value + b.Value,"(","+",")"); 
    }

    public static TraceCompute operator -(TraceCompute a, TraceCompute b)
    {
      return new TraceCompute(a, b, "Subtract", a.Value - b.Value, "(", "-", ")");
    }
    public static TraceCompute operator *(TraceCompute a, TraceCompute b)
    {
      return new TraceCompute(a, b, "Multiply", a.Value - b.Value, "(", "*", ")");
    }

    public static TraceCompute operator /(TraceCompute a, TraceCompute b)
    {
      return new TraceCompute(a, b, "Divide", a.Value / b.Value, "(", "/", ")");
    }

    public TraceCompute Abs()
    {
      return new TraceCompute(this, null, "Abs", Math.Abs(Value), "Abs(", "", ")");
    }

    public TraceCompute Squared()
    {
      return new TraceCompute(this, null, "Squared", Value*Value, "(", "", ")^2");
    }

    public TraceCompute Sqrt()
    {
      return new TraceCompute(this, null, "Sqrt", Math.Sqrt(Value), "Sqrt(", "", ")");      
    }
    public TraceCompute Sin()
    {
      return new TraceCompute(this, null, "Sin", Math.Sin(Value), "Sin(", "", ")");
    }

    public TraceCompute Cos()
    {
      return new TraceCompute(this, null, "Cos", Math.Cos(Value), "Cos(", "", ")");
    }
    public TraceCompute Tan()
    {
      return new TraceCompute(this, null, "Tan", Math.Tan(Value), "Tan(", "", ")");
    }

    public TraceCompute ASin()
    {
      return new TraceCompute(this, null, "ASin", Math.Asin(Value), "ASin(", "", ")");
    }

    public TraceCompute ACos()
    {
      return new TraceCompute(this, null, "ACos", Math.Acos(Value), "ACos(", "", ")");
    }
    public TraceCompute ATan()
    {
      return new TraceCompute(this, null, "ATan", Math.Atan(Value), "ATan(", "", ")");
    }

    public override string ToString()
    {
      if (First == null && Second == null)
        return Value.ToString();
      if (Second == null)
        return Pre + First.ToString() + Between + Post;
      return Pre + First.ToString() + Between + Second.ToString() + Post; 
    }

  }
}
