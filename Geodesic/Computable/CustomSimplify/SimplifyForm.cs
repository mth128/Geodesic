using Geodesic.Computable.CustomSimplify;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computable
{
  public partial class SimplifyForm : Form
  {
    public static int Instances { get; private set; } = 0; 

    List<Equation> equations = new List<Equation>();
    private Equation startEquation;
    public Equation CustomEquation; 


    public SimplifyForm()
    {
      Instances++;
      InitializeComponent();
      Prime.InitializePrimes();
    }

    public SimplifyForm(IValue startEquation)
    {
      Instances++;
      InitializeComponent();
      Prime.InitializePrimes();
      this.startEquation = new Equation(startEquation);
      SourceValueBox.Text = startEquation.Value.ToString();
      SourceEquationBox.Text = startEquation.Equation; 
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
      try
      {
        Equation equation = new Equation(InputBox.Text);
        Add(equation); 
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message + "[Equation: "+InputBox.Text +"]","Error"); 
      }
    }

    private void Add(IValue value)
    {
      Equation equation = new Equation(value); 
      OutputBox.Text = equation.ToString(); 
      equations.Add(equation);
      UpdateLists(); 
    }

    private void UpdateLists()
    {
      ABox.Items.Clear();
      BBox.Items.Clear(); 
      foreach (Equation equation in equations)
      {
        string s = equation.ToString(); 
        ABox.Items.Add(s);
        BBox.Items.Add(s);
      }
      if (equations.Count >0)
      {
        ABox.SelectedIndex = equations.Count - 1;
        BBox.SelectedIndex = equations.Count - 1; 
      }
    }

    public Equation GetA()
    {
      return equations[ABox.SelectedIndex];
    }
    public Equation GetB()
    {
      return equations[BBox.SelectedIndex];
    }

    private void NegateButton_Click(object sender, EventArgs e)
    {
      try
      {
        Add(GetA().Negate());
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error"); 
      }

    }

    private void SquareButton_Click(object sender, EventArgs e)
    {
      try
      {
        Add(GetA().Squared()); 
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error");
      }
    }

    private void SqrtButton_Click(object sender, EventArgs e)
    {
      try
      {
        Add(MathE.Sqrt(GetA()));
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error");
      }
    }

    private void PlusButton_Click(object sender, EventArgs e)
    {
      try
      {
        Add(new Sum(GetA(),GetB()).Simple());
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error");
      }
    }

    private void MinusButton_Click(object sender, EventArgs e)
    {
      try
      {
        Add(new Sum(GetA(), GetB().Negate()).Simple());
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error");
      }
    }

    private void MultiplyButton_Click(object sender, EventArgs e)
    {
      try
      {
        Add(new Product(GetA(), GetB()).Simple());
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error");
      }
    }

    private void DivideButton_Click(object sender, EventArgs e)
    {
      try
      {
        Add(new Fraction(GetA(), GetB()).Simple());
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error");
      }
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
      try
      {
        int index = ABox.SelectedIndex;
        equations.RemoveAt(index);
        UpdateLists();
        if (index < ABox.Items.Count)
        {
          ABox.SelectedIndex = index;
          BBox.SelectedIndex = index;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error");
      }
    }

    private void OK_Button_Click(object sender, EventArgs e)
    {
      if (this.startEquation == null)
      {
        MessageBox.Show("There is no startequation");
        return;
      }
      CustomEquation = GetA();

      if(CustomEquation.EqualsClosely(startEquation))
      {
        DialogResult = DialogResult.OK;
        this.Close();  
      }
      else
      {
        MessageBox.Show(startEquation.Value.ToString() + " does not equal " + CustomEquation.Value.ToString());
      }
    }

    private void Cancel_Button_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      this.Close(); 
    }

    private void AcceptButton_Click(object sender, EventArgs e)
    {
      if (this.startEquation == null)
      {
        MessageBox.Show("There is no startequation");
        return;
      }
      CustomEquation = startEquation;
      DialogResult = DialogResult.OK;
      this.Close();
    }

    private void SimplifyForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Instances--; 
    }

    private void DebugButton_Click(object sender, EventArgs e)
    {
      int i = 1;
    }

    private void ShowCurruntButton_Click(object sender, EventArgs e)
    {
      using (StorageForm storageForm = new StorageForm())
      {
        string text = "";
        List<KeyValuePair<string, Equation>> pairs = new List<KeyValuePair<string, Equation>>();
        foreach (KeyValuePair<string, Equation> pair in CustomSimplifyStorage.main.library)
          pairs.Add(pair); 
        pairs = pairs.OrderBy(p => p.Value.Value).ToList(); 

        foreach (KeyValuePair<string, Equation> pair in pairs)
          text += pair.Value.ToString() + " <--- " + pair.Key.ToString() + "\r\n";
        storageForm.TextBox.Text = text;
        storageForm.ShowDialog(); 
      }
    }
  }
}
