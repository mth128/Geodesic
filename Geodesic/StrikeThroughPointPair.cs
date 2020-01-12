using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodesic
{
  public class StrikeThroughPointPair
  {
    public double DistanceToScaledCenterLine { get; }
    public Vector3D Primary { get; }
    public Vector3D Secondary { get; }
    public double DistanceOnScaledCenterLine => Math.Sqrt(1 - DistanceToScaledCenterLine * DistanceToScaledCenterLine);
    public double Sigma => Math.Atan2(DistanceToScaledCenterLine, DistanceOnScaledCenterLine);

    public StrikeThroughPointPair(Geodesic geodesic, Vector3D seed)
    {
      Line line = Line.Construct(geodesic.ProjectionPoint, seed.Front);
      Vector3D top = line.UnitSphereIntersectionPositiveZ;
      Vector3D rotatedTop = top.RotateTop120Front;
      Vector3D target = Geodesic.ScaleEllipseOut(rotatedTop);
      Line strikeThrough = Line.Construct(Geodesic.ScaleEllipseOut(geodesic.ProjectionPoint), target);
      Vector3D primaryScaled = strikeThrough.UnitSphereIntersectionPositiveZ;

      DistanceToScaledCenterLine = Geodesic.MirrorPerpendicular.Dot(primaryScaled);
      Vector3D secondaryScaled = primaryScaled - Geodesic.MirrorPerpendicular * DistanceToScaledCenterLine * 2;
      Primary = Geodesic.ScaleEllipseIn(primaryScaled);
      Secondary = Geodesic.ScaleEllipseIn(secondaryScaled);
    }

    public StrikeThroughPointPair(double mirrorDistance)
    {
      DistanceToScaledCenterLine = Math.Abs(mirrorDistance);
      double primaryDistance = Math.Sqrt(1 - DistanceToScaledCenterLine * DistanceToScaledCenterLine);
      Vector3D primaryScaled = Geodesic.MirrorPoint * primaryDistance + Geodesic.MirrorPerpendicular * DistanceToScaledCenterLine;
      Vector3D secondaryScaled = Geodesic.MirrorPoint * primaryDistance - Geodesic.MirrorPerpendicular * DistanceToScaledCenterLine;
      Primary = Geodesic.ScaleEllipseIn(primaryScaled);
      Secondary = Geodesic.ScaleEllipseIn(secondaryScaled);
    }
  }
}
