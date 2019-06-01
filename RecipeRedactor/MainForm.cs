using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RecipeRedactor
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    public class Recipe
    {
      public PictureBox PictureBox;
      public TextBox TextBox;
      public Panel Panel;
      public int Pin;
      public int[] Amount;
    }

    private List<Recipe> Resipes = new List<Recipe>();

    private void Form1_Load(object sender, EventArgs e)
    {
      Resipes.Add(new Recipe()
      {
        Panel = panel1,
        TextBox = textBox1,
        PictureBox = pictureBox1,
        Amount = new int[6]
      });
      Resipes.Add(new Recipe()
      {
        Panel = panel2,
        TextBox = textBox2,
        PictureBox = pictureBox2,
        Amount = new int[6]
      });
      Resipes.Add(new Recipe()
      {
        Panel = panel3,
        TextBox = textBox3,
        PictureBox = pictureBox3,
        Amount = new int[6]
      });
      Resipes.Add(new Recipe()
      {
        Panel = panel4,
        TextBox = textBox4,
        PictureBox = pictureBox4,
        Amount = new int[6]
      });
      Resipes.Add(new Recipe()
      {
        Panel = panel5,
        TextBox = textBox5,
        PictureBox = pictureBox5,
        Amount = new int[6]
      });
      Resipes.Add(new Recipe()
      {
        Panel = panel6,
        TextBox = textBox6,
        PictureBox = pictureBox6,
        Amount = new int[6]
      });
    }

    private void panel1_DoubleClick(object sender, EventArgs e)
    {
      int n = int.Parse((sender as Control).Tag.ToString()) - 1;
      Editor ed = new Editor();
      ed.numericUpDown1.Value = Resipes[n].Amount[0];
      ed.numericUpDown2.Value = Resipes[n].Amount[1];
      ed.numericUpDown3.Value = Resipes[n].Amount[2];
      ed.numericUpDown4.Value = Resipes[n].Amount[3];
      ed.numericUpDown5.Value = Resipes[n].Amount[4];
      ed.numericUpDown6.Value = Resipes[n].Amount[5];
      ed.nudPin.Value = Resipes[n].Pin;
      ed.ShowDialog();
      Resipes[n].Amount[0] = (int)ed.numericUpDown1.Value;
      Resipes[n].Amount[1] = (int)ed.numericUpDown2.Value;
      Resipes[n].Amount[2] = (int)ed.numericUpDown3.Value;
      Resipes[n].Amount[3] = (int)ed.numericUpDown4.Value;
      Resipes[n].Amount[4] = (int)ed.numericUpDown5.Value;
      Resipes[n].Amount[5] = (int)ed.numericUpDown6.Value;
      Resipes[n].Pin = (int)ed.nudPin.Value;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      PinEditor pe = new PinEditor();
      pe.ShowDialog();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      saveFileDialog.ShowDialog();
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      openFileDialog.ShowDialog();
    }

    private void openFileDialog_FileOk(object sender, CancelEventArgs e)
    {
      FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
      BinaryReader F = new BinaryReader(fs, Encoding.ASCII);
      char[] buf;
      buf = F.ReadChars(11);
      if (new string(buf) != "ALCOBOTv0.3")
        return;
      PinEditor.Pins.Clear();
      for (int i = 0; i < 6; i++)
      {
        buf = F.ReadChars(255);

        PinEditor.Pins.Add(new PinEditor.PinName()
        {
          Name = new string(buf),
          Pin = F.ReadByte()
        });
      }
      for (int i = 0; i < 6; i++)
      {
        buf = F.ReadChars(255);
        Resipes[i].TextBox.Text = new string(buf);
        for (int j = 0; j < 6; j++)
        {
          Resipes[i].Amount[j] = F.ReadUInt16();
        }
        Resipes[i].Pin = F.ReadByte();
      }
    }

    private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
    {
      FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
      BinaryWriter F = new BinaryWriter(fs, Encoding.ASCII);
      
      F.Write("ALCOBOTv0.3".ToCharArray());
      // write pins
      foreach (PinEditor.PinName pin in PinEditor.Pins)
      {
        string name = pin.Name;
        while (name.Length < 255)
          name += '\0';
        F.Write(name.ToCharArray());
        F.Write((byte)pin.Pin);
      }
      foreach (var resipe in Resipes)
      {
        string name = resipe.TextBox.Text;
        while (name.Length < 255)
          name += '\0';
        F.Write(name.ToCharArray());
        foreach (int i in resipe.Amount)
        {
          F.Write((ushort)i);
        }
        F.Write((byte)resipe.Pin);
      }
      F.Close();
      fs.Close();
    }
  }
}
