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

namespace Geo
{
  public partial class TestForm : Form
  {
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
  }
}
