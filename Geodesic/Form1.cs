using Computable;
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

    /*
    private void Button1_Click(object sender, EventArgs e)
    {
      StrikeThroughPointPair center = new StrikeThroughPointPair(0);
      StrikeThroughPointPair bound = new StrikeThroughPointPair(Geodesic.ArcLeft.Dot(Geodesic.MirrorPerpendicular));

      Equation oneThird = (center.Sigma * 2 + bound.Sigma) / 3;
      Equation centerLineX = Math.Sin(oneThird);
      Equation centerLineY = Math.Cos(oneThird);

      StrikeThroughPointPair topStrikeThrough = new StrikeThroughPointPair(centerLineX);
      Line strikeLine = Line.Construct(topStrikeThrough.Right, new Vector3D(0, 0, 1));
      Vector3D projectionPoint = strikeLine.Intersect(Line.Construct(Geodesic.ArcTopRight, new Vector3D(0, 0, 0)));
      StrikeThroughPointPair test = new StrikeThroughPointPair(bound.DistanceToScaledCenterLine);

      //Geodesic geodesic = new Geodesic(4,projectionPoint);
      Geodesic geodesic = new Geodesic(7);

      Equation variance = VarianceOf(geodesic); 

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

    }*/

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

    public double VarianceOf(Geodesic geodesic)
    {
      List<double> areas = new List<double>();
      double totalArea = 0;
      double maxArea = -10;
      double minArea = 10;
      for (int i = 0; i < geodesic.MaxGridIndex; i++)
      {
        GridIndex index = geodesic.GetGridIndex(i);
        GeodesicGridTriangle triangle = index.GeodesicGridTriangle;
        double area = triangle.Area;
        areas.Add(area);
        totalArea += area;
        if (area > maxArea)
          maxArea = area;
        if (area < minArea)
          minArea = area;
      }
      double variance = maxArea / minArea;
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

    /*
    private void VarianceBox_TextChanged(object sender, EventArgs e)
    {
      try
      {
        Vector3D projectionPoint = Geodesic.DefaultProjectionPoint * new Equation(Convert.ToDouble(VarianceBox.Text),"Custom");
        Geodesic geodesic = new Geodesic(0, projectionPoint);
        Equation variance = VarianceOf(geodesic);
        VarianceLabel.Text = variance.ToString(); 
      }
      catch
      {

      }
    }*/

    private void Button6_Click(object sender, EventArgs e)
    {
      OldGeodesic oldGeodesic = new OldGeodesic(Convert.ToInt32(GenerationBox.Text));
      double min = 10;
      double max = 0;
      foreach (SphericalTriangle triangle in oldGeodesic.SphericalTriangles)
      {
        double area = triangle.Area;
        if (area < min)
          min = area;
        if (area > max)
          max = area; 
      }
      double variance = max / min;
      VarianceLabel.Text = (variance*100-100).ToString()+"%"; 
    }

    private void Button7_Click(object sender, EventArgs e)
    {
      List<double> sigmas = new List<double>();
      List<string> lines = new List<string>();
      Geodesic geodesic = new Geodesic();

      Equation step = new Equation(1) / geodesic.MaxRibIndex; 
      for (int i =0; i<=geodesic.MaxRibIndex;i++)
      {
        Vector3D point = geodesic.GetStrikePoint(i);
        double sigma = geodesic.Sigma(point);
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
      List<double> sigmas = new List<double>();
      List<string> lines = new List<string>();
      Geodesic geodesic = new Geodesic();

      Equation step = new Equation(1) / geodesic.MaxRibIndex;
      for (int i = 0; i <= geodesic.MaxRibIndex; i++)
      {
        Vector3D point = geodesic.GetStrikePoint(i);
        double sigma = geodesic.Sigma(point);
        sigmas.Add(sigma);
        //List<string> equation = sigma.GetFullEquation();
        //lines.Add((step * i).ToString() + "\t" + sigma.ToString());
        //lines.AddRange(equation);
        lines.Add(point.X.ToString());
        lines.Add(point.Y.ToString());
        lines.Add(point.Z.ToString());
        lines.Add(sigma.ToString()); 
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
        Radical radical = new Radical(a,b);

        ResultLabel.Text =  a.Value.ToString() + "+" + b.Value.ToString() +"="+ add.ToString() + "\n";
        ResultLabel.Text += a.Value.ToString() + "-" + b.Value.ToString() + "=" + subtract.ToString() + "\n";
        ResultLabel.Text += a.Value.ToString() + "*" + b.Value.ToString() + "=" + multiply.ToString() + "\n";
        ResultLabel.Text += a.Value.ToString() + "/" + b.Value.ToString() + "=" + divide.ToString() + "\n";
        ResultLabel.Text += b.Value.ToString() + "*Sqrt(" + a.Value.ToString() + ")="+  radical.ToString() + "\n";  
      }
      catch (Exception ex)
      {
        ResultLabel.Text = ex.Message; 
      }
    }

    private void ResultLabel_Click(object sender, EventArgs e)
    {

    }
  }
}
