﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geodesic.Drawing
{
  public partial class IllustrationForm : Form
  {
    public float centerX = 460;
    public float centerY = 400;
    public float drawScale = 700;
    public List<DrawTriangle> triangles;
    public bool fill = false;
    public bool lines = true;
    internal List<Color> colorArray;
    internal List<double> values;

    public PointF PointTop(Vector3D vector)
    {
      float x = Convert.ToSingle(vector.X * drawScale) + centerX;
      float y = Convert.ToSingle(vector.Y * drawScale) + centerY;

      return new PointF(x, y); 
    }


    public IllustrationForm()
    {
      InitializeComponent();

    }

    public void DrawTop(List<DrawTriangle> triangles)
    {
      Bitmap bitmap = new Bitmap(PictureBox.Width, PictureBox.Height);

      //using (Bitmap bitmap = new Bitmap(PictureBox.Width, PictureBox.Height))
      {
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
          graphics.Clear(Color.White);

          foreach (DrawTriangle triangle in triangles)
          {
            PointF[] points = new PointF[]
              {
                PointTop(triangle.point1),
                PointTop(triangle.point2),
                PointTop(triangle.point3),
              };

            if (fill)
              using (Brush brush = new SolidBrush(triangle.fillColor))
                graphics.FillPolygon(brush, points);

            if (lines)
              using (Pen pen = new Pen(triangle.lineColor))
                graphics.DrawPolygon(pen, points);

          }
        }
        PictureBox.Image = bitmap; 
      }
    
    }

    private void IllustrationForm_Shown(object sender, EventArgs e)
    {
      if (triangles != null)
        DrawTop(triangles);
      if (colorArray != null)
        DrawLines(colorArray,values); 
    }

    private void DrawLines(List<Color> colorArray, List<double> scaleValues = null)
    {
      if (scaleValues != null)
        PictureBox.Width = 200; 
      Bitmap bitmap = new Bitmap(PictureBox.Width, PictureBox.Height);

      //using (Bitmap bitmap = new Bitmap(PictureBox.Width, PictureBox.Height))
      {
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
          graphics.Clear(Color.White);

          for (int i = 0; i < colorArray.Count; i++)
          {
            using (Pen pen = new Pen(colorArray[i]))
            {
              graphics.DrawLine(pen, new Point(0, i), new Point(100, i));
            }
          }
          if (scaleValues!=null && scaleValues.Count == colorArray.Count && scaleValues.Count!=0)
          {
            double first = scaleValues[0];
            double last = scaleValues.Last();

            double range = last - first;
            double step = 0.01;

            double nextStep = Math.Round(first / step, 0) * step;
            nextStep += step;
            int fontSize = 9; 
            using (Brush brush = new SolidBrush(Color.Black))
            {
              using (Font font = new Font("Arial", fontSize))
              {
                for (int i = 0; i < scaleValues.Count; i++)
                {
                  double value = scaleValues[i]; 
                  if (value>=nextStep)
                  {
                    using (Pen pen = new Pen(Color.Black))
                    {
                      graphics.DrawLine(pen, new Point(110, i), new Point(140, i));
                    }
                
                    double percentage = Math.Round((nextStep) * 100, 1);
                    string text = percentage.ToString() + "%";
                    graphics.DrawString(text, font, brush, new Point(150, i-fontSize/2));

                    nextStep += step;
                  }
                }
              }


            }


          }


        }
        PictureBox.Image = bitmap;
      }

    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "*.png|*.png" })
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;
        PictureBox.Image.Save(sfd.FileName, ImageFormat.Png); 
      }
    }
  }
}
