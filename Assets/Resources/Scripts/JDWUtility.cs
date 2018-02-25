using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JDWUtility {
  public static string ReadFileText(string path) {
    StreamReader reader = new StreamReader(path);
    return reader.ReadToEnd();
  }
}