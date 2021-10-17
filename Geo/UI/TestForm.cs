using Geo.Analysis;
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

namespace Geo.UI
{
  public partial class TestForm : Form
  {
    private double maxGeneration; 
    private string fileName; 
    private ProgressBarForm progressBarForm;
    private bool done; 

    public TestForm()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      WriteObjFile(); 
    }

    private void DumpIcosahedron()
    { 

      List<string> debug = new List<string>();
      int generation = 0;
      GridParameters parameters = new GridParameters(generation);

      for (int i = 0; i < 12; i++)
      {
        Vector3D vector = Icosahedron.Points[i]; 
        string vLine = "v " + vector.X.ToString() + " " + vector.Y.ToString() + " " + vector.Z.ToString();
        debug.Add(vLine);
      }

      for (int i = 0; i < 20; i++)
      {
        IcosahedronTriangle triangle = Icosahedron.Triangles[i];

        int a = IndexOf(triangle.A, Icosahedron.Points)+1;
        int b = IndexOf(triangle.B, Icosahedron.Points)+1;
        int c = IndexOf(triangle.C, Icosahedron.Points)+1;


        string fLine = "f " + a.ToString() + " " + b.ToString() + " " + c.ToString();
        debug.Add(fLine);
      }

      System.IO.File.WriteAllLines("C:\\projecten\\debug1.obj", debug);
    }

    private int IndexOf(object v, IEnumerable<object> objects)
    {
      int index = 0;
      foreach (object o in objects)
      {
        if (v == o)
          return index;
        index++;
      }
      return -1; 
    }


    private void WriteObjFile()
    {
      try
      {
        string fileName; 
        using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "*.obj|*.obj" })
        {
          if (sfd.ShowDialog() != DialogResult.OK)
            return;
          fileName = sfd.FileName; 
        }
        
        int generation = Convert.ToInt32(GenerationBox.Text);
        int bisectGeneration = Convert.ToInt32(BisectGenerationBox.Text);
        if (bisectGeneration < generation)
          bisectGeneration = generation; 
        GridParameters parameters = new GridParameters(bisectGeneration);

