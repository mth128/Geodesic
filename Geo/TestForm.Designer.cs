
namespace Geo
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
      this.IterateButton = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.BisectGenerationBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(14, 202);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Save OBJ";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // ValueBox
      // 
      this.ValueBox.Location = new System.Drawing.Point(62, 9);
      this.ValueBox.Margin = new System.Windows.Forms.Padding(2);
      this.ValueBox.Name = "ValueBox";
      this.ValueBox.Size = new System.Drawing.Size(338, 20);
      this.ValueBox.TabIndex = 1;
      this.ValueBox.TextChanged += new System.EventHandler(this.ValueBox_TextChanged);
      // 
      // AnswerBox
      // 
      this.AnswerBox.Location = new System.Drawing.Point(62, 32);
      this.AnswerBox.Margin = new System.Windows.Forms.Padding(2);
      this.AnswerBox.Name = "AnswerBox";
      this.AnswerBox.ReadOnly = true;
      this.AnswerBox.Size = new System.Drawing.Size(338, 20);
      this.AnswerBox.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(11, 9);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(34, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Input:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(10, 35);
      this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(42, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Output:";
      // 
      // GenerationBox
      // 
      this.GenerationBox.Location = new System.Drawing.Point(110, 69);
      this.GenerationBox.Name = "GenerationBox";
      this.GenerationBox.Size = new System.Drawing.Size(338, 20);
      this.GenerationBox.TabIndex = 5;
      this.GenerationBox.Text = "3";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(10, 72);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(62, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Generation:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(10, 135);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(63, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Point Index:";
      // 
      // PointIndexBox
      // 
      this.PointIndexBox.Location = new System.Drawing.Point(110, 132);
      this.PointIndexBox.Name = "PointIndexBox";
      this.PointIndexBox.Size = new System.Drawing.Size(338, 20);
      this.PointIndexBox.TabIndex = 7;
      this.PointIndexBox.TextChanged += new System.EventHandler(this.PointIndexBox_TextChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(10, 161);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(61, 13);
      this.label5.TabIndex = 10;
      this.label5.Text = "Coordinate:";
      // 
      // CoordinateBox
      // 
      this.CoordinateBox.Location = new System.Drawing.Point(110, 158);
      this.CoordinateBox.Name = "CoordinateBox";
      this.CoordinateBox.ReadOnly = true;
      this.CoordinateBox.Size = new System.Drawing.Size(338, 20);
      this.CoordinateBox.TabIndex = 9;
      // 
      // IterateButton
      // 
      this.IterateButton.Location = new System.Drawing.Point(14, 231);
      this.IterateButton.Name = "IterateButton";
      this.IterateButton.Size = new System.Drawing.Size(75, 23);
      this.IterateButton.TabIndex = 11;
      this.IterateButton.Text = "Iterate";
      this.IterateButton.UseVisualStyleBackColor = true;
      this.IterateButton.Click += new System.EventHandler(this.IterateButton_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(10, 98);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(94, 13);
      this.label6.TabIndex = 13;
      this.label6.Text = "Bisect Generation:";
      // 
      // BisectGenerationBox
      // 
      this.BisectGenerationBox.Location = new System.Drawing.Point(110, 95);
      this.BisectGenerationBox.Name = "BisectGenerationBox";
      this.BisectGenerationBox.Size = new System.Drawing.Size(338, 20);
      this.BisectGenerationBox.TabIndex = 12;
      this.BisectGenerationBox.Text = "3";
      // 
      // TestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.BisectGenerationBox);
      this.Controls.Add(this.IterateButton);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.CoordinateBox);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.PointIndexBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.GenerationBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.AnswerBox);
      this.Controls.Add(this.ValueBox);
      this.Controls.Add(this.button1);
      this.Name = "TestForm";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

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
    private System.Windows.Forms.Button IterateButton;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox BisectGenerationBox;
  }
}

