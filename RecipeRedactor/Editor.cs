using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RecipeRedactor
{
  public partial class Editor : Form
  {
    public Editor()
    {
      InitializeComponent();
    }

    private void Editor_Load(object sender, EventArgs e)
    {
      label1.Text = PinEditor.Pins[0].Name;
      label2.Text = PinEditor.Pins[1].Name;
      label3.Text = PinEditor.Pins[2].Name;
      label4.Text = PinEditor.Pins[3].Name;
      label5.Text = PinEditor.Pins[4].Name;
      label6.Text = PinEditor.Pins[5].Name;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
