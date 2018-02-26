using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveDataManager {
  private static SaveDataManager _instance;
  public static SaveDataManager instance {
    get {
      if (_instance == null) {
        _instance = new SaveDataManager();
      }
      return _instance;
    }
  }

  public void toJson() {
    BasicInfo info = new BasicInfo(1f, 2f, 3f, 4f, 5f);
    Debug.Log(info);

    string json = JsonConvert.SerializeObject(info);
    Debug.Log(json);

    info = JsonConvert.DeserializeObject<BasicInfo>(json);
    Debug.Log(info);
  }
}