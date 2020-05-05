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

    public IValue Content { get; private set; }

    public Integer DivisorIntegerComponent => Content.DivisorIntegerComponent;

    public int Complexity => Content.Complexity;

    public Equation(IValue value)
    {
      if (value == null)
        Content = new Integer(0);

      Content = value.Direct();
      if (Content.Complexity > 5) 
        CustomSimplify(); 
    }

    public void CustomSimplify()
    {
      if (Content.Complexity > 5 && SimplifyForm.Instances < 1)
      {
        Content = Geodesic.Computable.CustomSimplify.CustomSimplifyStorage.CustomSimplify(this);
      }
    }

    public Equation(string equation)
    {
      const string valid = "0123456789sqrt-+*/() ";

      equation = equation.ToLower(); 

      if (equation == "")
      { 
        throw new Exception("String is empty"); 
      }

      bool integer = true;
      bool hasNumber = false;
      int currentOpenBracketsCount = 0;
      int bracketStartPosition = 0;

      int valueStart = -1;

      List<IValue> subEquations = new List<IValue>();
      string sequence = ""; 

      for (int i = 0; i<equation.Length;i++)
      {
        char c = equation[i];
        if (!valid.Contains(c))
          throw new Exception("Character " + c + " not allowed. You an only use " + valid);

        if (c <= '9' && c >= '0')
          hasNumber = true;

        if (c == '(')
        {
          if (currentOpenBracketsCount == 0)
            bracketStartPosition = i;
          currentOpenBracketsCount++;
        }
        else if (c == ')')
        {
          currentOpenBracketsCount--;
          if (currentOpenBracketsCount == 0)
          {
            subEquations.Add(new Equation(equation.Substring(bracketStartPosition + 1, i - bracketStartPosition - 1)));
            sequence += "e"; 
          }
        }
        if (currentOpenBracketsCount != 0)
          continue;

        if (c <= '9' && c >= '0')
        {
          if (valueStart == -1)
            valueStart = i;
        }
        else
        {
          integer = false;
          if (valueStart != -1)
          {
            //integer found. Add it.
            string integerString = equation.Substring(valueStart, i - valueStart);
            subEquations.Add(new Integer(Convert.ToInt64(integerString)));
            valueStart = -1;
            sequence += "e";
          }
        }

        if (c == 's')
        {
          if (equation[i + 1] == 'q' && equation[i + 2] == 'r')
          {
            sequence += 's';
            i += 2;
            if (equation[i + 1] == 't')
              i++;
          }
        }
        else if (c == '+')
          sequence += '+';
        else if (c == '-')
          sequence += '-';
        else if (c == '*')
          sequence += '*';
        else if (c == '/')
          sequence += '/';
      }
      


      if (integer)
      {
        //this is an integer. Returning integer.
        Content = new Integer(Convert.ToInt64(equation));
        return; 
      }

      if (valueStart != -1)
      {
        //we still need to add an integer.
        string integerString = equation.Substring(valueStart);
        subEquations.Add(new Integer(Convert.ToInt64(integerString)));
        sequence += "e";
      }

      if (!hasNumber)
        throw new Exception("No integer found in equation " + equation); 
      if (currentOpenBracketsCount != 0)
        throw new Exception("Mismatch in amount of open and closing brackets in equation "+ equation);

      string newSequence = "";
      //+-*/es

      //insert hidden multiplications
      for (int i = 0; i<sequence.Length-1; i++)
      {
        char current = sequence[i];
        char next = sequence[i+1];
        newSequence += current;
        if (current == 'e' && (next == 'e' || next == 's'))
          newSequence += '*';
      }
      newSequence += sequence.Last();

      sequence = newSequence;

      newSequence = "";
      List<IValue> newEquations = new List<IValue>();
      int equationIndex = 0; 
      //handle square roots
      for (int i = 0; i<sequence.Length;i++)
      {
        char c = sequence[i];
        if (c == 's')
        {
          //sanity check
          if (sequence[i + 1] != 'e')
            throw new Exception("Sqaure root should be followed by a value in brackets. Equation: " + equation);

          newEquations.Add(new Radical(subEquations[equationIndex]));
          i++;
          equationIndex++;
          newSequence += 'e';
        }
        else if (c == 'e')
        {
          newEquations.Add(subEquations[equationIndex]);
          equationIndex++; 
          newSequence += 'e';
        }
        else
          newSequence += c; 
      }

      sequence = newSequence;
      subEquations = newEquations;

      newSequence = "";
      newEquations = new List<IValue>(); 

      bool previousIsValue = false;
      //finding negatives
      for (int i = 0; i < sequence.Length; i++)
      {
        char c = sequence[i];
        if (c == '-')
        {
          if (previousIsValue)
            newSequence += '-';
          else
            newSequence += 'n';
        }
        else
        {
          newSequence += c;
        }

        previousIsValue = c == 'e'; 
      }
      sequence = newSequence;



      //subdividing using + and - signs. 
      string subSequence = "";
      List<IValue> betweenValues = new List<IValue>();
      IValue left = null;
      int currentIndex = 0;
      bool plus = false; 
      for (int i = 0; i < sequence.Length; i++)
      {
        char c = sequence[i];
        if (c == '-' || c == '+')
        {
          
          if (left==null)
            left = GenerateSubSequence(subSequence, betweenValues);
          else if (plus)
            left = new Sum(left, GenerateSubSequence(subSequence, betweenValues));
          else
            left = new Sum(left, GenerateSubSequence(subSequence, betweenValues).Negate()); 

          plus = c == '+';
          subSequence = "";
          betweenValues = new List<IValue>(); 
        }
        else if (c == 'e')
        {
          betweenValues.Add(subEquations[currentIndex]);
          currentIndex++;
          subSequence += c; 
        }
        else
        {
          subSequence += c; 
        }
      }
      if (subSequence == "")
        Content = left.Simple();
      else if (left == null)
        Content = GenerateSubSequence(subSequence, betweenValues).Simple();
      else if (plus)
        Content = new Sum(left, GenerateSubSequence(subSequence, betweenValues)).Simple();
      else
        Content = new Sum(left, GenerateSubSequence(subSequence, betweenValues).Negate()).Simple();
    }

    private IValue GenerateSubSequence(string sequence, List<IValue> values)
    {
      //handle n (negative), e (equation), * (multiply) and / (devision)
      int valueIndex = 0;
      string newSequence = ""; 
      for (int i = 0; i < sequence.Length; i++)
      {
        char c = sequence[i];
        if (c == 'n')
        {
          values[valueIndex] = values[valueIndex].Negate();
        }
        else if (c == 'e')
        {
          valueIndex++;
          newSequence += 'e';
        }
        else
          newSequence += c;
      }
      sequence = newSequence;

      if (sequence == "e")
      {
        return values[0];
      }

      while (sequence.Length > 1)
      {
        IValue startValue; 

        if (sequence.Substring(0, 3) == "e*e")
          startValue = new Product(values[0], values[1]);
        else if (sequence.Substring(0, 3) == "e/e")
          startValue = new Fraction(values[0], values[1]);
        else
          throw new Exception("Sequence [" + sequence + "] is not a valid multiplaction or fraction.");

        List<IValue> newValues = new List<IValue>();
          newValues.Add(startValue);
        if (values.Count > 2)
          newValues.AddRange(values.GetRange(2, values.Count - 2));
        values = newValues;
        sequence = sequence.Substring(2); 
      }

      return values[0];
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
