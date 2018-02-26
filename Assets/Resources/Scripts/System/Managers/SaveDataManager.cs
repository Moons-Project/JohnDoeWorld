using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveDataManager {
  public enum PlayerRoleType { John = 0, Jane = 1, Slarm = 2 };

  public class SaveData {
    public CreatureInfo[] creatureInfos = new CreatureInfo[3] { null, null, null };
    public PlayerRoleType playerRoleType = PlayerRoleType.Slarm;
  }

  public SaveData saveData;
  public bool hasSaved { get { return File.Exists(SAVE_DATA_PATH); } }

  private string SAVE_DATA_PATH = Application.persistentDataPath + "/SaveData.json";

  public SaveDataManager() {
    Read();
  }

  private static SaveDataManager _instance;
  public static SaveDataManager instance {
    get {
      if (_instance == null) {
        _instance = new SaveDataManager();
      }
      return _instance;
    }
  }

  public GameObject GetPlayerObj(GameObject obj) {
    var type = saveData.playerRoleType;
    return GetPlayerObj(type, obj);
  } 

  public GameObject GetPlayerObj(PlayerRoleType type, GameObject obj) {
    CreatureInfo info = saveData.creatureInfos[(int) type] == null ? 
                        GameManager.instance.creatureInfoDict.itemDict[Enum.GetName(type.GetType(), type)] :
                        saveData.creatureInfos[(int) type];
    return info.ApplyTo(obj);
  }

  public void Clear() {
    saveData = new SaveData();
    Debug.Log("create new data");
  }

  public void Read() {
    if (hasSaved) {
      try {
        using(FileStream file = File.Open(SAVE_DATA_PATH, FileMode.Open, FileAccess.Read)) {
          using(StreamReader reader = new StreamReader(file)) {
            saveData = JsonConvert.DeserializeObject<SaveData>(reader.ReadToEnd());
            if (saveData != null)
              Debug.Log("read old data");
            else
              throw new IOException("read old data error");
          };
        }
      } catch {
        Debug.Log("Error: read old data");
        Clear();
      }

    } else {
      Clear();
    }
  }

  public void Save() {
    using(FileStream file = File.Open(SAVE_DATA_PATH, FileMode.OpenOrCreate, FileAccess.Write)) {
      using(StreamWriter writer = new StreamWriter(file)) {
        writer.Write(JsonConvert.SerializeObject(saveData));
        Debug.Log("save data in " + SAVE_DATA_PATH);
      }
    }
  }
}