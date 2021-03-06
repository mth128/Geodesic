﻿using Computable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geodesic.Computable.CustomSimplify
{
  public class CustomSimplifyStorage
  {
    public static string DefaultFileName = "custom.eqt";
    public string FileName { get; set; } = "custom.eqt";
    public Dictionary<string, Equation> library = new Dictionary<string, Equation>();
    public Dictionary<double, Equation> libraryByValue = new Dictionary<double, Equation>(); 

    public static CustomSimplifyStorage main = FromFile(DefaultFileName); 

    public static CustomSimplifyStorage FromFile (string fileName)
    {
      if (!File.Exists(fileName))
        return new CustomSimplifyStorage() { FileName = fileName };

      CustomSimplifyStorage storage = new CustomSimplifyStorage();
      storage.FileName = fileName;
      var formatter = new BinaryFormatter();
      using (FileStream stream = File.OpenRead(fileName))
      {
        while (stream.Position < stream.Length)
        {
          string key = formatter.Deserialize(stream) as string;
          Equation equation = formatter.Deserialize(stream) as Equation;
          storage.library[key] = equation;
          storage.libraryByValue[equation.Value] = equation; 
        }
      }
      return storage; 
    }

    public static IValue CustomSimplify(IValue equation)
    {
      if (main.library.TryGetValue(equation.Equation, out Equation value))
        return value;

      if (main.libraryByValue.TryGetValue(equation.Value, out Equation value2))
        return value2;

      double v = equation.Value;
      if (main.libraryByValue.Count < 4000)
        foreach (KeyValuePair<double, Equation> pair in main.libraryByValue)
          if (Math.Abs(pair.Key - v) < 1.0e-11)
          {
            main.library[equation.Equation] = pair.Value;
            main.Save(main.FileName);
            main.libraryByValue[v] = pair.Value; 
            return pair.Value; 
          }

      if (SimplifyForm.Instances > 0)
        return equation; 
      using (SimplifyForm simplifyForm = new SimplifyForm(equation))
      {
        if (simplifyForm.ShowDialog() == DialogResult.OK)
        {
          main.library[equation.Equation] = simplifyForm.CustomEquation;
          main.Save(main.FileName);
          main.libraryByValue[equation.Value] = simplifyForm.CustomEquation; 
          return simplifyForm.CustomEquation; 
        }
      }
      return equation; 
    }

    public void Save(string fileName)
    {
      CustomSimplifyStorage storage = new CustomSimplifyStorage();
      var formatter = new BinaryFormatter();
      using (FileStream stream = File.Create(fileName))
      {
        foreach (KeyValuePair<string, Equation> pair in library)
        {
          formatter.Serialize(stream, pair.Key);
          formatter.Serialize(stream, pair.Value); 
        }
      }
    }
  }
}
