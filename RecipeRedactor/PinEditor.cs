using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RecipeRedactor
{
  public partial class PinEditor : Form
  {
    public PinEditor()
    {
      InitializeComponent();
    }

    public struct PinName
    {
      public string Name;
      public int Pin;
    }

    public static List<PinName> Pins = new List<PinName>(
      new []
      {
        new PinName()
        {
          Name = "1",
          Pin = 1
        },
        new PinName()
        {
          Name = "2",
          Pin = 2
        },
        new PinName()
        {
        Name = "3",
        Pin = 3
        },
        new PinName()
        {
          Name = "4",
          Pin = 4
        },
        new PinName()
        {
          Name = "5",
          Pin = 5
        },
        new PinName()
        {
          Name = "6",
          Pin = 6
        }
      });

    private void button1_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void PinEditor_Load(object sender, EventArgs e)
    {
      textBox1.Text = Pins[0].Name;
      numericUpDown1.Value = Pins[0].Pin;
      textBox2.Text = Pins[1].Name;
      numericUpDown2.Value = Pins[1].Pin;
      textBox3.Text = Pins[2].Name;
      numericUpDown3.Value = Pins[2].Pin;
      textBox4.Text = Pins[3].Name;
      numericUpDown4.Value = Pins[3].Pin;
      textBox5.Text = Pins[4].Name;
      numericUpDown5.Value = Pins[4].Pin;
      textBox6.Text = Pins[5].Name;
      numericUpDown6.Value = Pins[5].Pin;
    }

    private void PinEditor_FormClosing(object sender, FormClosingEventArgs e)
    {
      Pins.Clear();
      Pins.Add(new PinName()
      {
        Name = textBox1.Text,
        Pin = (int)numericUpDown1.Value
      });
      Pins.Add(new PinName()
      {
        Name = textBox2.Text,
        Pin = (int)numericUpDown2.Value
      });
      Pins.Add(new PinName()
      {
        Name = textBox3.Text,
        Pin = (int)numericUpDown3.Value
      });
      Pins.Add(new PinName()
      {
        Name = textBox4.Text,
        Pin = (int)numericUpDown4.Value
      });
      Pins.Add(new PinName()
      {
        Name = textBox5.Text,
        Pin = (int)numericUpDown5.Value
      });
      Pins.Add(new PinName()
      {
        Name = textBox6.Text,
        Pin = (int)numericUpDown6.Value
      });
    }
  }
}
