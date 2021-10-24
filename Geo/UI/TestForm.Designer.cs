
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
			this.OrthogonalityButton = new System.Windows.Forms.RadioButton();
			this.LengthButton = new System.Windows.Forms.RadioButton();
			this.MinAngleButton = new System.Windows.Forms.RadioButton();
			this.MaxAngleButton = new System.Windows.Forms.RadioButton();
			this.AreaButton = new System.Windows.Forms.RadioButton();
			this.label8 = new System.Windows.Forms.Label();
			this.DrawButton = new System.Windows.Forms.Button();
			this.ShortBox = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.FullAnalysisMaxGenerationBox = new System.Windows.Forms.TextBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.ShiftPButton = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.PShiftBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.ShiftPGenerationBox = new System.Windows.Forms.TextBox();
			this.TestIndexedButton = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(492, 97);
			this.button1.Margin = new System.Windows.Forms.Padding(4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(100, 28);
			this.button1.TabIndex = 0;
			this.button1.Text = "Save OBJ";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// ValueBox
			// 
			this.ValueBox.Location = new System.Drawing.Point(81, 20);
			this.ValueBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ValueBox.Name = "ValueBox";
			this.ValueBox.Size = new System.Drawing.Size(449, 22);
			this.ValueBox.TabIndex = 1;
			this.ValueBox.TextChanged += new System.EventHandler(this.ValueBox_TextChanged);
			// 
			// AnswerBox
			// 
			this.AnswerBox.Location = new System.Drawing.Point(81, 48);
			this.AnswerBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.AnswerBox.Name = "AnswerBox";
			this.AnswerBox.ReadOnly = true;
			this.AnswerBox.Size = new System.Drawing.Size(449, 22);
			this.AnswerBox.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "Value:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "Cut Point:";
			// 
			// GenerationBox
			// 
			this.GenerationBox.Location = new System.Drawing.Point(141, 33);
			this.GenerationBox.Margin = new System.Windows.Forms.Padding(4);
			this.GenerationBox.Name = "GenerationBox";
			this.GenerationBox.Size = new System.Drawing.Size(449, 22);
			this.GenerationBox.TabIndex = 5;
			this.GenerationBox.Text = "3";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 37);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 17);
			this.label3.TabIndex = 6;
			this.label3.Text = "Max Generation:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(544, 20);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 17);
			this.label4.TabIndex = 8;
			this.label4.Text = "Point Index:";
			// 
			// PointIndexBox
			// 
			this.PointIndexBox.Location = new System.Drawing.Point(636, 16);
			this.PointIndexBox.Margin = new System.Windows.Forms.Padding(4);
			this.PointIndexBox.Name = "PointIndexBox";
			this.PointIndexBox.Size = new System.Drawing.Size(449, 22);
			this.PointIndexBox.TabIndex = 7;
			this.PointIndexBox.TextChanged += new System.EventHandler(this.PointIndexBox_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(544, 52);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(81, 17);
			this.label5.TabIndex = 10;
			this.label5.Text = "Coordinate:";
			// 
			// CoordinateBox
			// 
			this.CoordinateBox.Location = new System.Drawing.Point(636, 48);
			this.CoordinateBox.Margin = new System.Windows.Forms.Padding(4);
			this.CoordinateBox.Name = "CoordinateBox";
			this.CoordinateBox.ReadOnly = true;
			this.CoordinateBox.Size = new System.Drawing.Size(449, 22);
			this.CoordinateBox.TabIndex = 9;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 69);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(125, 17);
			this.label6.TabIndex = 13;
			this.label6.Text = "Bisect Generation:";
			// 
			// BisectGenerationBox
			// 
			this.BisectGenerationBox.Location = new System.Drawing.Point(141, 65);
			this.BisectGenerationBox.Margin = new System.Windows.Forms.Padding(4);
			this.BisectGenerationBox.Name = "BisectGenerationBox";
			this.BisectGenerationBox.Size = new System.Drawing.Size(449, 22);
			this.BisectGenerationBox.TabIndex = 12;
			this.BisectGenerationBox.Text = "3";
			// 
			// FullAnalysisButton
			// 
			this.FullAnalysisButton.Location = new System.Drawing.Point(493, 65);
			this.FullAnalysisButton.Margin = new System.Windows.Forms.Padding(4);
			this.FullAnalysisButton.Name = "FullAnalysisButton";
			this.FullAnalysisButton.Size = new System.Drawing.Size(100, 28);
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
			this.groupBox1.Location = new System.Drawing.Point(16, 15);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(1092, 85);
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
			this.groupBox2.Location = new System.Drawing.Point(16, 107);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox2.Size = new System.Drawing.Size(601, 135);
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
			this.groupBox3.Location = new System.Drawing.Point(16, 250);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox3.Size = new System.Drawing.Size(601, 182);
			this.groupBox3.TabIndex = 17;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Length";
			// 
			// OrthogonalityButton
			// 
			this.OrthogonalityButton.AutoSize = true;
			this.OrthogonalityButton.Location = new System.Drawing.Point(432, 116);
			this.OrthogonalityButton.Margin = new System.Windows.Forms.Padding(4);
			this.OrthogonalityButton.Name = "OrthogonalityButton";
			this.OrthogonalityButton.Size = new System.Drawing.Size(114, 21);
			this.OrthogonalityButton.TabIndex = 22;
			this.OrthogonalityButton.Text = "Orthogonality";
			this.OrthogonalityButton.UseVisualStyleBackColor = true;
			// 
			// LengthButton
			// 
			this.LengthButton.AutoSize = true;
			this.LengthButton.Location = new System.Drawing.Point(347, 116);
			this.LengthButton.Margin = new System.Windows.Forms.Padding(4);
			this.LengthButton.Name = "LengthButton";
			this.LengthButton.Size = new System.Drawing.Size(73, 21);
			this.LengthButton.TabIndex = 21;
			this.LengthButton.Text = "Length";
			this.LengthButton.UseVisualStyleBackColor = true;
			// 
			// MinAngleButton
			// 
			this.MinAngleButton.AutoSize = true;
			this.MinAngleButton.Location = new System.Drawing.Point(243, 116);
			this.MinAngleButton.Margin = new System.Windows.Forms.Padding(4);
			this.MinAngleButton.Name = "MinAngleButton";
			this.MinAngleButton.Size = new System.Drawing.Size(91, 21);
			this.MinAngleButton.TabIndex = 20;
			this.MinAngleButton.Text = "Min Angle";
			this.MinAngleButton.UseVisualStyleBackColor = true;
			// 
			// MaxAngleButton
			// 
			this.MaxAngleButton.AutoSize = true;
			this.MaxAngleButton.Location = new System.Drawing.Point(135, 116);
			this.MaxAngleButton.Margin = new System.Windows.Forms.Padding(4);
			this.MaxAngleButton.Name = "MaxAngleButton";
			this.MaxAngleButton.Size = new System.Drawing.Size(94, 21);
			this.MaxAngleButton.TabIndex = 19;
			this.MaxAngleButton.Text = "Max Angle";
			this.MaxAngleButton.UseVisualStyleBackColor = true;
			// 
			// AreaButton
			// 
			this.AreaButton.AutoSize = true;
			this.AreaButton.Checked = true;
			this.AreaButton.Location = new System.Drawing.Point(64, 116);
			this.AreaButton.Margin = new System.Windows.Forms.Padding(4);
			this.AreaButton.Name = "AreaButton";
			this.AreaButton.Size = new System.Drawing.Size(59, 21);
			this.AreaButton.TabIndex = 18;
			this.AreaButton.TabStop = true;
			this.AreaButton.Text = "Area";
			this.AreaButton.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(9, 118);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(44, 17);
			this.label8.TabIndex = 17;
			this.label8.Text = "Draw:";
			// 
			// DrawButton
			// 
			this.DrawButton.Location = new System.Drawing.Point(492, 146);
			this.DrawButton.Margin = new System.Windows.Forms.Padding(4);
			this.DrawButton.Name = "DrawButton";
			this.DrawButton.Size = new System.Drawing.Size(100, 28);
			this.DrawButton.TabIndex = 16;
			this.DrawButton.Text = "Draw";
			this.DrawButton.UseVisualStyleBackColor = true;
			this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
			// 
			// ShortBox
			// 
			this.ShortBox.AutoSize = true;
			this.ShortBox.Location = new System.Drawing.Point(379, 74);
			this.ShortBox.Margin = new System.Windows.Forms.Padding(4);
			this.ShortBox.Name = "ShortBox";
			this.ShortBox.Size = new System.Drawing.Size(64, 21);
			this.ShortBox.TabIndex = 15;
			this.ShortBox.Text = "Short";
			this.ShortBox.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 37);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(112, 17);
			this.label7.TabIndex = 6;
			this.label7.Text = "Max Generation:";
			// 
			// FullAnalysisMaxGenerationBox
			// 
			this.FullAnalysisMaxGenerationBox.Location = new System.Drawing.Point(141, 33);
			this.FullAnalysisMaxGenerationBox.Margin = new System.Windows.Forms.Padding(4);
			this.FullAnalysisMaxGenerationBox.Name = "FullAnalysisMaxGenerationBox";
			this.FullAnalysisMaxGenerationBox.Size = new System.Drawing.Size(449, 22);
			this.FullAnalysisMaxGenerationBox.TabIndex = 5;
			this.FullAnalysisMaxGenerationBox.Text = "3";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.TestIndexedButton);
			this.groupBox4.Controls.Add(this.ShiftPButton);
			this.groupBox4.Controls.Add(this.label10);
			this.groupBox4.Controls.Add(this.PShiftBox);
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.ShiftPGenerationBox);
			this.groupBox4.Location = new System.Drawing.Point(624, 107);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(477, 125);
			this.groupBox4.TabIndex = 18;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Shift P";
			// 
			// ShiftPButton
			// 
			this.ShiftPButton.Location = new System.Drawing.Point(370, 82);
			this.ShiftPButton.Margin = new System.Windows.Forms.Padding(4);
			this.ShiftPButton.Name = "ShiftPButton";
			this.ShiftPButton.Size = new System.Drawing.Size(100, 28);
			this.ShiftPButton.TabIndex = 23;
			this.ShiftPButton.Text = "Test Single";
			this.ShiftPButton.UseVisualStyleBackColor = true;
			this.ShiftPButton.Click += new System.EventHandler(this.ShiftPButton_Click);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(7, 52);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(53, 17);
			this.label10.TabIndex = 26;
			this.label10.Text = "P Shift:";
			// 
			// PShiftBox
			// 
			this.PShiftBox.Location = new System.Drawing.Point(97, 52);
			this.PShiftBox.Margin = new System.Windows.Forms.Padding(4);
			this.PShiftBox.Name = "PShiftBox";
			this.PShiftBox.Size = new System.Drawing.Size(373, 22);
			this.PShiftBox.TabIndex = 25;
			this.PShiftBox.Text = "1";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(7, 22);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(83, 17);
			this.label9.TabIndex = 24;
			this.label9.Text = "Generation:";
			// 
			// ShiftPGenerationBox
			// 
			this.ShiftPGenerationBox.Location = new System.Drawing.Point(97, 22);
			this.ShiftPGenerationBox.Margin = new System.Windows.Forms.Padding(4);
			this.ShiftPGenerationBox.Name = "ShiftPGenerationBox";
			this.ShiftPGenerationBox.Size = new System.Drawing.Size(373, 22);
			this.ShiftPGenerationBox.TabIndex = 23;
			this.ShiftPGenerationBox.Text = "3";
			// 
			// button2
			// 
			this.TestIndexedButton.Location = new System.Drawing.Point(10, 82);
			this.TestIndexedButton.Margin = new System.Windows.Forms.Padding(4);
			this.TestIndexedButton.Name = "button2";
			this.TestIndexedButton.Size = new System.Drawing.Size(100, 28);
			this.TestIndexedButton.TabIndex = 27;
			this.TestIndexedButton.Text = "Test Indexed";
			this.TestIndexedButton.UseVisualStyleBackColor = true;
			this.TestIndexedButton.Click += new System.EventHandler(this.TestIndexedButton_Click);
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1127, 464);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "TestForm";
			this.Text = "Form1";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
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
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button ShiftPButton;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox PShiftBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox ShiftPGenerationBox;
		private System.Windows.Forms.Button TestIndexedButton;
	}
}

