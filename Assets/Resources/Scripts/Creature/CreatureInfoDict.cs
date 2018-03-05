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
  private static string ITEM_INFOS_PATH = "Jsons/creature_infos";

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
      string json = (Resources.Load(path, typeof(TextAsset)) as TextAsset).text;
      itemDict = new Dictionary<string, CreatureInfo>();
      CreatureInfos infos = JsonUtility.FromJson<CreatureInfos>(json);
      foreach (var i in infos.data) {
        itemDict.Add(i.idName, i);
      }
  }

  private CreatureInfoDict() {
    FromJson(ITEM_INFOS_PATH);
  }
}