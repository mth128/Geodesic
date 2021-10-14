//using Computable;
using Geodesic.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Geodesic
{
  public partial class TestForm : Form
  {
    public TestForm()
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
      for (int i = 0; i < geodesic.MaxGridIndex; i++)
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

    public Variance VarianceOf(Geodesic geodesic)
    {
      List<double> areas = new List<double>();
      double totalArea = 0;
      double maxArea = -10;
      double minArea = 10;

      double lengthMin = 10;
      double lengthMax = 0;
      double lengthTotal = 0;

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

        double a = (triangle.PointCA - triangle.PointAB).Magnitude;
        double b = (triangle.PointAB - triangle.PointBC).Magnitude;
        double c = (triangle.PointBC - triangle.PointCA).Magnitude;

        double maxLength = a > b ? a > c ? a : c : b > c ? b : c;
        double minLength = a < b ? a < c ? a : c : b < c ? b : c;
        if (maxLength > lengthMax)
          lengthMax = maxLength;
        if (minLength < lengthMin)
          lengthMin = minLength;

        lengthTotal += a + b + c;
      }
      Variance variance = new Variance();
      variance.variance = maxArea / minArea;

      variance.max = maxArea;
      variance.min = minArea;
      variance.average = totalArea / geodesic.MaxGridIndex;

      variance.lengthMin = lengthMin;
      variance.lengthMax = lengthMax;
      variance.lengthAverage = lengthTotal / geodesic.MaxGridIndex / 3;

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

    /// <summary>
    /// Calculate the area variance for both bisect and new geodesic grid for each generation up to the given one. 
    /// This is put in a csv file. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void VarianceButton_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "*.csv|*.csv" })
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;

        List<string> result = new List<string>();
        result.Add("generation,bisect min,bisect max, bisect average, projection point min, projection point max, projection point average," +
          "bisect length min, bisect length max, bisect length average, projection length min, projection length max, projection length average");



        int maxGeneration = Convert.ToInt32(GenerationBox.Text);

        for (int generation = 1; generation <= maxGeneration; generation++)
        {
          BisectGeodesicLowMemory bisectGeodesic = new BisectGeodesicLowMemory(generation);
          double min = 10;
          double max = 0;
          double total = 0;

          double lengthMin = 10;
          double lengthMax = 0;
          double lengthTotal = 0;
          //foreach (SphericalTriangle triangle in bisectGeodesic.SphericalTriangles)
          for (int i = 0; i < bisectGeodesic.TriangleCount; i++)
          {
            SphericalTriangle triangle = bisectGeodesic.GetTriangle(i);
            double area = triangle.Area;
            if (area < min)
              min = area;
            if (area > max)
              max = area;
            total += area;
            double ab = (triangle.A - triangle.B).Magnitude;
            double bc = (triangle.B - triangle.C).Magnitude;
            double ca = (triangle.C - triangle.A).Magnitude;
            double maxLength = ab > bc ? ab > ca ? ab : ca : bc > ca ? bc : ca;
            double minLength = ab < bc ? ab < ca ? ab : ca : bc < ca ? bc : ca;
            if (maxLength > lengthMax)
              lengthMax = maxLength;
            if (minLength < lengthMin)
              lengthMin = minLength;
            lengthTotal += ab + bc + ca;
          }
          double average = total / bisectGeodesic.TriangleCount;
          double averageLength = lengthTotal / bisectGeodesic.TriangleCount / 3;
          double bisectVariance = max / min;
          Geodesic geodesic = new Geodesic(generation - 2);
          Variance geodesicViariance = VarianceOf(geodesic);
          result.Add(generation.ToString() + "," + min.ToString() + "," + max.ToString() + "," + average.ToString() +
            "," + geodesicViariance.min.ToString() + "," + geodesicViariance.max.ToString() + "," + geodesicViariance.average.ToString() + "," +
           lengthMin.ToString() + "," + lengthMax.ToString() + "," + averageLength.ToString() + "," +
           geodesicViariance.lengthMin.ToString() + "," + geodesicViariance.lengthMax.ToString() + "," + geodesicViariance.lengthAverage.ToString()
            );
        }
        File.WriteAllLines(sfd.FileName, result);
      }
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
        foreach (StrikeThroughPointPair pair in geodesic.StrikeThroughPoints)
        {
          string line = i.ToString() + ";";
          i++;
          line += pair.LeftIndex.ToString() + ";";
          line += Convert.ToString(pair.LeftIndex, 2) + ";";
          line += (-pair.DistanceToScaledCenterLine).ToString() + ";";
          //line += (-pair.DistanceToScaledCenterLine.Value).ToString()+";";
          //line += (-pair.DistanceToScaledCenterLine).Content.Equation + ";";
          //line += pair.Left.X.Value.ToString() + ";";
          //line += pair.Left.Y.Value.ToString() + ";";

          line += pair.Left.X.ToString() + ";";
          line += pair.Left.Y.ToString() + ";";

          lines.Add(line);
          line = "";

          line += i.ToString() + ";";
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

            if (g < generations - 1)
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

    private void SavePointsButton_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog sfd = new SaveFileDialog())
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;

        Geodesic geodesic = new Geodesic(Convert.ToInt32(GenerationBox.Text));
        List<string> lines = new List<string>();

        for (int i = 0; i < geodesic.MaxGridIndex; i++)
        {
          GridIndex index = geodesic.GetGridIndex(i);
          GeodesicGridTriangle triangle = index.GeodesicGridTriangle;

          foreach (Vector3D point in triangle.Points)
            lines.Add(point.ToString());
        }

        System.IO.File.WriteAllLines(sfd.FileName, lines);
      }
    }

    private void GenerateButton_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "*.csv|*.csv" })
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;

        int finalLevel = Convert.ToInt32(LevelBox.Text);
        List<string> result = new List<string>();
        for (int level = 0; level <= finalLevel; level++)
        {
          if (level == 0)
          {
            result.Add(new Vector3D().ToString(true));
            continue;
          }

          List<Plane> planes = new List<Plane>();
          List<Plane> rotatedPlanes = new List<Plane>();
          double range = 1;
          double step = 1.0 / level;
          for (int i = 0; i <= level; i++, range -= step)
          {
            if (range < 0)
              range = 0;
            Vector2D cutPoint = MinimalEquation.GetVector2DByRange(range);
            Plane plane = MinimalEquation.GetPlane(cutPoint);
            planes.Add(plane);
            rotatedPlanes.Add(plane.RotateTop120);
          }

          List<Vector3D> points = new List<Vector3D>();
          for (int i = 0; i <= level; i++)
          {
            for (int j = 0; j + i <= level; j++)
            {
              Plane a = planes[i];
              Plane b = rotatedPlanes[j];
              Line intersection = b.Intersection(a);
              Vector3D point = intersection.UnitSphereIntersection1;
              points.Add(point);
            }
          }
          foreach (Vector3D point in points)
            result.Add((point * level).ToString(true));

        }
        File.WriteAllLines(sfd.FileName, result);
      }
    }


    /// <summary>
    /// Calculate the area variance with a shifting projection point. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MultipleVarianceButton_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "*.csv|*.csv" })
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;

        double min = 0;
        double max = 2;
        double step = 0.001;

        int steps = Convert.ToInt32((Math.Round((max - min) / step)));

        double current = step;
        int generation = Convert.ToInt32(GenerationBox.Text);
        List<string> content = new List<string>();
        content.Add("Position,Variance,");

        for (int i = 0; i < steps; i++, current += step)
        {
          Geodesic geodesic = new Geodesic(Convert.ToInt32(generation) - 2, Geodesic.DefaultProjectionPoint * current);
          double variance = VarianceOf(geodesic).variance;
          content.Add(current.ToString() + "," + variance.ToString() + ",");
        }
        File.WriteAllLines(sfd.FileName, content);
      }

    }

    private void DrawAreaVarianceButton_Click(object sender, EventArgs e)
    {
      int generation = Convert.ToInt32(GenerationBox.Text);
      Geodesic geodesic = new Geodesic(generation - 2);

      BisectGeodesic bisectGeodesic = new BisectGeodesic(generation);

      List<DrawTriangle> triangles = new List<DrawTriangle>();
      List<DrawTriangle> bisectTriangles = new List<DrawTriangle>();

      double minArea = 10;
      double maxArea = 0;
      double totalArea = 0;
      double totalBisectArea = 0;

      for (int i = 0; i < geodesic.MaxGridIndex; i++)
      {
        GridIndex index = geodesic.GetGridIndex(i);
        GeodesicGridTriangle triangle = index.GeodesicGridTriangle;
        double area = triangle.Area;
        if (area < minArea)
          minArea = area;
        if (area > maxArea)
          maxArea = area;
        totalArea += area;
      }

      foreach (SphericalTriangle triangle in bisectGeodesic.SphericalTriangles)
      {
        double area = triangle.Area;
        if (area < minArea)
          minArea = area;
        if (area > maxArea)
          maxArea = area;
        totalBisectArea += area;
      }

      double averageArea = totalArea / geodesic.MaxGridIndex;
      double averageBisectArea = totalBisectArea / bisectGeodesic.SphericalTriangles.Count;

      for (int i = 0; i < geodesic.MaxGridIndex; i++)
      {
        GridIndex index = geodesic.GetGridIndex(i);
        GeodesicGridTriangle triangle = index.GeodesicGridTriangle;
        DrawTriangle drawTriangle = new DrawTriangle()
        {
          point1 = triangle.PointAB,
          point2 = triangle.PointBC,
          point3 = triangle.PointCA
        };
        drawTriangle.fillColor = GenerateEnhancedColorFor(triangle.Area, minArea, maxArea, averageArea);
        triangles.Add(drawTriangle);
      }

      foreach (SphericalTriangle triangle in bisectGeodesic.SphericalTriangles)
      {
        DrawTriangle drawTriangle = new DrawTriangle()
        {
          point1 = triangle.A,
          point2 = triangle.B,
          point3 = triangle.C
        };
        drawTriangle.fillColor = GenerateEnhancedColorFor(triangle.Area, minArea, maxArea, averageBisectArea);
        bisectTriangles.Add(drawTriangle);
      }


      IllustrationForm illustrationForm = new IllustrationForm();
      //using (IllustrationForm illustrationForm = new IllustrationForm())
      {
        illustrationForm.triangles = triangles;
        illustrationForm.fill = true;
        illustrationForm.lines = false;
        illustrationForm.Text = "Cut Geodesic Grid";
        illustrationForm.Show();
      }

      IllustrationForm bisectForm = new IllustrationForm();
      //using (IllustrationForm illustrationForm = new IllustrationForm())
      {
        bisectForm.triangles = bisectTriangles;
        bisectForm.fill = true;
        bisectForm.lines = false;
        bisectForm.Text = "Bisect Geodesic Grid";
        bisectForm.Show();
      }

      DrawScale(minArea, maxArea, averageArea);

    }





    private Color GenerateColorFor(double area, double minArea, double maxArea, double averageArea)
    {
      double maxDeviation = maxArea - averageArea;
      double minDeviation = averageArea - minArea;

      double bigDeviation = maxDeviation > minDeviation ? maxDeviation : minDeviation;

      if (area == averageArea)
        return Color.White;
      if (area < averageArea)
      {
        double deviation = averageArea - area;
        int yellowNess = Convert.ToInt32((1 - deviation / bigDeviation) * 255);

        return Color.FromArgb(yellowNess, yellowNess, 255);
      }
      else
      {
        double deviation = area - averageArea;
        int cyanNess = Convert.ToInt32((1 - deviation / bigDeviation) * 255);

        return Color.FromArgb(255, cyanNess, cyanNess);
      }

    }

    private Color GenerateEnhancedColorFor(double area, double minArea, double maxArea, double averageArea)
    {
      double maxDeviation = maxArea - averageArea;
      double minDeviation = averageArea - minArea;

      double bigDeviation = maxDeviation > minDeviation ? maxDeviation : minDeviation;

      if (area == averageArea)
        return Color.White;
      if (area < averageArea)
      {
        double deviation = averageArea - area;
        double scale = deviation / bigDeviation;
        double enhanced = Enhance(scale);
        int greenNess = Convert.ToInt32((1 - scale) * 255);
        int redNess = Convert.ToInt32((1 - enhanced) * 255);
        return Color.FromArgb(redNess, greenNess, 255);
      }
      else
      {
        double deviation = area - averageArea;
        double scale = deviation / bigDeviation;
        double enhanced = Enhance(scale);
        int blueNess = Convert.ToInt32((1 - enhanced) * 255);
        int greenNess = Convert.ToInt32((1 - scale) * 255);
        return Color.FromArgb(255, greenNess, blueNess);
      }

    }

    private double Enhance(double value)
    {
      return Math.Sqrt(Math.Sqrt(value));
    }

    private void DrawScale(double minArea, double maxArea, double averageArea)
    {
      double deviation1 = maxArea - averageArea;
      double deviation2 = averageArea - minArea;
      double deviation = deviation1 > deviation2 ? deviation1 : deviation2;
      minArea = averageArea - deviation;
      maxArea = averageArea + deviation;

      double step = (maxArea - minArea) / 800;

      List<Color> colorArray = new List<Color>();
      List<double> values = new List<double>();
      for (int i = 0; i < 800; i++)
      {
        double current = minArea + step * i;
        colorArray.Add(GenerateEnhancedColorFor(current, minArea, maxArea, averageArea));
        values.Add(current / averageArea - 1);
      }

      IllustrationForm bisectForm = new IllustrationForm();
      //using (IllustrationForm illustrationForm = new IllustrationForm())
      {
        bisectForm.colorArray = colorArray;
        bisectForm.values = values;
        bisectForm.fill = true;
        bisectForm.lines = false;
        bisectForm.Text = "Scale";
        bisectForm.Show();
      }
    }

    private void CompareHybridButton_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "*.csv|*.csv" })
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;

        List<string> result = new List<string>();
        result.Add("projection point generation, bisect generation, area min, area max, area average, length min, length max, length average,");

        int maxGeneration = Convert.ToInt32(GenerationBox.Text);

        for (int bisectGeneration = 1; bisectGeneration <= maxGeneration; bisectGeneration++)
          for (int generation = 1; generation <= bisectGeneration; generation++)
          {
            HybridGrid hybridGrid = new HybridGrid(generation, bisectGeneration);
            double minArea = 10;
            double maxArea = 0;
            double totalArea = 0;

            double lengthMin = 10;
            double lengthMax = 0;
            double lengthTotal = 0;
            //foreach (SphericalTriangle triangle in bisectGeodesic.SphericalTriangles)
            for (int i = 0; i < hybridGrid.TriangleCount; i++)
            {
              SphericalTriangle triangle = hybridGrid.GetTriangle(i);
              double area = triangle.Area;
              if (area < minArea)
                minArea = area;
              if (area > maxArea)
                maxArea = area;
              totalArea += area;
              double ab = (triangle.A - triangle.B).Magnitude;
              double bc = (triangle.B - triangle.C).Magnitude;
              double ca = (triangle.C - triangle.A).Magnitude;
              double maxLength = ab > bc ? ab > ca ? ab : ca : bc > ca ? bc : ca;
              double minLength = ab < bc ? ab < ca ? ab : ca : bc < ca ? bc : ca;
              if (maxLength > lengthMax)
                lengthMax = maxLength;
              if (minLength < lengthMin)
                lengthMin = minLength;
              lengthTotal += ab + bc + ca;
            }
            double averageArea = totalArea / hybridGrid.TriangleCount;
            double averageLength = lengthTotal / hybridGrid.TriangleCount / 3;

            result.Add(generation.ToString() + "," + bisectGeneration.ToString() + "," +
              minArea.ToString() + "," + maxArea.ToString() + "," + averageArea.ToString() + "," +
             lengthMin.ToString() + "," + lengthMax.ToString() + "," + averageLength.ToString() + ","
              );
            try
            {
              File.WriteAllLines(sfd.FileName + ".temp.csv", result);
            }
            catch
            {

            }
          }
        File.WriteAllLines(sfd.FileName, result);
      }
    }

    private void ComparePropertiesButton_Click(object sender, EventArgs e)
    {
      int generation = Convert.ToInt32(GenerationBox.Text);
      int hybridGeneration = Convert.ToInt32(HybridGenerationBox.Text);
      BisectGeodesicLowMemory bisectGeodesic = new BisectGeodesicLowMemory(generation);
      Geodesic geodesic = new Geodesic(generation - 2);
      HybridGrid hybridGrid = new HybridGrid(hybridGeneration, generation);


      Analyze bisectAngle = new Analyze();
      Analyze bisectLength = new Analyze();
      Analyze bisectArea = new Analyze();
      Analyze bisectOrthogonality = new Analyze();

      Analyze geodesicAngle = new Analyze();
      Analyze geodesicLength = new Analyze();
      Analyze geodesicArea = new Analyze();
      Analyze geodesicOrthogonality = new Analyze();

      Analyze hybridAngle = new Analyze();
      Analyze hybridLength = new Analyze();
      Analyze hybridArea = new Analyze();

      for (int i = 0; i < bisectGeodesic.TriangleCount; i++)
      {
        SphericalTriangle[] triangleWithNeighbours = bisectGeodesic.GetTriangleAndNeighbourTriangles(i);
        SphericalTriangle sphericalTriangle = triangleWithNeighbours[0];
        //do stuff with bisect triangle. 

        FlatTriangle triangle = new FlatTriangle(sphericalTriangle);

        double angleA = triangle.AB.AngleBetweenDegree(triangle.CA);
        double angleB = triangle.BC.AngleBetweenDegree(triangle.AB);
        double angleC = triangle.CA.AngleBetweenDegree(triangle.BC);

        double lengthAB = (triangle.A - triangle.B).Magnitude;
        double lengthBC = (triangle.B - triangle.C).Magnitude;
        double lengthCA = (triangle.C - triangle.A).Magnitude;

        bisectAngle.Add(angleA, angleB, angleC);
        bisectLength.Add(lengthAB, lengthBC, lengthCA);
        bisectArea.Add(triangle.Area);

        //do stuff with triangleneighbours. 
        double orthogonality = CalculateOrthogonality(triangleWithNeighbours);
        bisectOrthogonality.Add(orthogonality);
      }


      for (int i = 0; i < geodesic.MaxGridIndex; i++)
      {
        GridIndex index = geodesic.GetGridIndex(i);
        GeodesicGridTriangle geoTriangle = index.GeodesicGridTriangle;
        //do stuff with projection point triangle. 

        FlatTriangle triangle = new FlatTriangle(index);
        double angleA = triangle.AB.AngleBetweenDegree(triangle.CA);
        double angleB = triangle.BC.AngleBetweenDegree(triangle.AB);
        double angleC = triangle.CA.AngleBetweenDegree(triangle.BC);

        double lengthAB = (triangle.A - triangle.B).Magnitude;
        double lengthBC = (triangle.B - triangle.C).Magnitude;
        double lengthCA = (triangle.C - triangle.A).Magnitude;

        geodesicAngle.Add(angleA, angleB, angleC);
        geodesicLength.Add(lengthAB, lengthBC, lengthCA);
        geodesicArea.Add(triangle.Area);

        GridIndex[] neighbours = index.GetNeighbours();
        //do stuff with neighbours. 
        double orthogonality = CalculateOrthogonality(index, neighbours);
        geodesicOrthogonality.Add(orthogonality);
      }


      for (int i = 0; i < hybridGrid.TriangleCount; i++)
      {
        SphericalTriangle hybridTriangle = hybridGrid.GetTriangle(i);
        //do stuff with hybrid triangle. 

        FlatTriangle triangle = new FlatTriangle(hybridTriangle.A, hybridTriangle.B, hybridTriangle.C);
        double angleA = triangle.AB.AngleBetweenDegree(triangle.CA);
        double angleB = triangle.BC.AngleBetweenDegree(triangle.AB);
        double angleC = triangle.CA.AngleBetweenDegree(triangle.BC);

        double lengthAB = (triangle.A - triangle.B).Magnitude;
        double lengthBC = (triangle.B - triangle.C).Magnitude;
        double lengthCA = (triangle.C - triangle.A).Magnitude;

        hybridAngle.Add(angleA, angleB, angleC);
        hybridLength.Add(lengthAB, lengthBC, lengthCA);
        hybridArea.Add(triangle.Area);
      }


      MessageBox.Show("Bisect:\nLength:" + bisectLength.ToString() + "\n\nAngle:" + bisectAngle.ToString() + "\n\nArea" + bisectArea.ToString() +
        "\n\n\nGeodesic:\nLength:" + geodesicLength.ToString() + "\n\nAngle:" + geodesicAngle.ToString() + "\n\nArea" + geodesicArea.ToString() +
        "\n\n\nHybrid:\nLength:" + hybridLength.ToString() + "\n\nAngle:" + hybridAngle.ToString() + "\n\nArea" + hybridArea.ToString());

      MessageBox.Show("Bisect:\nOrthogonality:" + bisectOrthogonality.ToString() +
        "\n\n\nGeodesic:\nOrthogonality:" + geodesicOrthogonality.ToString());
    }

    private double CalculateOrthogonality(FlatTriangle triangle, FlatTriangle[] neighbours)
    {
      Minimum minimum = new Minimum();
      //checking validity
      if (neighbours.Length != 3)
        throw new Exception("Triangle should have 3 neighbours");
      int a = 0;
      int b = 0;
      int c = 0;
      for (int i = 0; i < 3; i++)
      {
        FlatTriangle neighbour = neighbours[i];
        if (triangle.A.Is(neighbour.A))
          a++;
        if (triangle.A.Is(neighbour.B))
          a++;
        if (triangle.A.Is(neighbour.C))
          a++;

        if (triangle.B.Is(neighbour.A))
          b++;
        if (triangle.B.Is(neighbour.B))
          b++;
        if (triangle.B.Is(neighbour.C))
          b++;

        if (triangle.C.Is(neighbour.A))
          c++;
        if (triangle.C.Is(neighbour.B))
          c++;
        if (triangle.C.Is(neighbour.C))
          c++;
      }
      if (a != 2 || b != 2 || c != 2)
        throw new Exception("Invalid triangle neighbours"); 


      //calculating actual orthogonality. 
      for (int i = 0; i < 3; i++)
        minimum.Add(triangle.Orthogonality(neighbours[i]));
      return minimum.min;
    }

		private double CalculateOrthogonality(SphericalTriangle[] triangleWithNeighbours)
		{
      FlatTriangle[] neighbours = new FlatTriangle[3];
      for (int i = 0; i < 3; i++)
        neighbours[i] = new FlatTriangle(triangleWithNeighbours[i + 1]);
      return CalculateOrthogonality(new FlatTriangle(triangleWithNeighbours[0]), neighbours); 
    }

		private double CalculateOrthogonality(GridIndex index, GridIndex[] neighbourIndices)
		{
      FlatTriangle triangle = new FlatTriangle(index);
      FlatTriangle[] neighbours = new FlatTriangle[3];
      for (int i = 0; i < 3; i++)
        neighbours[i] = new FlatTriangle(neighbourIndices[i]);
      return CalculateOrthogonality(triangle, neighbours); 
		}

    private void NewSystemButton_Click(object sender, EventArgs e)
    {
      using (Geo.TestForm form = new Geo.TestForm())
        form.ShowDialog(); 

    }
  }

  public class Variance
  {
    public double min;
    public double max;
    public double average;
    public double variance;
    public double lengthMin;
    public double lengthMax;
    public double lengthAverage;
  }
}
 