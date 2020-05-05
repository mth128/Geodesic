using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class MinimalEquation
  {
    public static Vector2D ProjectionPoint;
    public static Vector2D ArcLeft; 
    public static Vector2D FirstSeed;
    public static Vector2D MirrorPerpendicular; 
    public static double ScaleOut;
    public static double ScaleIn;

    public static void Initialize()
    {
      Geodesic geodesic = new Geodesic(-1); 
      ProjectionPoint = Vector2D.XZOf(geodesic.ProjectionPoint);
      ArcLeft = Vector2D.XZOf(Geodesic.ArcLeft);
      FirstSeed = Vector2D.XZOf(Geodesic.ArcTopFront);
      MirrorPerpendicular = Vector2D.XZOf(Geodesic.MirrorPerpendicular);
      ScaleIn = Geodesic.EllipseSecondaryRadius.Value;
      ScaleOut = 1 / ScaleIn; 
    }

    public static Vector2D ScaleEllipseIn(Vector2D vector)
    {
      Vector2D primary = ArcLeft;
      Vector2D secondary = ArcLeft.Rotate90;
      double dPrimary = vector.Dot(primary);
      double dSecondary = vector.Dot(secondary);
      return primary * dPrimary + secondary * (dSecondary * ScaleIn);
    }

    public static Vector2D ScaleEllipseOut(Vector2D vector)
    {
      Vector2D primary = ArcLeft;
      Vector2D secondary = ArcLeft.Rotate90;
      double dPrimary = vector.Dot(primary);
      double dSecondary = vector.Dot(secondary);
      return primary * dPrimary + secondary * (dSecondary * ScaleOut);
    }

    public static Vector2D[] Next(Vector2D seed)
    {
      Line2D line = Line2D.Construct(ProjectionPoint, seed);
      Vector2D top = line.Reach1;
      Vector2D rotatedTop = new Vector2D(top.x/-2, top.y);
      Vector2D target = ScaleEllipseOut(rotatedTop);
      Line2D strikeThrough = Line2D.Construct(ScaleEllipseOut(ProjectionPoint), target);
      Vector2D primaryScaled = strikeThrough.Reach1;
      double distanceToScaledCenterLine = MirrorPerpendicular.Dot(primaryScaled);
      Vector2D secondaryScaled = primaryScaled - MirrorPerpendicular * (distanceToScaledCenterLine * 2);
      Vector2D primary = ScaleEllipseIn(primaryScaled);
      Vector2D secondary = ScaleEllipseIn(secondaryScaled);
      return new Vector2D[] { primary, secondary }; 
    }
  }
}
