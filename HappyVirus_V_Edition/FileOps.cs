using System;
using System.Collections.Generic;
using System.IO;

namespace HappyVirus_V_Edition
{
  static public class FileOps
  {
    static public bool DoesPathExist(string path) { return (File.Exists(path) || Directory.Exists(path)); }
    static public void CreateFile(string path) { FileStream fs = File.Create(path); fs.Close(); }
    static public void CreateDirectory(string path) { Directory.CreateDirectory(path); }
    static public long GetFileSize(string path) { return (new FileInfo(path).Length); }
  }
}
