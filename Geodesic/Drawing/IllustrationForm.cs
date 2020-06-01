using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public float scale = 700;
    public List<DrawTriangle> triangles;
    public bool fill = false;
    public bool lines = true;
    internal List<Color> colorArray;

    public PointF PointTop(Vector3D vector)
    {
      float x = Convert.ToSingle(vector.X * scale) + centerX;
      float y = Convert.ToSingle(vector.Y * scale) + centerY;

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
        DrawLines(colorArray); 
    }

    private void DrawLines(List<Color> colorArray)
    {
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
              graphics.DrawLine(pen, new Point(0, i), new Point(PictureBox.Width, i));
            }
          }
        }
        PictureBox.Image = bitmap;
      }

    }
  }
}
