using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geodesic
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
      StrikeThroughPointPair center = new StrikeThroughPointPair(0);
      StrikeThroughPointPair bound = new StrikeThroughPointPair(Geodesic.ArcLeft.Dot(Geodesic.MirrorPerpendicular));

      double oneThird = (center.Sigma * 2 + bound.Sigma) / 3;
      double centerLineX = Math.Sin(oneThird);
      double centerLineY = Math.Cos(oneThird);

      StrikeThroughPointPair topStrikeThrough = new StrikeThroughPointPair(centerLineX);
      Line strikeLine = Line.Construct(topStrikeThrough.Primary, new Vector3D(0, 0, 1));
      Vector3D projectionPoint = strikeLine.Intersect(Line.Construct(Geodesic.ArcTopRight, new Vector3D(0, 0, 0)));
      StrikeThroughPointPair test = new StrikeThroughPointPair(bound.DistanceToScaledCenterLine);

      Geodesic geodesic = new Geodesic(4,projectionPoint); 
      //Geodesic geodesic = new Geodesic(4);

      using (SaveFileDialog sfd = new SaveFileDialog())
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;

        List<string> lines = new List<string>(); 
        foreach(StrikeThroughPointPair pointPair in geodesic.StrikeThroughPoints)
        {
          lines.Add(pointPair.DistanceToScaledCenterLine.ToString() + "\t" + pointPair.DistanceOnScaledCenterLine.ToString());
        }
        System.IO.File.WriteAllLines(sfd.FileName, lines);
      }

    }
  }
}
