
namespace Geo.UI
{
  partial class TestForm
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
      this.ValueBox = new System.Windows.Forms.TextBox();
      this.AnswerBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.GenerationBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.PointIndexBox = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.CoordinateBox = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.BisectGenerationBox = new System.Windows.Forms.TextBox();
      this.FullAnalysisButton = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.ShortBox = new System.Windows.Forms.CheckBox();
      this.label7 = new System.Windows.Forms.Label();
      this.FullAnalysisMaxGenerationBox = new System.Windows.Forms.TextBox();
      this.DrawButton = new System.Windows.Forms.Button();
      this.label8 = new System.Windows.Forms.Label();
      this.AreaButton = new System.Windows.Forms.RadioButton();
      this.MaxAngleButton = new System.Windows.Forms.RadioButton();
      this.MinAngleButton = new System.Windows.Forms.RadioButton();
      this.LengthButton = new System.Windows.Forms.RadioButton();
      this.OrthogonalityButton = new System.Windows.Forms.RadioButton();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(369, 79);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Save OBJ";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // ValueBox
      // 
      this.ValueBox.Location = new System.Drawing.Point(61, 16);
      this.ValueBox.Margin = new System.Windows.Forms.Padding(2);
      this.ValueBox.Name = "ValueBox";
      this.ValueBox.Size = new System.Drawing.Size(338, 20);
      this.ValueBox.TabIndex = 1;
      this.ValueBox.TextChanged += new System.EventHandler(this.ValueBox_TextChanged);
      // 
      // AnswerBox
      // 
      this.AnswerBox.Location = new System.Drawing.Point(61, 39);
      this.AnswerBox.Margin = new System.Windows.Forms.Padding(2);
      this.AnswerBox.Name = "AnswerBox";
      this.AnswerBox.ReadOnly = true;
      this.AnswerBox.Size = new System.Drawing.Size(338, 20);
      this.AnswerBox.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(4, 19);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(37, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Value:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(4, 42);
      this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(53, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Cut Point:";
      // 
      // GenerationBox
      // 
      this.GenerationBox.Location = new System.Drawing.Point(106, 27);
      this.GenerationBox.Name = "GenerationBox";
      this.GenerationBox.Size = new System.Drawing.Size(338, 20);
      this.GenerationBox.TabIndex = 5;
      this.GenerationBox.Text = "3";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 30);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(85, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Max Generation:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(408, 16);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(63, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Point Index:";
      // 
      // PointIndexBox
      // 
      this.PointIndexBox.Location = new System.Drawing.Point(477, 13);
      this.PointIndexBox.Name = "PointIndexBox";
      this.PointIndexBox.Size = new System.Drawing.Size(338, 20);
      this.PointIndexBox.TabIndex = 7;
      this.PointIndexBox.TextChanged += new System.EventHandler(this.PointIndexBox_TextChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(408, 42);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(61, 13);
      this.label5.TabIndex = 10;
      this.label5.Text = "Coordinate:";
      // 
      // CoordinateBox
      // 
      this.CoordinateBox.Location = new System.Drawing.Point(477, 39);
      this.CoordinateBox.Name = "CoordinateBox";
      this.CoordinateBox.ReadOnly = true;
      this.CoordinateBox.Size = new System.Drawing.Size(338, 20);
      this.CoordinateBox.TabIndex = 9;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 56);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(94, 13);
      this.label6.TabIndex = 13;
      this.label6.Text = "Bisect Generation:";
      // 
      // BisectGenerationBox
      // 
      this.BisectGenerationBox.Location = new System.Drawing.Point(106, 53);
      this.BisectGenerationBox.Name = "BisectGenerationBox";
      this.BisectGenerationBox.Size = new System.Drawing.Size(338, 20);
      this.BisectGenerationBox.TabIndex = 12;
      this.BisectGenerationBox.Text = "3";
      // 
      // FullAnalysisButton
      // 
      this.FullAnalysisButton.Location = new System.Drawing.Point(370, 53);
      this.FullAnalysisButton.Name = "FullAnalysisButton";
      this.FullAnalysisButton.Size = new System.Drawing.Size(75, 23);
      this.FullAnalysisButton.TabIndex = 14;
      this.FullAnalysisButton.Text = "Full Analysis";
      this.FullAnalysisButton.UseVisualStyleBackColor = true;
      this.FullAnalysisButton.Click += new System.EventHandler(this.FullAnalysisButton_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.ValueBox);
      this.groupBox1.Controls.Add(this.AnswerBox);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.PointIndexBox);
      this.groupBox1.Controls.Add(this.CoordinateBox);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(819, 69);
      this.groupBox1.TabIndex = 15;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Various";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.GenerationBox);
      this.groupBox2.Controls.Add(this.BisectGenerationBox);
      this.groupBox2.Controls.Add(this.button1);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Location = new System.Drawing.Point(12, 87);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(451, 110);
      this.groupBox2.TabIndex = 16;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Generate OBJ file";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.OrthogonalityButton);
      this.groupBox3.Controls.Add(this.LengthButton);
      this.groupBox3.Controls.Add(this.MinAngleButton);
      this.groupBox3.Controls.Add(this.MaxAngleButton);
      this.groupBox3.Controls.Add(this.AreaButton);
      this.groupBox3.Controls.Add(this.label8);
      this.groupBox3.Controls.Add(this.DrawButton);
      this.groupBox3.Controls.Add(this.ShortBox);
      this.groupBox3.Controls.Add(this.label7);
      this.groupBox3.Controls.Add(this.FullAnalysisMaxGenerationBox);
      this.groupBox3.Controls.Add(this.FullAnalysisButton);
      this.groupBox3.Location = new System.Drawing.Point(12, 203);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(451, 148);
      this.groupBox3.TabIndex = 17;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Length";
      // 
      // ShortBox
      // 
      this.ShortBox.AutoSize = true;
      this.ShortBox.Location = new System.Drawing.Point(284, 60);
      this.ShortBox.Name = "ShortBox";
      this.ShortBox.Size = new System.Drawing.Size(51, 17);
      this.ShortBox.TabIndex = 15;
      this.ShortBox.Text = "Short";
      this.ShortBox.UseVisualStyleBackColor = true;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(6, 30);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(85, 13);
      this.label7.TabIndex = 6;
      this.label7.Text = "Max Generation:";
      // 
      // FullAnalysisMaxGenerationBox
      // 
      this.FullAnalysisMaxGenerationBox.Location = new System.Drawing.Point(106, 27);
      this.FullAnalysisMaxGenerationBox.Name = "FullAnalysisMaxGenerationBox";
      this.FullAnalysisMaxGenerationBox.Size = new System.Drawing.Size(338, 20);
      this.FullAnalysisMaxGenerationBox.TabIndex = 5;
      this.FullAnalysisMaxGenerationBox.Text = "3";
      // 
      // DrawButton
      // 
      this.DrawButton.Location = new System.Drawing.Point(369, 119);
      this.DrawButton.Name = "DrawButton";
      this.DrawButton.Size = new System.Drawing.Size(75, 23);
      this.DrawButton.TabIndex = 16;
      this.DrawButton.Text = "Draw";
      this.DrawButton.UseVisualStyleBackColor = true;
      this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(7, 96);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(35, 13);
      this.label8.TabIndex = 17;
      this.label8.Text = "Draw:";
      // 
      // AreaButton
      // 
      this.AreaButton.AutoSize = true;
      this.AreaButton.Checked = true;
      this.AreaButton.Location = new System.Drawing.Point(48, 94);
      this.AreaButton.Name = "AreaButton";
      this.AreaButton.Size = new System.Drawing.Size(47, 17);
      this.AreaButton.TabIndex = 18;
      this.AreaButton.Text = "Area";
      this.AreaButton.UseVisualStyleBackColor = true;
      // 
      // MaxAngleButton
      // 
      this.MaxAngleButton.AutoSize = true;
      this.MaxAngleButton.Location = new System.Drawing.Point(101, 94);
      this.MaxAngleButton.Name = "MaxAngleButton";
      this.MaxAngleButton.Size = new System.Drawing.Size(75, 17);
      this.MaxAngleButton.TabIndex = 19;
      this.MaxAngleButton.Text = "Max Angle";
      this.MaxAngleButton.UseVisualStyleBackColor = true;
      // 
      // MinAngleButton
      // 
      this.MinAngleButton.AutoSize = true;
      this.MinAngleButton.Location = new System.Drawing.Point(182, 94);
      this.MinAngleButton.Name = "MinAngleButton";
      this.MinAngleButton.Size = new System.Drawing.Size(72, 17);
      this.MinAngleButton.TabIndex = 20;
      this.MinAngleButton.Text = "Min Angle";
      this.MinAngleButton.UseVisualStyleBackColor = true;
      // 
      // LengthButton
      // 
      this.LengthButton.AutoSize = true;
      this.LengthButton.Location = new System.Drawing.Point(260, 94);
      this.LengthButton.Name = "LengthButton";
      this.LengthButton.Size = new System.Drawing.Size(58, 17);
      this.LengthButton.TabIndex = 21;
      this.LengthButton.Text = "Length";
      this.LengthButton.UseVisualStyleBackColor = true;
      // 
      // OrthogonalityButton
      // 
      this.OrthogonalityButton.AutoSize = true;
      this.OrthogonalityButton.Location = new System.Drawing.Point(324, 94);
      this.OrthogonalityButton.Name = "OrthogonalityButton";
      this.OrthogonalityButton.Size = new System.Drawing.Size(87, 17);
      this.OrthogonalityButton.TabIndex = 22;
      this.OrthogonalityButton.Text = "Orthogonality";
      this.OrthogonalityButton.UseVisualStyleBackColor = true;
      // 
      // TestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(845, 377);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Name = "TestForm";
      this.Text = "Form1";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox ValueBox;
		private System.Windows.Forms.TextBox AnswerBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox GenerationBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox PointIndexBox;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox CoordinateBox;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox BisectGenerationBox;
    private System.Windows.Forms.Button FullAnalysisButton;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox FullAnalysisMaxGenerationBox;
    private System.Windows.Forms.CheckBox ShortBox;
    private System.Windows.Forms.Button DrawButton;
    private System.Windows.Forms.RadioButton LengthButton;
    private System.Windows.Forms.RadioButton MinAngleButton;
    private System.Windows.Forms.RadioButton MaxAngleButton;
    private System.Windows.Forms.RadioButton AreaButton;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.RadioButton OrthogonalityButton;
  }
}

