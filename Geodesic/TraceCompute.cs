using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class TraceCompute : IComparable<TraceCompute>
  {
    private static int counter = 0;
    private string name = "";

    public string Name { get => name == "" ? name = VariableName(counter++) : name; }  
    public TraceCompute First { get; }
    public TraceCompute Second { get; }
    public string Action { get; }

    public string Pre { get; } = "";
    public string Post { get; } = "";
    public string Between { get; } = "";
    public int Input { get; }
    public double Value { get; }
    public List<string> GetFullEquation()
    {
      if (First == null)
        return new List<string> { Value.ToString() };

      HashSet<TraceCompute> previous = new HashSet<TraceCompute>();
      HashSet<TraceCompute> current = new HashSet<TraceCompute>();
      List<string> lines = new List<string>(); 
      current.Add(this);

      while (current.Count!=0)
      {
        HashSet<TraceCompute> next = new HashSet<TraceCompute>(); 
        foreach (TraceCompute value in current)
        {
          if (previous.Contains(value))
            continue;
          previous.Add(value);
          string line = value.GetNamedEquation();
          if (line[0]>='a'&&line[0]<='z')
            lines.Add(line);
          foreach (TraceCompute nextValue in value.GetDependancies())
            next.Add(nextValue);
        }
        current = next; 
      }
      return lines; 
    }

    public TraceCompute(double value, string action)
    {
      Value = value;
      Action = action; 
    }

    public TraceCompute(int value)
    {
      Input = value; 
      Value = value;
      name = value.ToString(); 
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
      return new TraceCompute(a, b, "Multiply", a.Value * b.Value, "(", "*", ")");
    }

    public static TraceCompute operator /(TraceCompute a, TraceCompute b)
    {
      return new TraceCompute(a, b, "Divide", a.Value / b.Value, "(", "/", ")");
    }

    public static TraceCompute operator +(TraceCompute a, int b)
    {
      return new TraceCompute(a, new TraceCompute(b), "Add", a.Value + b, "(", "+", ")");
    }

    public static TraceCompute operator -(TraceCompute a, int b)
    {
      return new TraceCompute(a, new TraceCompute(b), "Subtract", a.Value - b, "(", "-", ")");
    }
    public static TraceCompute operator *(TraceCompute a, int b)
    {
      return new TraceCompute(a, new TraceCompute(b), "Multiply", a.Value * b, "(", "*", ")");
    }

    public static TraceCompute operator /(TraceCompute a, int b)
    {
      return new TraceCompute(a, new TraceCompute(b), "Divide", a.Value / b, "(", "/", ")");
    }

    public static bool operator >(TraceCompute a, double b)
    {
      return a.Value > b; 
    }

    public static bool operator <(TraceCompute a, double b)
    {
      return a.Value < b;
    }

    public static bool operator >=(TraceCompute a, double b)
    {
      return a.Value >= b;
    }

    public static bool operator <=(TraceCompute a, double b)
    {
      return a.Value <= b;
    }


    public static bool operator >(TraceCompute a, TraceCompute b)
    {
      return a.Value > b.Value;
    }

    public static bool operator <(TraceCompute a, TraceCompute b)
    {
      return a.Value < b.Value;
    }

    public static bool operator >=(TraceCompute a, TraceCompute b)
    {
      return a.Value >= b.Value;
    }

    public static bool operator <=(TraceCompute a, TraceCompute b)
    {
      return a.Value <= b.Value;
    }

    public static bool operator ==(TraceCompute a, TraceCompute b)
    {
      if (object.ReferenceEquals(a, null))
        return (object.ReferenceEquals(b, null));
      if (object.ReferenceEquals(b, null))
        return false;
      return a.Value == b.Value;
    }

    public static bool operator !=(TraceCompute a, TraceCompute b)
    {
      if (object.ReferenceEquals(a, null))
        return !(object.ReferenceEquals(b, null));
      if (object.ReferenceEquals(b, null))
        return true;
      return a.Value != b.Value;
    }
    public static bool operator ==(TraceCompute a, double b)
    {
      return a.Value == b;
    }
    public static bool operator !=(TraceCompute a, double b)
    {
      return a.Value != b;
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

    public TraceCompute Asin()
    {
      return new TraceCompute(this, null, "Asin", Math.Asin(Value), "Asin(", "", ")");
    }

    public TraceCompute Acos()
    {
      return new TraceCompute(this, null, "Acos", Math.Acos(Value), "Acos(", "", ")");
    }
    public TraceCompute Atan()
    {
      return new TraceCompute(this, null, "Atan", Math.Atan(Value), "Atan(", "", ")");
    }

    public string VariableName(int index)
    {
      index++; 
      string result = "";
      while (index != 0)
      {
        int letterIndex = index % 26;
        index /= 26;
        if (letterIndex == 0)
        {
          letterIndex = 26;
          index--; 
        }
        result = Convert.ToChar('a' + letterIndex-1) + result; 
      }
      return result;
    }

    public string GetEquation()
    {
      if (Second == null)
      {
        if (First == null)
          return Value.ToString();
        return Pre + First.GetEquation() + Between + Post;
      }

      if (First.Value == Value)
        return First.GetEquation();
      if (Second.Value == Value)
        return Second.GetEquation(); 

      return Pre + First.Name + Between + Second.Name + Post; 
    }

    public List<TraceCompute> GetDependancies()
    {
      List<TraceCompute> dependancies = new List<TraceCompute>(); 

      if (Second == null)
      {
        if (First == null)
          return dependancies;
        return First.GetDependancies();
      }

      if (First.Value == Value)
        return First.GetDependancies();
      if (Second.Value == Value)
        return Second.GetDependancies();

      dependancies.Add(First);
      dependancies.Add(Second); 
      return dependancies;

    }

    public string GetNamedEquation()
    {
      return Name + "=" + Value.ToString() + "=" + GetEquation(); 
    }

   
    public override string ToString()
    {
      return Value.ToString(); 
    }

    public static TraceCompute PI()
    {
      return new TraceCompute(Math.PI, "Pi"); 
    }

    public int CompareTo(TraceCompute other)
    {
      return Value.CompareTo(other.Value); 
    }

    public override bool Equals(object obj)
    {
      return obj is TraceCompute compute &&
             Value == compute.Value && compute.First == First && compute.Second == Second;
    }

    public override int GetHashCode()
    {
      return -1937169414 + Value.GetHashCode();
    }
    

  }

  public class TraceComputeEquation
  {
    public string Equation { get; set; } = "";
    public HashSet<TraceCompute> TraceComputes { get; } = new HashSet<TraceCompute>(); 
    public double Value { get; }
    public string Name { get; }

    public TraceComputeEquation(TraceCompute value, string name)
    {
      TraceComputes.Add(value);
      Value = value.Value;
      Name = name; 
    }

    public void Add(TraceCompute value)
    {
      if (Value != value.Value)
        throw new Exception("Value mismatch!");
      TraceComputes.Add(value); 
    }

  }
}
