using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class BulletDict {

  [System.Serializable]
  public class Bullets {
    public BulletInfo[] data;
  }

  public Dictionary<string, BulletInfo> itemDict;
  private static string ITEM_INFOS_PATH = "Assets/Resources/Jsons/bullet_infos.json";

  public Dictionary<string, RuntimeAnimatorController> animatorControllerDict;

  private static BulletDict _instance;
  public static BulletDict instance {
    get {
      if (_instance == null) {
        _instance = new BulletDict();
      }
      return _instance;
    }
  }

  private void FromJson(string path) {
    string json = JDWUtility.ReadFileText(path);
    itemDict = new Dictionary<string, BulletInfo>();
    Bullets infos = JsonUtility.FromJson<Bullets>(json);

    foreach (var i in infos.data) {
      Debug.Log(i.idName);
      itemDict.Add(i.idName, i);
    }
  }

  private void ImportResources() {
    animatorControllerDict = new Dictionary<string, RuntimeAnimatorController>();

    var animators = Resources.LoadAll<RuntimeAnimatorController>("Animations/Bullet");
    foreach (var anim in animators) {
      Debug.Log(anim.name);
      animatorControllerDict.Add(anim.name, anim);
    }
  }

  private BulletDict() {
    FromJson(ITEM_INFOS_PATH);
    ImportResources();
  }
}
