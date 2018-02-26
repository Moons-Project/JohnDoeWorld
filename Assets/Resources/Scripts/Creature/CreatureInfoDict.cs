using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreatureInfoDict {

  [System.Serializable]
  public class CreatureInfos {
    public CreatureInfo[] data;
  }

  public Dictionary<string, CreatureInfo> itemDict;
  private static string ITEM_INFOS_PATH = "Assets/Resources/Jsons/creature_infos.json";

  private static CreatureInfoDict _instance;
  public static CreatureInfoDict instance {
    get {
      if (_instance == null) {
        _instance = new CreatureInfoDict();
      }
      return _instance;
    }
  }

  private void FromJson(string path) {
    using(StreamReader reader = new StreamReader(path)) {
      string json = reader.ReadToEnd();
      itemDict = new Dictionary<string, CreatureInfo>();
      CreatureInfos infos = JsonUtility.FromJson<CreatureInfos>(json);
      foreach (var i in infos.data) {
        itemDict.Add(i.idName, i);
      }
    }

  }

  public void ToJson() {
    using (StreamWriter writer = new StreamWriter(ITEM_INFOS_PATH + ".test")) {
      var itemDict = new Dictionary<string, CreatureInfo>();
      CreatureInfo info = new CreatureInfo();
      itemDict.Add(info.idName, info);
      writer.Write(JsonUtility.ToJson(info));
    }
  }

  private CreatureInfoDict() {
    FromJson(ITEM_INFOS_PATH);
  }
}