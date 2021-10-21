using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Drawing
{
  public class ColorScale
  {
    public double Max { get; }
    public double Min { get; }
    public double Target { get; }
    public double MaxDeviation { get; }
    public double MinDeviation { get; }
    public double BigDeviation { get; }
    public bool Enhanced { get; set; } = true;

    public ColorScale(double min, double max, double target)
    {
      Max = max;
      Min = min;
      Target = target;
      MaxDeviation = max - target;     
      MinDeviation = target - min;
      BigDeviation = MaxDeviation > MinDeviation ? MaxDeviation : MinDeviation;
    }

    public Color GenerateEnhancedColorFor(double value)
    {
      Color color;
      //Color offWhite = Color.FromArgb(240, 240, 240);
      Color offWhite = Color.FromArgb(255, 255, 255);
      if (value == Target)
        return offWhite;
      if (value < Target)
      {
        double deviation = Target - value;
        double scale = deviation / BigDeviation;
        double enhanced = Enhance(scale);
        if (scale > 1)
          scale = 1;
        if (scale < 0)
          scale = 0;
        if (enhanced > 1)
          enhanced = 1;
        if (enhanced < 0)
          enhanced = 0;
        int greenNess = Convert.ToInt32((1 - scale) * 255);
        int redNess = Convert.ToInt32((1 - enhanced) * 255);
        color = Color.FromArgb(redNess, greenNess, 255);
      }
      else
      {
        double deviation = value - Target;
        double scale = deviation / BigDeviation;
        double enhanced = Enhance(scale);
        if (scale > 1)
          scale = 1;
        if (scale < 0)
          scale = 0;
        if (enhanced > 1)
          enhanced = 1;
        if (enhanced < 0)
          enhanced = 0;

        int blueNess = Convert.ToInt32((1 - enhanced) * 255);
        int greenNess = Convert.ToInt32((1 - scale) * 255);
        color = Color.FromArgb(255, greenNess, blueNess);
      }
      if (color.R == 255 && color.G == 255 && color.B == 255)
        return offWhite;
      return color; 
    }

    private double Enhance(double value)
    {
      if (!Enhanced)
        return value; 
      return Math.Sqrt(Math.Sqrt(value));
    }

  }
}
