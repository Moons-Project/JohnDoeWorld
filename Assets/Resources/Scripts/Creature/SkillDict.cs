using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkillDict {

  [System.Serializable]
  public class SkillItems {
    public Skill[] data;
  }

  public Dictionary<string, Skill> itemDict;
  public Dictionary<string, Sprite> spriteDict;
  private static string ITEM_INFOS_PATH = "Jsons/skill_infos";

  private static SkillDict _instance;
  public static SkillDict instance {
    get {
      if (_instance == null) {
        _instance = new SkillDict();
      }
      return _instance;
    }
  }

  private void FromJson(string path) {
    string json =  (Resources.Load(path, typeof(TextAsset)) as TextAsset).text;

    itemDict = new Dictionary<string, Skill>();
    SkillItems infos = JsonUtility.FromJson<SkillItems>(json);
    foreach (var i in infos.data) {
      itemDict.Add(i.idName, i);
    }
  }

  private void ImportResources() {
    // WIP
  }

  private SkillDict() {
    FromJson(ITEM_INFOS_PATH);
    ImportResources();
  }
}