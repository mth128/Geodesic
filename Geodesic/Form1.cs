//using Computable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
    /*
    private void Button2_Click(object sender, EventArgs e)
    {
      VarianceLabel.Text = (VarianceOf(new Geodesic(Convert.ToInt32(GenerationBox.Text)-2))*100-100).ToString()+"%"; 
    }*/

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

    /*
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
    }*/

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

     
    private void VarianceButton_Click(object sender, EventArgs e)
    {
      int generation = Convert.ToInt32(GenerationBox.Text);
      BisectGeodesic bisectGeodesic = new BisectGeodesic(generation);
      double min = 10;
      double max = 0;
      foreach (SphericalTriangle triangle in bisectGeodesic.SphericalTriangles)
      {
        double area = triangle.Area;
        if (area < min)
          min = area;
        if (area > max)
          max = area; 
      }
      double bisectVariance = max / min;
      double geodesicViariance = VarianceOf(new Geodesic(Convert.ToInt32(generation) - 2));

      string bisectVarianceString = "Bisect: "+(bisectVariance * 100 - 100).ToString() + "%";
      string geodesicVarianceString = "Projection point: " +(geodesicViariance * 100 - 100).ToString() + "%"; 

      MessageBox.Show(bisectVarianceString + " - " + geodesicVarianceString); 
    }
    /*
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
    }*/

    private void Button8_Click(object sender, EventArgs e)
    {
      List<double> sigmas = new List<double>();
      List<string> lines = new List<string>();
      Geodesic geodesic = new Geodesic();

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
    /*
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
    }*/

    private void ResultLabel_Click(object sender, EventArgs e)
    {

    }

    /*
    private void Button10_Click(object sender, EventArgs e)
    {
      Equation IcosahedronRibLength = new Equation(4) / MathE.Sqrt(new Equation(10) + MathE.Sqrt(new Equation(20)));

      Equation FrontViewLength = MathE.Sqrt(new Fraction(new Product(MathE.Squared(IcosahedronRibLength), 3), 4));
      Equation FrontViewLength13 = FrontViewLength / 3;
      Equation FrontViewLength23 = FrontViewLength13 * 2;

      Vector3D ArcLeft = new Vector3D(-FrontViewLength23, 0, MathE.Sqrt(new Equation(1) - FrontViewLength23 * FrontViewLength23));
      Vector3D ArcRight = new Vector3D(FrontViewLength13, IcosahedronRibLength / 2, ArcLeft.Z);
      Vector3D DefaultProjectionPoint  = new Vector3D(ArcLeft.X, 0, ArcRight.Z * -2);
      Equation length = DefaultProjectionPoint.Magnitude; 
    }*/

      /*
    private void OpenEquationFormButton_Click(object sender, EventArgs e)
    {
      using (SimplifyForm form = new SimplifyForm())
        form.ShowDialog(); 
    }*/

    private void AnalyseButton_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "*.csv|*.csv" })
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;
        Geodesic geodesic = new Geodesic(Convert.ToInt32(GenerationBox.Text));
        List<string> lines = new List<string>();
        string header = "Count;Index;IndexBin;Distance;DistanceEquation;X;Y;";
        lines.Add(header);
        int i = 0; 
        foreach(StrikeThroughPointPair pair in geodesic.StrikeThroughPoints)
        {
          string line = i.ToString() + ";";
          i++;
          line += pair.LeftIndex.ToString() + ";";
          line += Convert.ToString(pair.LeftIndex, 2)+";";
          line += (-pair.DistanceToScaledCenterLine).ToString() + ";";
          //line += (-pair.DistanceToScaledCenterLine.Value).ToString()+";";
          //line += (-pair.DistanceToScaledCenterLine).Content.Equation + ";";
          //line += pair.Left.X.Value.ToString() + ";";
          //line += pair.Left.Y.Value.ToString() + ";";

          line += pair.Left.X.ToString() + ";";
          line += pair.Left.Y.ToString() + ";";

          lines.Add(line);
          line = "";

          line+=i.ToString() + ";";
          i++;
          line += pair.RightIndex.ToString() + ";";
          line += Convert.ToString(pair.RightIndex, 2) + ";";
          line += pair.DistanceToScaledCenterLine.ToString() + ";";
          //line += pair.DistanceToScaledCenterLine.Value.ToString() + ";";
          //line += pair.DistanceToScaledCenterLine.Content.Equation + ";";
          line += pair.Right.X.ToString() + ";";
          line += pair.Right.Y.ToString() + ";";
          //line += pair.Right.X.Value.ToString() + ";";
          //line += pair.Right.Y.Value.ToString() + ";";

          lines.Add(line);
        }
      }
    }

    private void TestMinimalButton_Click(object sender, EventArgs e)
    {
      MinimalEquation.Initialize();
      Vector2D[] first = MinimalEquation.Next1(MinimalEquation.FirstSeed);
    }

    /*
    private void TestMinimal2Button_Click(object sender, EventArgs e)
    {
      MinimalEquation.Initialize();
      Vector3D[] first = MinimalEquation.NextEquational(MinimalEquation.FirstSeedE);
    }*/

    private void GeodesicAnalysisButton_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "*.csv|*.csv" })
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;

        List<ScaledCenterlinePair> current = new List<ScaledCenterlinePair>();
        List<ScaledCenterlinePair> next = new List<ScaledCenterlinePair>();

        MinimalEquation.Initialize();
        Vector2D seed = MinimalEquation.FirstSeed;

        ScaledCenterlinePair basicSet = new ScaledCenterlinePair();
        basicSet.primary = seed;
        basicSet.distanceToScaledCenterLine = 0;
        basicSet.primaryIndex = 1;

        current.Add(basicSet);

        List<string> lines = new List<string>();
        string header = "Count;Index;IndexBin;Distance;X;Y;";
        lines.Add(header);
        long i = 1;
        long generations = Convert.ToInt32(GenerationBox.Text);

        for (int g = -1; g < generations; g++)
        {
          foreach (ScaledCenterlinePair set in current)
          {
            string line = i.ToString() + ";";
            i++;
            line += set.primaryIndex.ToString() + ";";
            line += Convert.ToString(set.primaryIndex, 2) + ";";
            line += (-set.distanceToScaledCenterLine).ToString() + ";";
            line += set.primary.x.ToString() + ";";
            line += set.primary.y.ToString() + ";";
            lines.Add(line);
            line = "";

            if (set.secondary != null)
            {
              line += i.ToString() + ";";
              i++;
              line += set.secondaryIndex.ToString() + ";";
              line += Convert.ToString(set.secondaryIndex, 2) + ";";
              line += set.distanceToScaledCenterLine.ToString() + ";";
              line += set.secondary.x.ToString() + ";";
              line += set.secondary.y.ToString() + ";";
              lines.Add(line);
            }
            
            if (g<generations-1)
            {
              next.Add(MinimalEquation.NextBasic(set.primary, set.primaryIndex));
              if (set.secondary != null)
                next.Add(MinimalEquation.NextBasic(set.secondary, set.secondaryIndex));
            }
          }
          current = next;
          next = new List<ScaledCenterlinePair>(); 
        }
        File.WriteAllLines(sfd.FileName, lines); 
      }
    }

    private void GetByIndexButton_Click(object sender, EventArgs e)
    {
      try
      {
        long index = Convert.ToInt64(IndexBox.Text);
        Vector2D result = MinimalEquation.ByIndex(index);
        XBox.Text = result.x.ToString();
        YBox.Text = result.y.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error"); 
      }
    }

    private void GetByPositionButton_Click(object sender, EventArgs e)
    {
      try
      {
        double position = Convert.ToDouble(PositionBox.Text);

        BoundPair pair = MinimalEquation.GetByX(position);
        UpperPositionBox.Text = pair.upperIndex.ToString();
        UpperXBox.Text = pair.upper.x.ToString();
        UpperRangeBox.Text = pair.upperRange.ToString();
        LowerPositionBox.Text = pair.lowerIndex.ToString();
        LowerXBox.Text = pair.lower.x.ToString();
        LowerRangeBox.Text = pair.lowerRange.ToString();
        ItterationsBox.Text = pair.itterations.ToString();
        NextGenerationCallsBox.Text = pair.nextGenerationCalls.ToString(); 
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error");
      }
    }

    private void RangePositionButton_Click(object sender, EventArgs e)
    {
      try
      {
        double position = Convert.ToDouble(RangeBox.Text);
        BoundPair pair = MinimalEquation.GetByRange(position);
        UpperPositionBox.Text = pair.upperIndex.ToString();
        UpperXBox.Text = pair.upper.x.ToString();
        UpperRangeBox.Text = pair.upperRange.ToString(); 
        LowerPositionBox.Text = pair.lowerIndex.ToString();
        LowerXBox.Text = pair.lower.x.ToString();
        LowerRangeBox.Text = pair.lowerRange.ToString();
        ItterationsBox.Text = pair.itterations.ToString();
        NextGenerationCallsBox.Text = pair.nextGenerationCalls.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error");
      }
    }
  }
}