        using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
          using (StreamWriter writer = new StreamWriter(fileStream))
          {
            for (int i = 0; i < parameters.PointCount; i++)
            {
              GridPoint gridPoint = new GridPoint(generation, bisectGeneration, i);
              Vector3D vector = gridPoint.Point;
              string vLine = "v " + vector.X.ToString() + " " + vector.Y.ToString() + " " + vector.Z.ToString();
              writer.WriteLine(vLine);
            }

            for (int i = 0; i < parameters.TriangleCount; i++)
            {
              TriangleIndex index = new TriangleIndex(parameters.Generation, i);
              PointIndex[] p = index.PointIndices;

              string fLine = "f " + (p[0].Index + 1).ToString() + " " + (p[1].Index + 1).ToString() + " " + (p[2].Index + 1).ToString();
              writer.WriteLine(fLine);
            }
          }
        }

        MessageBox.Show("Done!"); 
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message); 
      }
    }

    void test1()
    {
      List<string> debug = new List<string>();
      int generation = 8; 
      GridParameters parameters = new GridParameters(generation);
      for (int i = 0; i < parameters.TriangleCount; i++)
      {
        TriangleIndex index = new TriangleIndex(generation, i);
        PointIndex[] p = index.PointIndices;
        debug.Add(index.Index.ToString() + " " + p[0].ToString() + " " + p[1].ToString() + " " + p[2].ToString());
      }

      System.IO.File.WriteAllLines("C:\\projecten\\debug.txt", debug);

      Parallel.For(0, parameters.TriangleCount, i =>
      {
        TriangleIndex triangleIndex = new TriangleIndex(generation, i);
        TriangleIndex[] neighbours = triangleIndex.Neighbours;
        if (neighbours[0] == neighbours[1] || neighbours[1] == neighbours[2] || neighbours[2] == neighbours[0])
          throw new Exception("Fail");
        foreach (TriangleIndex neighbour in neighbours)
        {
          if (neighbour.Index == triangleIndex.Index)
            throw new Exception("Fail");
          bool found = false;
          foreach (TriangleIndex reverse in neighbour.Neighbours)
          {
            if (reverse.Index == triangleIndex.Index)
            {
              if (found)
                throw new Exception("Fail");
              found = true;
            }
          }
          if (!found)
            throw new Exception("Fail");
        }
      });

      MessageBox.Show("Done!"); 
    }

		private void ValueBox_TextChanged(object sender, EventArgs e)
		{
      try
      {
        double input = Convert.ToDouble(ValueBox.Text);
        Vector3D result = Paper.GetCutPoint(input);
        AnswerBox.Text = result.ToString(); 
      }
      catch (Exception ex)
      {
        AnswerBox.Text = ex.Message;
      }
		}

    private void PointIndexBox_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int generation = Convert.ToInt32(GenerationBox.Text);
        long index = Convert.ToInt64(PointIndexBox.Text);
        GridPoint gridPoint = new GridPoint(generation, index);
        CoordinateBox.Text = gridPoint.Point.ToString(); 
      }
      catch (Exception ex)
      {
        CoordinateBox.Text = ex.Message; 
      }
    }

    private void IterateButton_Click(object sender, EventArgs e)
    {
      try
      {

        int generation = Convert.ToInt32(GenerationBox.Text);
        GridParameters parameters = new GridParameters(generation);

        object locker = new object(); 
        int count = 0;
        Parallel.For(0, parameters.PointCount, i =>
         {
           GridPoint gridPoint = new GridPoint(parameters.Generation, i);
           Vector3D vector = gridPoint.Point;
           lock (locker)
             count++;
         });

        Parallel.For(0, parameters.TriangleCount, i =>
         {
           TriangleIndex index = new TriangleIndex(generation, i);
           PointIndex[] p = index.PointIndices;
           lock (locker)
             count++;
         });
        MessageBox.Show(count.ToString());
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void TestButton_Click(object sender, EventArgs e)
    {
      int generation = Convert.ToInt32(GenerationBox.Text);
      int bisectGeneration = Convert.ToInt32(BisectGenerationBox.Text);
      if (bisectGeneration < generation)
        bisectGeneration = generation;
      GridParameters parameters = new GridParameters(bisectGeneration);
      Analyze areaAnalysis = new Analyze();
      Analyze orthogonalityAnalysis = new Analyze(); 
      object locker = new object();

      Parallel.For(0, parameters.TileSize, i =>
      {
        FlatTriangle triangle = FlatTriangle.GetTriangle(generation,bisectGeneration,i);
        double area = triangle.Area;
        Maximum orthogonality = new Maximum();
        foreach (TriangleIndex neighbourIndex in new TriangleIndex(bisectGeneration, i).Neighbours)
        {
          FlatTriangle neighbour = FlatTriangle.GetTriangle(generation, bisectGeneration, neighbourIndex.Index);
          orthogonality.Add(triangle.Orthogonality(neighbour)); 
        }
        lock (locker)
        {
          areaAnalysis.Add(triangle.Area);
          orthogonalityAnalysis.Add(orthogonality.max);
        }
      });
      MessageBox.Show("Area: "+ areaAnalysis.ToString() + "\n\nOrthogonality: "+ orthogonalityAnalysis.ToString()); 

    }

    private void FullAnalysisButton_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "*.csv|*.csv" })
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;
        fileName = sfd.FileName;
      }
      progressBarForm = new ProgressBarForm();
      try
      {
        maxGeneration = Convert.ToDouble(FullAnalysisMaxGenerationBox.Text);

        progressBarForm.Show();
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        done = false;
        backgroundWorker.DoWork += FullAnalysis;
        backgroundWorker.RunWorkerAsync();
        while(!done)
          System.Threading.Thread.Sleep(1);
        backgroundWorker.Dispose();

      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message); 
      }
      progressBarForm.Close();
      progressBarForm.Dispose();
      progressBarForm = null;
    }

    private void FullAnalysis(object sender, DoWorkEventArgs e)
    {
      try
      {
        using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
          using (StreamWriter writer = new StreamWriter(fileStream))
          {
            writer.WriteLine("bisect;projection point;area max;area min;area avg;length max;length min;length avg;angle max;angle min;angle avg;ortho max;ortho min;ortho avg;");

            for (int bisectGeneration = 0; bisectGeneration <= maxGeneration; bisectGeneration++)
            {
              for (int projectionPointGeneration = 0; projectionPointGeneration <= bisectGeneration; projectionPointGeneration++)
              {
                progressBarForm?.SetMessage("Projection Point: " + projectionPointGeneration.ToString() +
                  ", Bisect: " + bisectGeneration.ToString() + ", Target: " + maxGeneration.ToString());

                Analyze areaAnalysis = new Analyze();
                Analyze lengthAnalysis = new Analyze();
                Analyze angleAnalysis = new Analyze();
                Analyze orthogonalityAnalysis = new Analyze();
                object locker = new object(); 
                GridParameters parameters = new GridParameters(projectionPointGeneration);
                int count = 0; 
                Parallel.For(0, parameters.TileSize, i =>
                {
                  FlatTriangle triangle = FlatTriangle.GetTriangle(projectionPointGeneration, bisectGeneration, i);
                  double area = triangle.Area;
                  Maximum orthogonality = new Maximum();
                  foreach (TriangleIndex neighbourIndex in new TriangleIndex(bisectGeneration, i).Neighbours)
                  {
                    FlatTriangle neighbour = FlatTriangle.GetTriangle(projectionPointGeneration, bisectGeneration, neighbourIndex.Index);
                    orthogonality.Add(triangle.Orthogonality(neighbour));
                  }

                  double[] lengths = triangle.Lengths; 
                  double[] angles = triangle.Angles;

                  lock (locker)
                  {
                    areaAnalysis.Add(triangle.Area);
                    orthogonalityAnalysis.Add(orthogonality.max);
                    foreach (double angle in angles)
                      angleAnalysis.Add(angle);
                    foreach (double length in lengths)
                      lengthAnalysis.Add(length);
                    progressBarForm?.SetProgress(count++, parameters.TileSize);
                  }
                });

                writer.WriteLine(bisectGeneration.ToString()+ ";"+projectionPointGeneration.ToString()+";"+areaAnalysis.ToCSVString() + lengthAnalysis.ToCSVString() + angleAnalysis.ToCSVString() + orthogonalityAnalysis.ToCSVString());
              }
            }
          }
        }

      }
      catch (Exception ex)
      {
        progressBarForm?.SetMessage(ex.Message);
      }

      done = true; 
    }
  }
}
