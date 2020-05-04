using Computable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geodesic
{
  public class StrikeThroughPointPair
  {
    internal static double minimalSigma;
    internal static double maximalSigma; 
    public Equation onArcValue; 

    public Geodesic Parent { get; }
    public Equation DistanceToScaledCenterLine { get; }
    public Vector3D Right { get; }
    public Vector3D Left { get; }
    public int LeftIndex { get; }
    public int RightIndex { get; }

    public Equation DistanceOnScaledCenterLine => new Equation(MathE.Sqrt(new Equation(1)-MathE.Squared(DistanceToScaledCenterLine)));

    //The "on arc" angle. 
    public double Sigma => MathE.Asin(DistanceToScaledCenterLine);

    //The expected "on arc" angle, in case Sigma would be linear. 
    public double ExpectedSigma => onArcValue * (maximalSigma - minimalSigma) + minimalSigma;

    public Plane RightPlane => new Plane(Right, Parent.ProjectionPoint, Right + new Vector3D(0, 1, 0));
    public Plane LeftPlane => new Plane(Left, Parent.ProjectionPoint, Left + new Vector3D(0, 1, 0));

    public StrikeThroughPointPair(Geodesic geodesic, Vector3D seed, int index)
    { 
      Line line = Line.Construct(geodesic.ProjectionPoint, seed.Front);
      Vector3D top = line.UnitSphereIntersectionPositiveZ;
      Vector3D rotatedTop = top.RotateTop120Front;
      Vector3D target = Geodesic.ScaleEllipseOut(rotatedTop);
      Line strikeThrough = Line.Construct(Geodesic.ScaleEllipseOut(geodesic.ProjectionPoint), target);
      Vector3D primaryScaled = strikeThrough.UnitSphereIntersectionPositiveZ;

      DistanceToScaledCenterLine = Geodesic.MirrorPerpendicular.Dot(primaryScaled);
      Vector3D secondaryScaled = primaryScaled - Geodesic.MirrorPerpendicular * (DistanceToScaledCenterLine * 2);
      Right = Geodesic.ScaleEllipseIn(primaryScaled);
      Left = Geodesic.ScaleEllipseIn(secondaryScaled);
      RightIndex = index << 1;
      LeftIndex = RightIndex | 1;

      if (Left.X>Right.X)
      {
        Vector3D swap = Left;
        Left = Right;
        Right = swap;
        int swapIndex = LeftIndex;
        LeftIndex = RightIndex;
        RightIndex = swapIndex; 
      }

      Parent = geodesic; 
    }

    public StrikeThroughPointPair(Equation mirrorDistance)
    {
      DistanceToScaledCenterLine = MathE.Abs(mirrorDistance);
      Equation primaryDistance = MathE.Sqrt(new Equation(1) - MathE.Squared(DistanceToScaledCenterLine));
      Vector3D primaryScaled = Geodesic.MirrorPoint * primaryDistance + Geodesic.MirrorPerpendicular * DistanceToScaledCenterLine;
      Vector3D secondaryScaled = Geodesic.MirrorPoint * primaryDistance - Geodesic.MirrorPerpendicular * DistanceToScaledCenterLine;
      Right = Geodesic.ScaleEllipseIn(primaryScaled);
      Left = Geodesic.ScaleEllipseIn(secondaryScaled);
    }
    public StrikeThroughPointPair(int mirrorDistance)
      : this (new Equation(mirrorDistance))
    {
    }

  }
}
