using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveDataManager {

  private string SAVE_DATA_PATH = Application.persistentDataPath + "/SaveData.json";
  public class SaveData {
    // remember to set dafault value
    public BasicInfo info = new BasicInfo(1f, 2f, 3f, 4f, 5f);
  }

  public SaveData saveData;
  public bool hasSaved { get { return File.Exists(SAVE_DATA_PATH); } }

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

  public void Clear() {
    saveData = new SaveData();
    Debug.Log("create new data");
  }

  public void Read() {
    if (hasSaved) {
      using(FileStream file = File.Open(SAVE_DATA_PATH, FileMode.Open, FileAccess.Read)) {
        using(StreamReader reader = new StreamReader(file)) {
          saveData = JsonConvert.DeserializeObject<SaveData>(reader.ReadToEnd());
          if (saveData != null) {
            Debug.Log("read old data");
          } else {
            Debug.Log("Error: read old data");
            Clear();
          };
        }
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