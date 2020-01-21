using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class StrikeThroughPointPair
  {
    internal static TraceCompute minimalSigma;
    internal static TraceCompute maximalSigma; 
    public TraceCompute onArcValue; 

    public Geodesic Parent { get; }
    public TraceCompute DistanceToScaledCenterLine { get; }
    public Vector3D Right { get; }
    public Vector3D Left { get; }
    public TraceCompute DistanceOnScaledCenterLine => (new TraceCompute(1)-DistanceToScaledCenterLine.Squared()).Sqrt();

    //The "on arc" angle. 
    public TraceCompute Sigma => DistanceToScaledCenterLine.Asin();

    //The expected "on arc" angle, in case Sigma would be linear. 
    public TraceCompute ExpectedSigma => onArcValue * (maximalSigma - minimalSigma) + minimalSigma;

    public Plane RightPlane => new Plane(Right, Parent.ProjectionPoint, Right + new Vector3D(0, 1, 0));
    public Plane LeftPlane => new Plane(Left, Parent.ProjectionPoint, Left + new Vector3D(0, 1, 0));

    public StrikeThroughPointPair(Geodesic geodesic, Vector3D seed)
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

      if (Left.X>Right.X)
      {
        Vector3D swap = Left;
        Left = Right;
        Right = swap; 
      }

      Parent = geodesic; 
    }

    public StrikeThroughPointPair(TraceCompute mirrorDistance)
    {
      DistanceToScaledCenterLine = mirrorDistance.Abs();
      TraceCompute primaryDistance = (new TraceCompute(1) - DistanceToScaledCenterLine.Squared()).Sqrt();
      Vector3D primaryScaled = Geodesic.MirrorPoint * primaryDistance + Geodesic.MirrorPerpendicular * DistanceToScaledCenterLine;
      Vector3D secondaryScaled = Geodesic.MirrorPoint * primaryDistance - Geodesic.MirrorPerpendicular * DistanceToScaledCenterLine;
      Right = Geodesic.ScaleEllipseIn(primaryScaled);
      Left = Geodesic.ScaleEllipseIn(secondaryScaled);
    }
    public StrikeThroughPointPair(int mirrorDistance)
      : this (new TraceCompute(mirrorDistance))
    {
    }

  }
}
