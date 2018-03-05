using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager {

  [System.Serializable]
  public class Skills {
    public Skill[] data;
  }

  [System.Serializable]
  public class CreatureInfos {
    public CreatureInfo[] data;
  }

  [System.Serializable]
  public class ItemInfos {
    public Item[] itemData;
    public Equipment[] equipmentData;
  }

  public Dictionary<string, Skill> skillDict;
  public Dictionary<string, CreatureInfo> creatureInfoDict;
  public Dictionary<string, Item> itemDict;
  public Dictionary<string, Sprite> spriteDict;
  private static string ITEM_INFOS_PATH = "Jsons/item_infos";
  private static string CREATURE_INFOS_PATH = "Jsons/creature_infos";
  private static string SKILL_DICT_PATH = "Jsons/skill_infos";
  private static string SPRITE_DICT_PATH = "Sprites/items";

  private static JsonManager _instance;
  public static JsonManager instance {
    get {
      if (_instance == null) {
        _instance = new JsonManager();
      }
      return _instance;
    }
  }

  private void LoadSkillDict(string path) {
    string json =  (Resources.Load(path, typeof(TextAsset)) as TextAsset).text;

    skillDict = new Dictionary<string, Skill>();
    Skills infos = JsonUtility.FromJson<Skills>(json);
    foreach (var i in infos.data) {
      skillDict.Add(i.idName, i);
    }
  }

  private void LoadCreatureInfoDict(string path) {
      string json = (Resources.Load(path, typeof(TextAsset)) as TextAsset).text;
      creatureInfoDict = new Dictionary<string, CreatureInfo>();
      CreatureInfos infos = JsonUtility.FromJson<CreatureInfos>(json);
      foreach (var i in infos.data) {
        creatureInfoDict.Add(i.idName, i);
      }
  }

  private void LoadItemDict(string path) {
    string json = (Resources.Load(path, typeof(TextAsset)) as TextAsset).text;

    itemDict = new Dictionary<string, Item>();
    ItemInfos infos = JsonUtility.FromJson<ItemInfos>(json);
    foreach (var i in infos.itemData) {
      itemDict.Add(i.idName, i);
    }
    foreach (var i in infos.equipmentData) {
      itemDict.Add(i.idName, i);
    }
  }

  private void LoadSpriteDict(string path) {
    spriteDict = new Dictionary<string, Sprite>();

    var sprites = Resources.LoadAll<Sprite>(path);
    foreach (var sp in sprites) {
      spriteDict.Add(sp.name, sp);
    }
  }


  private JsonManager() {
    LoadSkillDict(SKILL_DICT_PATH);
    LoadCreatureInfoDict(CREATURE_INFOS_PATH);
    LoadItemDict(ITEM_INFOS_PATH);
    LoadSpriteDict(SPRITE_DICT_PATH);
  }
}