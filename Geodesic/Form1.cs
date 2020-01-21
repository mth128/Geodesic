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

      TraceCompute oneThird = (center.Sigma * 2 + bound.Sigma) / 3;
      TraceCompute centerLineX = oneThird.Sin();
      TraceCompute centerLineY = oneThird.Cos();

      StrikeThroughPointPair topStrikeThrough = new StrikeThroughPointPair(centerLineX);
      Line strikeLine = Line.Construct(topStrikeThrough.Right, new Vector3D(0, 0, 1));
      Vector3D projectionPoint = strikeLine.Intersect(Line.Construct(Geodesic.ArcTopRight, new Vector3D(0, 0, 0)));
      StrikeThroughPointPair test = new StrikeThroughPointPair(bound.DistanceToScaledCenterLine);

      //Geodesic geodesic = new Geodesic(4,projectionPoint);
      Geodesic geodesic = new Geodesic(7);

      TraceCompute variance = VarianceOf(geodesic); 

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

    private void Button2_Click(object sender, EventArgs e)
    {
      VarianceLabel.Text = (VarianceOf(new Geodesic(Convert.ToInt32(GenerationBox.Text)-2))*100-100).ToString()+"%"; 
    }

    private void Button3_Click(object sender, EventArgs e)
    {
      Geodesic geodesic = new Geodesic(4);

      Plane plane = geodesic.StrikeThroughPoints[13].RightPlane; 

    }

    private void Button4_Click(object sender, EventArgs e)
    {
      Geodesic geodesic = new Geodesic();
      List<string> points = new List<string>();
      for (int i = 0; i <= 64; i++)
        points.Add(geodesic.GetStrikePoint(i).ToString());

      using (SaveFileDialog sfd = new SaveFileDialog())
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;
        System.IO.File.WriteAllLines(sfd.FileName, points);
      }
    }

    private void Button5_Click(object sender, EventArgs e)
    {
      Geodesic geodesic = new Geodesic(1);
      List<string> areas = new List<string>(); 
      for (int i =0; i<geodesic.MaxGridIndex;i++)
      {
        GridIndex index = geodesic.GetGridIndex(i);
        GeodesicGridTriangle triangle = index.GeodesicGridTriangle;
        areas.Add(triangle.Area.ToString());
      }

      using (SaveFileDialog sfd = new SaveFileDialog())
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;
        System.IO.File.WriteAllLines(sfd.FileName, areas);
      }
    }

    public TraceCompute VarianceOf(Geodesic geodesic)
    {
      List<TraceCompute> areas = new List<TraceCompute>();
      TraceCompute totalArea = new TraceCompute(0);
      TraceCompute maxArea = new TraceCompute (-10);
      TraceCompute minArea = new TraceCompute(10);
      for (int i = 0; i < geodesic.MaxGridIndex; i++)
      {
        GridIndex index = geodesic.GetGridIndex(i);
        GeodesicGridTriangle triangle = index.GeodesicGridTriangle;
        TraceCompute area = triangle.Area;
        areas.Add(area);
        totalArea += area;
        if (area > maxArea)
          maxArea = area;
        if (area < minArea)
          minArea = area;
      }
      TraceCompute variance = maxArea / minArea;
      return variance; 

    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void DownShiftBox_Click(object sender, EventArgs e)
    {
      ShiftBox.Text = (Convert.ToDouble(ShiftBox.Text) / 2).ToString(); 
    }

    private void UpShiftBox_Click(object sender, EventArgs e)
    {
      ShiftBox.Text = (Convert.ToDouble(ShiftBox.Text) * 2).ToString();
    }

    private void UpButton_Click(object sender, EventArgs e)
    {
      VarianceBox.Text = (Convert.ToDouble(VarianceBox.Text) + Convert.ToDouble(ShiftBox.Text)).ToString(); 
    }

    private void DownButton_Click(object sender, EventArgs e)
    {
      VarianceBox.Text = (Convert.ToDouble(VarianceBox.Text) - Convert.ToDouble(ShiftBox.Text)).ToString();
    }

    private void VarianceBox_TextChanged(object sender, EventArgs e)
    {
      try
      {
        Vector3D projectionPoint = Geodesic.DefaultProjectionPoint * new TraceCompute(Convert.ToDouble(VarianceBox.Text),"Custom");
        Geodesic geodesic = new Geodesic(0, projectionPoint);
        TraceCompute variance = VarianceOf(geodesic);
        VarianceLabel.Text = variance.ToString(); 
      }
      catch
      {

      }
    }

    private void Button6_Click(object sender, EventArgs e)
    {
      OldGeodesic oldGeodesic = new OldGeodesic(Convert.ToInt32(GenerationBox.Text));
      TraceCompute min = new TraceCompute(10);
      TraceCompute max = new TraceCompute(0);
      foreach (SphericalTriangle triangle in oldGeodesic.SphericalTriangles)
      {
        TraceCompute area = triangle.Area;
        if (area < min)
          min = area;
        if (area > max)
          max = area; 
      }
      TraceCompute variance = max / min;
      VarianceLabel.Text = (variance*100-100).ToString()+"%"; 
    }

    private void Button7_Click(object sender, EventArgs e)
    {
      List<TraceCompute> sigmas = new List<TraceCompute>();
      List<string> lines = new List<string>();
      Geodesic geodesic = new Geodesic();

      TraceCompute step = new TraceCompute(1) / geodesic.MaxRibIndex; 
      for (int i =0; i<=geodesic.MaxRibIndex;i++)
      {
        Vector3D point = geodesic.GetStrikePoint(i);
        TraceCompute sigma = geodesic.Sigma(point);
        sigmas.Add(sigma);
        lines.Add((step * i).ToString() + "\t" + sigma.ToString());// + "\t" + sigma.Equation); 
      }
      using (SaveFileDialog sfd = new SaveFileDialog())
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;

        System.IO.File.WriteAllLines(sfd.FileName, lines);
      }
    }

    private void Button8_Click(object sender, EventArgs e)
    {
      List<TraceCompute> sigmas = new List<TraceCompute>();
      List<string> lines = new List<string>();
      Geodesic geodesic = new Geodesic();

      TraceCompute step = new TraceCompute(1) / geodesic.MaxRibIndex;
      for (int i = 0; i <= geodesic.MaxRibIndex; i++)
      {
        Vector3D point = geodesic.GetStrikePoint(i);
        TraceCompute sigma = geodesic.Sigma(point);
        sigmas.Add(sigma);
        List<string> equation = sigma.GetFullEquation();
        lines.Add((step * i).ToString() + "\t" + sigma.ToString());
        lines.AddRange(equation);
        lines.Add("-----------------------------------"); 
      }
      using (SaveFileDialog sfd = new SaveFileDialog())
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;

        System.IO.File.WriteAllLines(sfd.FileName, lines);
      }
    }

    private void Button9_Click(object sender, EventArgs e)
    {
      try
      {
        Fraction a = new Fraction(Convert.ToInt64(Numerator1Box.Text), Convert.ToInt64(Denominator1Box.Text));
        Fraction b = new Fraction(Convert.ToInt64(Numerator2Box.Text), Convert.ToInt64(Denominator2Box.Text));

        Fraction add = a + b;
        Fraction subtract = a - b;
        Fraction multiply = a * b;
        Fraction divide = a / b;

        ResultLabel.Text = add.ToString() + "\n";
        ResultLabel.Text += subtract.ToString() + "\n";
        ResultLabel.Text += multiply.ToString() + "\n";
        ResultLabel.Text += divide.ToString() + "\n";
      }
      catch (Exception ex)
      {
        ResultLabel.Text = ex.Message; 
      }
    }
  }
}
