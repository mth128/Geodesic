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
      this.label2 = new System.Windows.Forms.Label();
      this.GenerationBox = new System.Windows.Forms.TextBox();
      this.GeodesicAnalysisButton = new System.Windows.Forms.Button();
      this.GetByIndexButton = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.IndexBox = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.YBox = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.XBox = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.RangeBox = new System.Windows.Forms.TextBox();
      this.RangePositionButton = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.LowerXBox = new System.Windows.Forms.TextBox();
      this.UpperXBox = new System.Windows.Forms.TextBox();
      this.LowerPositionBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.UpperPositionBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.PositionBox = new System.Windows.Forms.TextBox();
      this.GetByPositionButton = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.VarianceButton = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.LowerRangeBox = new System.Windows.Forms.TextBox();
      this.UpperRangeBox = new System.Windows.Forms.TextBox();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(10, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(59, 13);
      this.label2.TabIndex = 14;
      this.label2.Text = "Generation";
      // 
      // GenerationBox
      // 
      this.GenerationBox.Location = new System.Drawing.Point(75, 6);
      this.GenerationBox.Name = "GenerationBox";
      this.GenerationBox.Size = new System.Drawing.Size(100, 20);
      this.GenerationBox.TabIndex = 15;
      this.GenerationBox.Text = "5";
      // 
      // GeodesicAnalysisButton
      // 
      this.GeodesicAnalysisButton.Location = new System.Drawing.Point(12, 32);
      this.GeodesicAnalysisButton.Name = "GeodesicAnalysisButton";
      this.GeodesicAnalysisButton.Size = new System.Drawing.Size(283, 23);
      this.GeodesicAnalysisButton.TabIndex = 34;
      this.GeodesicAnalysisButton.Text = "Simple Geodesic Analyse Distance To Scaled Centerline";
      this.GeodesicAnalysisButton.UseVisualStyleBackColor = true;
      this.GeodesicAnalysisButton.Click += new System.EventHandler(this.GeodesicAnalysisButton_Click);
      // 
      // GetByIndexButton
      // 
      this.GetByIndexButton.Location = new System.Drawing.Point(94, 45);
      this.GetByIndexButton.Name = "GetByIndexButton";
      this.GetByIndexButton.Size = new System.Drawing.Size(100, 23);
      this.GetByIndexButton.TabIndex = 35;
      this.GetByIndexButton.Text = "Get By Index";
      this.GetByIndexButton.UseVisualStyleBackColor = true;
      this.GetByIndexButton.Click += new System.EventHandler(this.GetByIndexButton_Click);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(8, 22);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(36, 13);
      this.label7.TabIndex = 36;
      this.label7.Text = "Index:";
      // 
      // IndexBox
      // 
      this.IndexBox.Location = new System.Drawing.Point(94, 19);
      this.IndexBox.Name = "IndexBox";
      this.IndexBox.Size = new System.Drawing.Size(100, 20);
      this.IndexBox.TabIndex = 37;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.YBox);
      this.groupBox1.Controls.Add(this.label9);
      this.groupBox1.Controls.Add(this.XBox);
      this.groupBox1.Controls.Add(this.label8);
      this.groupBox1.Controls.Add(this.IndexBox);
      this.groupBox1.Controls.Add(this.GetByIndexButton);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Location = new System.Drawing.Point(12, 61);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(200, 126);
      this.groupBox1.TabIndex = 38;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "groupBox1";
      // 
      // YBox
      // 
      this.YBox.Location = new System.Drawing.Point(94, 99);
      this.YBox.Name = "YBox";
      this.YBox.Size = new System.Drawing.Size(100, 20);
      this.YBox.TabIndex = 41;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(8, 102);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(17, 13);
      this.label9.TabIndex = 40;
      this.label9.Text = "Y:";
      // 
      // XBox
      // 
      this.XBox.Location = new System.Drawing.Point(94, 73);
      this.XBox.Name = "XBox";
      this.XBox.Size = new System.Drawing.Size(100, 20);
      this.XBox.TabIndex = 39;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(8, 76);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(17, 13);
      this.label8.TabIndex = 38;
      this.label8.Text = "X:";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label11);
      this.groupBox2.Controls.Add(this.label12);
      this.groupBox2.Controls.Add(this.LowerRangeBox);
      this.groupBox2.Controls.Add(this.UpperRangeBox);
      this.groupBox2.Controls.Add(this.label10);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Controls.Add(this.RangeBox);
      this.groupBox2.Controls.Add(this.RangePositionButton);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.LowerXBox);
      this.groupBox2.Controls.Add(this.UpperXBox);
      this.groupBox2.Controls.Add(this.LowerPositionBox);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this.UpperPositionBox);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.PositionBox);
      this.groupBox2.Controls.Add(this.GetByPositionButton);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Location = new System.Drawing.Point(12, 193);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(498, 126);
      this.groupBox2.TabIndex = 42;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "groupBox2";
      // 
      // RangeBox
      // 
      this.RangeBox.Location = new System.Drawing.Point(79, 46);
      this.RangeBox.Name = "RangeBox";
      this.RangeBox.Size = new System.Drawing.Size(100, 20);
      this.RangeBox.TabIndex = 46;
      // 
      // RangePositionButton
      // 
      this.RangePositionButton.Location = new System.Drawing.Point(200, 44);
      this.RangePositionButton.Name = "RangePositionButton";
      this.RangePositionButton.Size = new System.Drawing.Size(137, 23);
      this.RangePositionButton.TabIndex = 44;
      this.RangePositionButton.Text = "Get By Range Position";
      this.RangePositionButton.UseVisualStyleBackColor = true;
      this.RangePositionButton.Click += new System.EventHandler(this.RangePositionButton_Click);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(8, 49);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(72, 13);
      this.label5.TabIndex = 45;
      this.label5.Text = "Range (0 - 1):";
      // 
      // LowerXBox
      // 
      this.LowerXBox.Location = new System.Drawing.Point(200, 99);
      this.LowerXBox.Name = "LowerXBox";
      this.LowerXBox.Size = new System.Drawing.Size(137, 20);
      this.LowerXBox.TabIndex = 43;
      // 
      // UpperXBox
      // 
      this.UpperXBox.Location = new System.Drawing.Point(200, 73);
      this.UpperXBox.Name = "UpperXBox";
      this.UpperXBox.Size = new System.Drawing.Size(137, 20);
      this.UpperXBox.TabIndex = 42;
      // 
      // LowerPositionBox
      // 
      this.LowerPositionBox.Location = new System.Drawing.Point(79, 99);
      this.LowerPositionBox.Name = "LowerPositionBox";
      this.LowerPositionBox.Size = new System.Drawing.Size(100, 20);
      this.LowerPositionBox.TabIndex = 41;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(8, 102);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(39, 13);
      this.label1.TabIndex = 40;
      this.label1.Text = "Lower:";
      // 
      // UpperPositionBox
      // 
      this.UpperPositionBox.Location = new System.Drawing.Point(79, 73);
      this.UpperPositionBox.Name = "UpperPositionBox";
      this.UpperPositionBox.Size = new System.Drawing.Size(100, 20);
      this.UpperPositionBox.TabIndex = 39;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(8, 76);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(39, 13);
      this.label3.TabIndex = 38;
      this.label3.Text = "Upper:";
      // 
      // PositionBox
      // 
      this.PositionBox.Location = new System.Drawing.Point(79, 19);
      this.PositionBox.Name = "PositionBox";
      this.PositionBox.Size = new System.Drawing.Size(100, 20);
      this.PositionBox.TabIndex = 37;
      // 
      // GetByPositionButton
      // 
      this.GetByPositionButton.Location = new System.Drawing.Point(200, 17);
      this.GetByPositionButton.Name = "GetByPositionButton";
      this.GetByPositionButton.Size = new System.Drawing.Size(137, 23);
      this.GetByPositionButton.TabIndex = 35;
      this.GetByPositionButton.Text = "Get By X Position";
      this.GetByPositionButton.UseVisualStyleBackColor = true;
      this.GetByPositionButton.Click += new System.EventHandler(this.GetByPositionButton_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(8, 22);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(56, 13);
      this.label4.TabIndex = 36;
      this.label4.Text = "X position:";
      // 
      // VarianceButton
      // 
      this.VarianceButton.Location = new System.Drawing.Point(194, 4);
      this.VarianceButton.Name = "VarianceButton";
      this.VarianceButton.Size = new System.Drawing.Size(173, 23);
      this.VarianceButton.TabIndex = 43;
      this.VarianceButton.Text = "Calculate Area Variance";
      this.VarianceButton.UseVisualStyleBackColor = true;
      this.VarianceButton.Click += new System.EventHandler(this.VarianceButton_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(179, 76);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(17, 13);
      this.label6.TabIndex = 47;
      this.label6.Text = "X:";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(179, 102);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(17, 13);
      this.label10.TabIndex = 48;
      this.label10.Text = "X:";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(343, 102);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(18, 13);
      this.label11.TabIndex = 52;
      this.label11.Text = "R:";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(343, 76);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(18, 13);
      this.label12.TabIndex = 51;
      this.label12.Text = "R:";
      // 
      // LowerRangeBox
      // 
      this.LowerRangeBox.Location = new System.Drawing.Point(364, 99);
      this.LowerRangeBox.Name = "LowerRangeBox";
      this.LowerRangeBox.Size = new System.Drawing.Size(128, 20);
      this.LowerRangeBox.TabIndex = 50;
      // 
      // UpperRangeBox
      // 
      this.UpperRangeBox.Location = new System.Drawing.Point(364, 73);
      this.UpperRangeBox.Name = "UpperRangeBox";
      this.UpperRangeBox.Size = new System.Drawing.Size(128, 20);
      this.UpperRangeBox.TabIndex = 49;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(522, 325);
      this.Controls.Add(this.VarianceButton);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.GeodesicAnalysisButton);
      this.Controls.Add(this.GenerationBox);
      this.Controls.Add(this.label2);
      this.Name = "Form1";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox GenerationBox;
    private System.Windows.Forms.Button GeodesicAnalysisButton;
    private System.Windows.Forms.Button GetByIndexButton;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox IndexBox;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox YBox;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox XBox;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox LowerPositionBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox UpperPositionBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox PositionBox;
    private System.Windows.Forms.Button GetByPositionButton;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button VarianceButton;
    private System.Windows.Forms.TextBox LowerXBox;
    private System.Windows.Forms.TextBox UpperXBox;
    private System.Windows.Forms.TextBox RangeBox;
    private System.Windows.Forms.Button RangePositionButton;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox LowerRangeBox;
    private System.Windows.Forms.TextBox UpperRangeBox;
  }
}

