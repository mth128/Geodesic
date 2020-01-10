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
      Vector3D a = new Vector3D(0.5, 0, 0.2);
      Vector3D b = a.RotateTop120();
      Vector3D c = b.RotateTop120();
      Vector3D d = c.RotateTop120(); 
    }
  }
}
