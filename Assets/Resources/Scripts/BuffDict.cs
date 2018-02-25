using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class BuffDict {

  [System.Serializable]
  public class Buffs {
    public Buff[] data;
  }

  public Dictionary<string, Buff> itemDict;
  private static string ITEM_INFOS_PATH = "Assets/Resources/Jsons/buff_infos.json";

  private static BuffDict _instance;
  public static BuffDict instance {
    get {
      if (_instance == null) {
        _instance = new BuffDict();
      }
      return _instance;
    }
  }

  private void FromJson(string path) {
    string json = JDWUtility.ReadFileText(path);
    itemDict = new Dictionary<string, Buff>();
    Buffs infos = JsonUtility.FromJson<Buffs>(json);

    foreach (var i in infos.data) {
      if (i.shouldUpdate) {
        // Reflection to find OnUpdate
        Type t = Type.GetType(i.idName);
        Buff buff = Activator.CreateInstance(t, i) as Buff;
        itemDict.Add(i.idName, buff);
      } else {
        itemDict.Add(i.idName, i);
      }
    }
  }

  private BuffDict() {
    FromJson(ITEM_INFOS_PATH);
  }
}
