namespace Geodesic.Drawing
{
  partial class IllustrationForm
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
      this.PictureBox = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // PictureBox
      // 
      this.PictureBox.Location = new System.Drawing.Point(12, 12);
      this.PictureBox.Name = "PictureBox";
      this.PictureBox.Size = new System.Drawing.Size(800, 800);
      this.PictureBox.TabIndex = 0;
      this.PictureBox.TabStop = false;
      // 
      // IllustrationForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(816, 813);
      this.Controls.Add(this.PictureBox);
      this.Name = "IllustrationForm";
      this.Text = "IllustrationForm";
      this.Shown += new System.EventHandler(this.IllustrationForm_Shown);
      ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
      this.ResumeLayout(false);

    }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox;
    }
}