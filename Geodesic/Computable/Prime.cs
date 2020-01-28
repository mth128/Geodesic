using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Prime
{
  private static int limit = 268435456;
  private static bool[] prime;
  private static int max = 524288;

  public static int[] Primes { get; } = PrimesBelow(max); 

  public static bool CalculateIsPrime(long value)
  {
    int max = Convert.ToInt32(Math.Ceiling(Math.Sqrt(value)));
    for (int i = 2; i < max; i++)
      if (prime[i] && (value % i == 0))
        return false;
    return true;       
  }
  public static bool IsPrime(long value)
  {
    if (value < limit)
      return IsPrime((int)value);

    InitializePrimes(limit);

    long sqrt = Convert.ToInt64(Math.Sqrt(value)); 
    for (long i =0; i<sqrt;i++)
    {
      if (!prime[i])
        continue;
      if (value % i == 0)
        return false; 
    }
    return true; 
  }

  public static void InitializePrimes()
  {
    InitializePrimes(max); 
  }

  public static void InitializePrimes(int value)
  {
    if (max == limit)
      return; 

    if (prime == null || value > max)
    {
      while (max < value && max!= limit)
      {
        if (max < limit/2)
          max = max * 2;
        else
          max = limit;
      }
      //Console.WriteLine("Initializing primes smaller than " + max + ".");
      prime = new bool[max];
      for (int i = 2; i < max; i++)
        prime[i] = true;
      //Parallel.For(2, max, i =>
      //{
      for (long i = 2; i<max;i++)
        for (long j = i * 2; j < max; j += i)
          prime[j] = false;
      //});
    }
  }

  public static bool IsPrime(int value)
  {
    InitializePrimes(value); 
    return prime[value]; 
  }

  public static int[] PrimesBelow(int maximum)
  {
    List<int> result = new List<int>();
    for (int i = 2; i < maximum; i++)
      if (IsPrime(i))
        result.Add(i);
    return result.ToArray(); 
  }

  public static List<long> Factorize(long value)
  {
    if (value < 0)
      value = -value;

    if (value <2)
      return new List<long>() { value };
    if (value >= 281474976710656)
      throw new OverflowException();

    List<long> factors = new List<long>();
    foreach (int prime in Prime.Primes)
    {
      while (value % prime == 0)
      {
        factors.Add(prime);
        value /= prime;
      }
      if (prime*prime>value)
      {
        break; 
      }
    }
    if (value!=1)
      factors.Add(value); 
    return factors;
  }
}


public class FactorCounter
{
  public int factor;
  public int counter; 
}

