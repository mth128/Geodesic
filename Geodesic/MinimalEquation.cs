using Computable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class MinimalEquation
  {

    private static bool initialized = false; 
    private static Geodesic geodesic; 
    public static Vector2D ProjectionPoint;
    public static Vector2D ScaledProjectionPoint;
    public static Vector3D ScaledProjectionPoint3D; 
    public static Vector2D ArcLeft;
    public static Vector2D ArcRight; 
    public static Vector2D FirstSeed;
    public static Vector2D MirrorPerpendicular; 
    public static double ScaleOut;
    public static double ScaleIn;
    public static Equation ScaleOutE;
    public static Equation ScaleInE;
    public static Vector3D FirstSeedE;

    //Mark: i is the most interesting number. This is the distance to the centerline.

    //general calculation values. 
    static double v1x;
    static double v1y;
    static double v2x;
    static double v2y;
    static double v3x;
    static double v3y;
    static double v4x;
    static double v4y;
    static double v5;
    static double v6;


    public static void Initialize()
    {
      if (initialized)
        return;
      initialized = true; 
      geodesic = new Geodesic(-1); 
      ProjectionPoint = Vector2D.XZOf(geodesic.ProjectionPoint);
      ArcLeft = Vector2D.XZOf(Geodesic.ArcLeft);
      ArcRight = Vector2D.XZOf(Geodesic.ArcRight); 
      FirstSeed = Vector2D.XZOf(Geodesic.ArcTopFront);
      MirrorPerpendicular = Vector2D.XZOf(Geodesic.MirrorPerpendicular);
      ScaleIn = Geodesic.EllipseSecondaryRadius.Value;
      ScaleOut = 1 / ScaleIn;
      ScaledProjectionPoint = ScaleEllipseOut(ProjectionPoint);
      ScaledProjectionPoint3D = Geodesic.ScaleEllipseOut(geodesic.ProjectionPoint);

      ScaleInE = Geodesic.EllipseSecondaryRadius;
      ScaleOutE = 1 / ScaleInE;
      FirstSeedE = new Vector3D(Geodesic.ArcTopFront.X, Geodesic.ArcTopFront.Z);

       v1x = ProjectionPoint.x;
       v1y = ProjectionPoint.y;
       v2x = ArcLeft.x;
       v2y = ArcLeft.y;
       v3x = ScaledProjectionPoint.x;
       v3y = ScaledProjectionPoint.y;
       v4x = MirrorPerpendicular.x;
       v4y = MirrorPerpendicular.y;
       v5 = ScaleOut;
       v6 = ScaleIn;

    }

    public static Vector2D ScaleEllipseIn(Vector2D vector)
    {
      Vector2D primary = ArcLeft;
      Vector2D secondary = ArcLeft.Rotate90;
      double dPrimary = vector.Dot(primary);
      double dSecondary = vector.Dot(secondary);
      return primary * dPrimary + secondary * (dSecondary * ScaleIn);

      return ArcLeft * vector.Dot(ArcLeft) + ArcLeft.Rotate90 * (vector.Dot(ArcLeft.Rotate90) * ScaleIn);
    }

    public static Vector2D ScaleEllipseOut(Vector2D vector)
    {
      Vector2D primary = ArcLeft;
      Vector2D secondary = ArcLeft.Rotate90;
      double dPrimary = vector.Dot(primary);
      double dSecondary = vector.Dot(secondary);
      return primary * dPrimary + secondary * (dSecondary * ScaleOut);
    }

    public static Vector2D[] Next1(Vector2D seed)
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

    /*
    public static Vector2D[] Next2(Vector2D seed)
    {
      Vector2D lineUnitVector = (seed - ProjectionPoint).UnitVector;
      Vector2D linePoint = ProjectionPoint - lineUnitVector * lineUnitVector.Dot(ProjectionPoint);
      Vector2D top = linePoint + lineUnitVector * Math.Sqrt(1 - linePoint.MagnitudeSquared); 
      Vector2D rotatedTop = new Vector2D(top.x / -2, top.y);
      Vector2D target = ArcLeft * rotatedTop.Dot(ArcLeft) + ArcLeft.Rotate90 * (rotatedTop.Dot(ArcLeft.Rotate90) * ScaleOut);
      Vector2D strikeThroughLineVector = (target - ScaledProjectionPoint).UnitVector;
      Vector2D strikeThroughLinePoint = ScaledProjectionPoint - strikeThroughLineVector * strikeThroughLineVector.Dot(ScaledProjectionPoint);
      Vector2D primaryScaled = strikeThroughLinePoint + strikeThroughLineVector * Math.Sqrt(1 - strikeThroughLinePoint.MagnitudeSquared);
      double distanceToScaledCenterLine = MirrorPerpendicular.Dot(primaryScaled);
      Vector2D secondaryScaled = primaryScaled - MirrorPerpendicular * (distanceToScaledCenterLine * 2);
      Vector2D primary = ArcLeft * primaryScaled.Dot(ArcLeft) + ArcLeft.Rotate90 * (primaryScaled.Dot(ArcLeft.Rotate90) * ScaleIn);
      Vector2D secondary = ArcLeft * secondaryScaled.Dot(ArcLeft) + ArcLeft.Rotate90 * (secondaryScaled.Dot(ArcLeft.Rotate90) * ScaleIn);
      return new Vector2D[] { primary, secondary };
    }
    */
    /*
    public static Vector2D[] Next3(Vector2D v0)
    {
      double v0x = v0.x;
      double v0y = v0.y; 
      double v1x = ProjectionPoint.x;
      double v1y = ProjectionPoint.y;
      double v2x = ArcLeft.x;
      double v2y = ArcLeft.y;
      double v3x = ScaledProjectionPoint.x;
      double v3y = ScaledProjectionPoint.y;
      double v4x = MirrorPerpendicular.x;
      double v4y = MirrorPerpendicular.y;

      double v5 = ScaleOut;
      double v6 = ScaleIn;

      double t0x = v0x - v1x;
      double t0y = v0y - v1y;
      double t0m = Math.Sqrt(t0x * t0x + t0y * t0y);

      double ax = t0x / t0m;
      double ay = t0y / t0m;
      double av1d = ax * v1x + ay * v1y;

      double bx = v1x - ax * av1d;
      double by = v1y - ay * av1d;
      double binv = Math.Sqrt(1 - bx * bx - by * by);

      double dx = (bx + ax * binv) / -2;
      double dy = by + ay * binv;

      double dv2d = dx * v2x + dy * v2y;
      double dv2drot = dx * v2y - dy * v2x;

      double ex = v2x * dv2d + v2y * dv2drot * v5;
      double ey = v2y * dv2d - v2x * dv2drot * v5;

      double t1x = ex - v3x;
      double t1y = ey - v3y; 
      double t1m = Math.Sqrt(t1x * t1x + t1y * t1y);

      double fx = t1x / t1m;
      double fy = t1y / t1m;
      double fv3d = fx * v3x + fy * v3y;

      double gx = v3x - fx * fv3d;
      double gy = v3y - fy * fv3d;
      double ginv = Math.Sqrt(1 - gx * gx - gy * gy);
      
      double hx = gx + fx * ginv;
      double hy = gy + fy * ginv;

      double i = v4x * hx + v4y * hy;

      double jx = hx - v4x * i * 2;
      double jy = hy - v4y * i * 2;
      
      double hv2d = hx * v2x + hy * v2y;
      double hv2drot = hx * v2y - hy * v2x;

      double kx = v2x * hv2d + v2y * hv2drot * v6;
      double ky = v2y * hv2d - v2x * hv2drot * v6;

      double jv2d = jx * v2x + jy * v2y;
      double jv2drot = jx * v2y - jy * v2x;

      double lx = v2x * jv2d + v2y * jv2drot * v6;
      double ly = v2y * jv2d - v2x * jv2drot * v6; 
      
      return new Vector2D[] { new Vector2D(kx,ky), new Vector2D(lx,ly) };
   
      //Mark: We are mostly interested in i. This is the distance to the centerline.
    }*/

    /*
    public static Vector2D[] Next(Vector2D v0)
    {
      //Mark: i is the most interesting number. This is the distance to the centerline.
      
      //gather variables. 
      double v0x = v0.x;//the seed
      double v0y = v0.y;
      double v1x = ProjectionPoint.x;
      double v1y = ProjectionPoint.y;
      double v2x = ArcLeft.x;
      double v2y = ArcLeft.y;
      double v3x = ScaledProjectionPoint.x;
      double v3y = ScaledProjectionPoint.y;
      double v4x = MirrorPerpendicular.x;
      double v4y = MirrorPerpendicular.y;
      double v5 = ScaleOut;
      double v6 = ScaleIn;

      //starting calculation. 
      double ax = v0x - v1x;
      double ay = v0y - v1y;
      double a0 = Math.Sqrt(ax * ax + ay * ay);

      double bx = ax / a0;
      double by = ay / a0;
      double b0 = bx * v1x + by * v1y;

      //c is the base-point of the line from projectionpoint to the seed. 
      double cx = v1x - bx * b0;
      double cy = v1y - by * b0;
      double c0 = Math.Sqrt(1 - cx * cx - cy * cy);

      //d is the rotated new target point
      double dx = (cx + bx * c0) / -2;
      double dy = cy + by * c0;

      double t1 = dx * v2x + dy * v2y;
      double t2 = dx * v2y - dy * v2x;

      //e is the scaled target
      double ex = v2x * t1 + v2y * t2 * v5;
      double ey = v2y * t1 - v2x * t2 * v5;

      double t3x = ex - v3x;
      double t3y = ey - v3y;
      double t3 = Math.Sqrt(t3x * t3x + t3y * t3y);

      double fx = t3x / t3;
      double fy = t3y / t3;
      double f0 = fx * v3x + fy * v3y;

      double gx = v3x - fx * f0;
      double gy = v3y - fy * f0;
      double g0 = Math.Sqrt(1 - gx * gx - gy * gy);

      //h is the scaled primary point
      double hx = gx + fx * g0;
      double hy = gy + fy * g0;

      //i is the distance to the scaled centerline. 
      double i = v4x * hx + v4y * hy;
      double i2 = i * 2; 

      //j is the scaled secondary point
      double jx = hx - v4x * i2;
      double jy = hy - v4y * i2;

      double h0 = hx * v2x + hy * v2y;
      double h1 = (hx * v2y - hy * v2x)*v6;

      //k is the unscaled primary point
      double kx = v2x * h0 + v2y * h1;
      double ky = v2y * h0 - v2x * h1;

      double j0 = jx * v2x + jy * v2y;
      double j1 = (jx * v2y - jy * v2x)*v6;

      //l the unscaled secondary point
      double lx = v2x * j0 + v2y * j1;
      double ly = v2y * j0 - v2x * j1;

      return new Vector2D[] { new Vector2D(kx, ky), new Vector2D(lx, ly) };
    }
    */

    public static BasicSet NextBasic(Vector2D v0, int index)
    {
      //gather variables. 
      double v0x = v0.x;//the seed
      double v0y = v0.y;

      //starting calculation. 
      double ax = v0x - v1x;
      double ay = v0y - v1y;
      double a0 = Math.Sqrt(ax * ax + ay * ay);

      double bx = ax / a0;
      double by = ay / a0;
      double b0 = bx * v1x + by * v1y;

      //c is the base-point of the line from projectionpoint to the seed. 
      double cx = v1x - bx * b0;
      double cy = v1y - by * b0;
      double c0 = Math.Sqrt(1 - cx * cx - cy * cy);

      //d is the rotated new target point
      double dx = (cx + bx * c0) / -2;
      double dy = cy + by * c0;

      double t1 = dx * v2x + dy * v2y;
      double t2 = dx * v2y - dy * v2x;

      //e is the scaled target
      double ex = v2x * t1 + v2y * t2 * v5;
      double ey = v2y * t1 - v2x * t2 * v5;

      double t3x = ex - v3x;
      double t3y = ey - v3y;
      double t3 = Math.Sqrt(t3x * t3x + t3y * t3y);

      double fx = t3x / t3;
      double fy = t3y / t3;
      double f0 = fx * v3x + fy * v3y;

      double gx = v3x - fx * f0;
      double gy = v3y - fy * f0;
      double g0 = Math.Sqrt(1 - gx * gx - gy * gy);

      //h is the scaled primary point
      double hx = gx + fx * g0;
      double hy = gy + fy * g0;

      //i is the distance to the scaled centerline. 
      double i = v4x * hx + v4y * hy;
      double i2 = i * 2;

      //j is the scaled secondary point
      double jx = hx - v4x * i2;
      double jy = hy - v4y * i2;

      double h0 = hx * v2x + hy * v2y;
      double h1 = (hx * v2y - hy * v2x) * v6;

      //k is the unscaled primary point
      double kx = v2x * h0 + v2y * h1;
      double ky = v2y * h0 - v2x * h1;

      double j0 = jx * v2x + jy * v2y;
      double j1 = (jx * v2y - jy * v2x) * v6;

      //l the unscaled secondary point
      double lx = v2x * j0 + v2y * j1;
      double ly = v2y * j0 - v2x * j1;

      BasicSet result = new BasicSet()
      {
        distanceToScaledCenterLine = i,
        primary = new Vector2D(kx, ky),
        secondary = new Vector2D(lx, ly),
        primaryIndex = index << 1,
        secondaryIndex = index << 1 | 1
      };
      return result; 
    }

    public static Vector2D Next(Vector2D v0, bool primary)
    {
      //gather variables. 
      double v0x = v0.x;//the seed
      double v0y = v0.y;

      //starting calculation. 
      double ax = v0x - v1x;
      double ay = v0y - v1y;
      double a0 = Math.Sqrt(ax * ax + ay * ay);

      double bx = ax / a0;
      double by = ay / a0;
      double b0 = bx * v1x + by * v1y;

      //c is the base-point of the line from projectionpoint to the seed. 
      double cx = v1x - bx * b0;
      double cy = v1y - by * b0;
      double c0 = Math.Sqrt(1 - cx * cx - cy * cy);

      //d is the rotated new target point
      double dx = (cx + bx * c0) / -2;
      double dy = cy + by * c0;

      double t1 = dx * v2x + dy * v2y;
      double t2 = dx * v2y - dy * v2x;

      //e is the scaled target
      double ex = v2x * t1 + v2y * t2 * v5;
      double ey = v2y * t1 - v2x * t2 * v5;

      double t3x = ex - v3x;
      double t3y = ey - v3y;
      double t3 = Math.Sqrt(t3x * t3x + t3y * t3y);

      double fx = t3x / t3;
      double fy = t3y / t3;
      double f0 = fx * v3x + fy * v3y;

      double gx = v3x - fx * f0;
      double gy = v3y - fy * f0;
      double g0 = Math.Sqrt(1 - gx * gx - gy * gy);

      //h is the scaled primary point
      double hx = gx + fx * g0;
      double hy = gy + fy * g0;

      //i is the distance to the scaled centerline. 
      double i = v4x * hx + v4y * hy;
      double i2 = i * 2;

      double h0 = hx * v2x + hy * v2y;
      double h1 = (hx * v2y - hy * v2x) * v6;

      if (primary)
      {
        //k is the unscaled primary point
        double kx = v2x * h0 + v2y * h1;
        double ky = v2y * h0 - v2x * h1;
        return new Vector2D(kx, ky);
      }

      //j is the scaled secondary point
      double jx = hx - v4x * i2;
      double jy = hy - v4y * i2;

      double j0 = jx * v2x + jy * v2y;
      double j1 = (jx * v2y - jy * v2x) * v6;

      //l the unscaled secondary point
      double lx = v2x * j0 + v2y * j1;
      double ly = v2y * j0 - v2x * j1;

      return new Vector2D(lx, ly);
    }

    public static Vector2D Next2(Vector2D seed, bool primary)
    {
      Vector2D lineUnitVector = (seed - ProjectionPoint).UnitVector;
      Vector2D linePoint = ProjectionPoint - lineUnitVector * lineUnitVector.Dot(ProjectionPoint);
      Vector2D top = linePoint + lineUnitVector * Math.Sqrt(1 - linePoint.MagnitudeSquared);
      Vector2D rotatedTop = new Vector2D(top.x / -2, top.y);
      Vector2D target = ArcLeft * rotatedTop.Dot(ArcLeft) + ArcLeft.Rotate90 * (rotatedTop.Dot(ArcLeft.Rotate90) * ScaleOut);
      Vector2D strikeThroughLineVector = (target - ScaledProjectionPoint).UnitVector;
      Vector2D strikeThroughLinePoint = ScaledProjectionPoint - strikeThroughLineVector * strikeThroughLineVector.Dot(ScaledProjectionPoint);
      Vector2D primaryScaled = strikeThroughLinePoint + strikeThroughLineVector * Math.Sqrt(1 - strikeThroughLinePoint.MagnitudeSquared);
      double distanceToScaledCenterLine = MirrorPerpendicular.Dot(primaryScaled);

      Vector2D p  = ArcLeft * primaryScaled.Dot(ArcLeft) + ArcLeft.Rotate90 * (primaryScaled.Dot(ArcLeft.Rotate90) * ScaleIn);
      if (primary)
        return p;

      Vector2D secondaryScaled = primaryScaled - MirrorPerpendicular * (distanceToScaledCenterLine * 2);
      Vector2D s = ArcLeft * secondaryScaled.Dot(ArcLeft) + ArcLeft.Rotate90 * (secondaryScaled.Dot(ArcLeft.Rotate90) * ScaleIn);
      return s;
    }


    public static Vector2D ByIndex(int index)
    {
      Initialize(); 

      if (index == 0)
        return ArcLeft;
      if (index == -1)
        return ArcRight;
      if (index == 1)
        return FirstSeed;
      int frontBit = 1;
      while (frontBit <= index)
        frontBit <<= 1;
      Vector2D current = FirstSeed;

      for (frontBit >>= 2; frontBit > 0; frontBit >>= 1)
      {
        bool primary = (index & frontBit) == 0;
        Vector2D test = Next(current, primary);
        current = Next2(current, primary);
        if (Math.Abs(test.x-current.x)>1e-11 || Math.Abs(test.y-current.y)>1e-11)
          throw new Exception("Error"); 
      }

      return current; 
    }

    public static Vector3D[] NextEquational(Vector3D v0)
    {
      //Mark: i is the most interesting number. This is the distance to the centerline.

      //gather variables. 
      Equation v0x = v0.X;//the seed
      Equation v0y = v0.Y;
      Equation v1x = geodesic.ProjectionPoint.X;
      Equation v1y = geodesic.ProjectionPoint.Y;
      Equation v2x = Geodesic.ArcLeft.X;
      Equation v2y = Geodesic.ArcLeft.Y;
      Equation v3x = ScaledProjectionPoint3D.X;
      Equation v3y = ScaledProjectionPoint3D.Y;
      Equation v4x = Geodesic.MirrorPerpendicular.X;
      Equation v4y = Geodesic.MirrorPerpendicular.Y;
      Equation v5 = ScaleOutE;
      Equation v6 = ScaleInE;

      //starting calculation. 
      Equation ax = v0x - v1x;
      Equation ay = v0y - v1y;
      Equation a0 = MathE.Sqrt(ax * ax + ay * ay);
      a0.CustomSimplify(); 

      Equation bx = ax / a0;
      Equation by = ay / a0;
      Equation b0 = bx * v1x + by * v1y;

      //c is the base-point of the line from projectionpoint to the seed. 
      Equation cx = v1x - bx * b0;
      Equation cy = v1y - by * b0;
      Equation c0 = MathE.Sqrt(1 - cx * cx - cy * cy);
      c0.CustomSimplify(); 

      //d is the rotated new target point
      Equation dx = (cx + bx * c0) / -2;
      Equation dy = cy + by * c0;

      Equation t1 = dx * v2x + dy * v2y;
      Equation t2 = dx * v2y - dy * v2x;

      //e is the scaled target
      Equation ex = v2x * t1 + v2y * t2 * v5;
      Equation ey = v2y * t1 - v2x * t2 * v5;

      Equation t3x = ex - v3x;
      Equation t3y = ey - v3y;
      Equation t3 = MathE.Sqrt(t3x * t3x + t3y * t3y);
      t3.CustomSimplify(); 

      Equation fx = t3x / t3;
      Equation fy = t3y / t3;
      Equation f0 = fx * v3x + fy * v3y;

      Equation gx = v3x - fx * f0;
      Equation gy = v3y - fy * f0;
      Equation g0 = MathE.Sqrt(1 - gx * gx - gy * gy);
      g0.CustomSimplify(); 

      //h is the scaled primary point
      Equation hx = gx + fx * g0;
      Equation hy = gy + fy * g0;

      //i is the distance to the scaled centerline. 
      Equation i = v4x * hx + v4y * hy;
      i.CustomSimplify(); 
      Equation i2 = i * 2;

      //j is the scaled secondary point
      Equation jx = hx - v4x * i2;
      Equation jy = hy - v4y * i2;

      Equation h0 = hx * v2x + hy * v2y;
      Equation h1 = (hx * v2y - hy * v2x) * v6;

      //k is the unscaled primary point
      Equation kx = v2x * h0 + v2y * h1;
      Equation ky = v2y * h0 - v2x * h1;

      Equation j0 = jx * v2x + jy * v2y;
      Equation j1 = (jx * v2y - jy * v2x) * v6;

      //l the unscaled secondary point
      Equation lx = v2x * j0 + v2y * j1;
      Equation ly = v2y * j0 - v2x * j1;

      return new Vector3D[] { new Vector3D(kx, ky), new Vector3D(lx, ly) };
    }


    /*
    public static Vector2D[] Next4(Vector2D v0)
    {
      double v0x = v0.x;
      double v0y = v0.y;
      double v1x = ProjectionPoint.x;
      double v1y = ProjectionPoint.y;
      double v2x = ArcLeft.x;
      double v2y = ArcLeft.y;
      double v3x = ScaledProjectionPoint.x;
      double v3y = ScaledProjectionPoint.y;
      double v4x = MirrorPerpendicular.x;
      double v4y = MirrorPerpendicular.y;

      double v5 = ScaleOut;
      double v6 = ScaleIn;

      double t0x = v0x - v1x;
      double t0y = v0y - v1y;
      double t0m = Math.Sqrt(t0x * t0x + t0y * t0y);

      double ax = t0x / t0m;
      double ay = t0y / t0m;
      double av1d = ax * v1x + ay * v1y;

   
      Vector2D lineUnitVector = (v0 - ProjectionPoint).UnitVector;

      double bx = v1x - ax * av1d;
      double by = v1y - ay * av1d;
      double binv = Math.Sqrt(1 - bx * bx - by * by);

      Vector2D linePoint = ProjectionPoint - lineUnitVector * lineUnitVector.Dot(ProjectionPoint);


      double dx = (bx + ax * binv) / -2;
      double dy = by + ay * binv;

      Vector2D top = linePoint + lineUnitVector * Math.Sqrt(1 - linePoint.MagnitudeSquared);
      Vector2D rotatedTop = new Vector2D(top.x / -2, top.y);

      double dv2d = dx * v2x + dy * v2y;
      double dv2drot = dx * v2y - dy * v2x;

      double ex = v2x * dv2d + v2y * dv2drot * v5;
      double ey = v2y * dv2d - v2x * dv2drot * v5;
      Vector2D target = ArcLeft * rotatedTop.Dot(ArcLeft) + ArcLeft.Rotate90 * (rotatedTop.Dot(ArcLeft.Rotate90) * ScaleOut);

      double t1x = ex - v3x;
      double t1y = ey - v3y;
      double t1m = Math.Sqrt(t1x * t1x + t1y * t1y);

      double fx = t1x / t1m;
      double fy = t1y / t1m;
      double fv3d = fx * v3x + fy * v3y;
      Vector2D strikeThroughLineVector = (target - ScaledProjectionPoint).UnitVector;

      double gx = v3x - fx * fv3d;
      double gy = v3y - fy * fv3d;
      double ginv = Math.Sqrt(1 - gx * gx - gy * gy);
      Vector2D strikeThroughLinePoint = ScaledProjectionPoint - strikeThroughLineVector * strikeThroughLineVector.Dot(ScaledProjectionPoint);

      double hx = gx + fx * ginv;
      double hy = gy + fy * ginv;

      Vector2D primaryScaled = strikeThroughLinePoint + strikeThroughLineVector * Math.Sqrt(1 - strikeThroughLinePoint.MagnitudeSquared);

      double i = v4x * hx + v4y * hy;

      double distanceToScaledCenterLine = MirrorPerpendicular.Dot(primaryScaled);

      double jx = hx - v4x * i * 2;
      double jy = hy - v4y * i * 2;

      Vector2D secondaryScaled = primaryScaled - MirrorPerpendicular * (distanceToScaledCenterLine * 2);

      double hv2d = hx * v2x + hy * v2y;
      double hv2drot = hx * v2y - hy * v2x;

      double kx = v2x * hv2d + v2y * hv2drot * v6;
      double ky = v2y * hv2d - v2x * hv2drot * v6;
      Vector2D primary = ArcLeft * primaryScaled.Dot(ArcLeft) + ArcLeft.Rotate90 * (primaryScaled.Dot(ArcLeft.Rotate90) * ScaleIn);

      double jv2d = jx * v2x + jy * v2y;
      double jv2drot = jx * v2y - jy * v2x;
      
      double lx = v2x * jv2d + v2y * jv2drot * v6;
      double ly = v2y * jv2d - v2x * jv2drot * v6;
      Vector2D secondary = ArcLeft * secondaryScaled.Dot(ArcLeft) + ArcLeft.Rotate90 * (secondaryScaled.Dot(ArcLeft.Rotate90) * ScaleIn);

      return new Vector2D[] { new Vector2D(kx, ky), new Vector2D(lx, ly) };

      //Mark: We are mostly interested in i. This is the distance to the centerline.
    }
    */
  }

  public class BasicSet
  {
    public double distanceToScaledCenterLine;
    public Vector2D primary;
    public Vector2D secondary;
    public int primaryIndex;
    public int secondaryIndex; 
  }
}
