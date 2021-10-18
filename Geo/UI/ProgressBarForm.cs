using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geo.UI
{
  public partial class ProgressBarForm : Form
  {
    private string message; 
    private long value;
    private long maximum;
    private bool done = false; 
    public ProgressBarForm()
    {
      InitializeComponent();
    }
    public void SetProgress(long value, long maximum)
    {
      this.value = value;
      this.maximum = maximum; 
    }
    public void SetMessage(string message)
    {
      this.message = message; 
    }
    public void Done()
    {
      done = true; 
    }
    private void UpdateTimer_Tick(object sender, EventArgs e)
    {
      int limit = 16777216;
      double vDouble = value * limit;
      if (maximum < 1)
        maximum = 1; 
      int val = Convert.ToInt32(vDouble / maximum); 

      Label.Text = message;
      ProgressBar.Minimum = 0; 
      ProgressBar.Maximum = limit;
      ProgressBar.Value = val;
      if (done)
        Close(); 
    }
  }
}
