
namespace Geo.UI
{
  partial class ProgressBarForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.ProgressBar = new System.Windows.Forms.ProgressBar();
      this.Label = new System.Windows.Forms.Label();
      this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // ProgressBar
      // 
      this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ProgressBar.Location = new System.Drawing.Point(12, 12);
      this.ProgressBar.Name = "ProgressBar";
      this.ProgressBar.Size = new System.Drawing.Size(995, 23);
      this.ProgressBar.TabIndex = 0;
      // 
      // Label
      // 
      this.Label.AutoSize = true;
      this.Label.Location = new System.Drawing.Point(12, 38);
      this.Label.Name = "Label";
      this.Label.Size = new System.Drawing.Size(28, 13);
      this.Label.TabIndex = 1;
      this.Label.Text = "Text";
      // 
      // UpdateTimer
      // 
      this.UpdateTimer.Enabled = true;
      this.UpdateTimer.Interval = 1;
      this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
      // 
      // ProgressBarForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1019, 65);
      this.ControlBox = false;
      this.Controls.Add(this.Label);
      this.Controls.Add(this.ProgressBar);
      this.Name = "ProgressBarForm";
      this.ShowIcon = false;
      this.Text = "Progress";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar ProgressBar;
    private System.Windows.Forms.Label Label;
    private System.Windows.Forms.Timer UpdateTimer;
  }
}