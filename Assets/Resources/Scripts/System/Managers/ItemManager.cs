using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemManager {
  [System.Serializable]
  public class ItemInfo {
    public int id;
    public string idName;
    public string name;
    public string description;
  }

  [System.Serializable]
  public class ItemInfos {
    public ItemInfo[] data;
  }

  public Dictionary<int, ItemInfo> itemDict;
  public Dictionary<string, Sprite> spriteDict;
  private static string ITEM_INFOS_PATH = "Assets/Resources/Jsons/item_infos.json";

  private static ItemManager _instance;
  public static ItemManager instance {
    get {
      if (_instance == null) {
        _instance = new ItemManager();
      }
      return _instance;
    }
  }

  private void FromJson(string path) {
    StreamReader reader = new StreamReader(path);

    string json = reader.ReadToEnd();

    itemDict = new Dictionary<int, ItemInfo>();
    ItemInfos infos = JsonUtility.FromJson<ItemInfos>(json);
    foreach (var i in infos.data) {
      itemDict.Add(i.id, i);
    }
  }

  private void ImportResources() {
    spriteDict = new Dictionary<string, Sprite>();

    var sprites = Resources.LoadAll<Sprite>("Sprites/items");
    foreach (var sp in sprites) {
      spriteDict.Add(sp.name, sp);
    }
  }

  private ItemManager() {
    FromJson(ITEM_INFOS_PATH);
    ImportResources();
  }
}