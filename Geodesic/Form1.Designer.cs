namespace Geodesic
{
  partial class Form1
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
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.button5 = new System.Windows.Forms.Button();
      this.VarianceLabel = new System.Windows.Forms.Label();
      this.VarianceBox = new System.Windows.Forms.TextBox();
      this.UpButton = new System.Windows.Forms.Button();
      this.DownButton = new System.Windows.Forms.Button();
      this.ShiftBox = new System.Windows.Forms.TextBox();
      this.DownShiftBox = new System.Windows.Forms.Button();
      this.UpShiftBox = new System.Windows.Forms.Button();
      this.button6 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.GenerationBox = new System.Windows.Forms.TextBox();
      this.button7 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(12, 12);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.Button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(205, 50);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(91, 23);
      this.button2.TabIndex = 1;
      this.button2.Text = "New Geodesic";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.Button2_Click);
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(174, 12);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(75, 23);
      this.button3.TabIndex = 2;
      this.button3.Text = "button3";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.Button3_Click);
      // 
      // button4
      // 
      this.button4.Location = new System.Drawing.Point(255, 12);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(75, 23);
      this.button4.TabIndex = 3;
      this.button4.Text = "button4";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new System.EventHandler(this.Button4_Click);
      // 
      // button5
      // 
      this.button5.Location = new System.Drawing.Point(336, 12);
      this.button5.Name = "button5";
      this.button5.Size = new System.Drawing.Size(75, 23);
      this.button5.TabIndex = 4;
      this.button5.Text = "button5";
      this.button5.UseVisualStyleBackColor = true;
      this.button5.Click += new System.EventHandler(this.Button5_Click);
      // 
      // VarianceLabel
      // 
      this.VarianceLabel.AutoSize = true;
      this.VarianceLabel.Location = new System.Drawing.Point(202, 95);
      this.VarianceLabel.Name = "VarianceLabel";
      this.VarianceLabel.Size = new System.Drawing.Size(35, 13);
      this.VarianceLabel.TabIndex = 5;
      this.VarianceLabel.Text = "label1";
      // 
      // VarianceBox
      // 
      this.VarianceBox.Location = new System.Drawing.Point(12, 88);
      this.VarianceBox.Name = "VarianceBox";
      this.VarianceBox.Size = new System.Drawing.Size(100, 20);
      this.VarianceBox.TabIndex = 6;
      this.VarianceBox.Text = "0.992866587834743";
      this.VarianceBox.TextChanged += new System.EventHandler(this.VarianceBox_TextChanged);
      // 
      // UpButton
      // 
      this.UpButton.Location = new System.Drawing.Point(13, 115);
      this.UpButton.Name = "UpButton";
      this.UpButton.Size = new System.Drawing.Size(52, 23);
      this.UpButton.TabIndex = 7;
      this.UpButton.Text = "+";
      this.UpButton.UseVisualStyleBackColor = true;
      this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
      // 
      // DownButton
      // 
      this.DownButton.Location = new System.Drawing.Point(71, 115);
      this.DownButton.Name = "DownButton";
      this.DownButton.Size = new System.Drawing.Size(41, 23);
      this.DownButton.TabIndex = 8;
      this.DownButton.Text = "-";
      this.DownButton.UseVisualStyleBackColor = true;
      this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
      // 
      // ShiftBox
      // 
      this.ShiftBox.Location = new System.Drawing.Point(12, 161);
      this.ShiftBox.Name = "ShiftBox";
      this.ShiftBox.Size = new System.Drawing.Size(100, 20);
      this.ShiftBox.TabIndex = 9;
      this.ShiftBox.Text = "0.5";
      // 
      // DownShiftBox
      // 
      this.DownShiftBox.Location = new System.Drawing.Point(70, 187);
      this.DownShiftBox.Name = "DownShiftBox";
      this.DownShiftBox.Size = new System.Drawing.Size(41, 23);
      this.DownShiftBox.TabIndex = 11;
      this.DownShiftBox.Text = "-";
      this.DownShiftBox.UseVisualStyleBackColor = true;
      this.DownShiftBox.Click += new System.EventHandler(this.DownShiftBox_Click);
      // 
      // UpShiftBox
      // 
      this.UpShiftBox.Location = new System.Drawing.Point(12, 187);
      this.UpShiftBox.Name = "UpShiftBox";
      this.UpShiftBox.Size = new System.Drawing.Size(52, 23);
      this.UpShiftBox.TabIndex = 10;
      this.UpShiftBox.Text = "+";
      this.UpShiftBox.UseVisualStyleBackColor = true;
      this.UpShiftBox.Click += new System.EventHandler(this.UpShiftBox_Click);
      // 
      // button6
      // 
      this.button6.Location = new System.Drawing.Point(316, 51);
      this.button6.Name = "button6";
      this.button6.Size = new System.Drawing.Size(121, 23);
      this.button6.TabIndex = 12;
      this.button6.Text = "OldGeodesic";
      this.button6.UseVisualStyleBackColor = true;
      this.button6.Click += new System.EventHandler(this.Button6_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(144, 95);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(52, 13);
      this.label1.TabIndex = 13;
      this.label1.Text = "Variance:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 56);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(59, 13);
      this.label2.TabIndex = 14;
      this.label2.Text = "Generation";
      // 
      // GenerationBox
      // 
      this.GenerationBox.Location = new System.Drawing.Point(78, 53);
      this.GenerationBox.Name = "GenerationBox";
      this.GenerationBox.Size = new System.Drawing.Size(100, 20);
      this.GenerationBox.TabIndex = 15;
      this.GenerationBox.Text = "5";
      // 
      // button7
      // 
      this.button7.Location = new System.Drawing.Point(517, 12);
      this.button7.Name = "button7";
      this.button7.Size = new System.Drawing.Size(75, 23);
      this.button7.TabIndex = 16;
      this.button7.Text = "Sigma";
      this.button7.UseVisualStyleBackColor = true;
      this.button7.Click += new System.EventHandler(this.Button7_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.button7);
      this.Controls.Add(this.GenerationBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.button6);
      this.Controls.Add(this.DownShiftBox);
      this.Controls.Add(this.UpShiftBox);
      this.Controls.Add(this.ShiftBox);
      this.Controls.Add(this.DownButton);
      this.Controls.Add(this.UpButton);
      this.Controls.Add(this.VarianceBox);
      this.Controls.Add(this.VarianceLabel);
      this.Controls.Add(this.button5);
      this.Controls.Add(this.button4);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Label VarianceLabel;
    private System.Windows.Forms.TextBox VarianceBox;
    private System.Windows.Forms.Button UpButton;
    private System.Windows.Forms.Button DownButton;
    private System.Windows.Forms.TextBox ShiftBox;
    private System.Windows.Forms.Button DownShiftBox;
    private System.Windows.Forms.Button UpShiftBox;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox GenerationBox;
    private System.Windows.Forms.Button button7;
  }
}

