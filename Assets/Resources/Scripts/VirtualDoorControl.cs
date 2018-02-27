using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualDoorControl : MonoBehaviour {

  public string nextSceneName = "";
  public string doorName = "";

  public enum VDoorType {
    Hit,
    Control
  }
  public VDoorType doorType = VDoorType.Hit;

  public int progreess = 0;
  public bool isDoor = true;
  public enum ScriptType {
    None,
    ChangeToScene1ToJohn,
    PleaseGoNextWay,
    ChangeToSene5ToJane,
    ChangeToScene3ToJohn
  }
  public ScriptType scriptType = ScriptType.None;

  private GameManager manager;

  // Use this for initialization
  void Start() {
    manager = GameManager.instance;
  }

  // Update is called once per frame
  void Update() {

  }

  void ChangeToScene3ToJohn() {
    manager.SwitchScene("scene_3");
    manager.lastVDoorName = "scene_2_right";
    manager.saveDataManager.saveData.playerRoleType = SaveDataManager.PlayerRoleType.John;
    manager.saveDataManager.Save("scene_3");
    manager.scriptManager.FinishedEvent -= ChangeToScene3ToJohn;
  }

  void ChangeToScene1ToJohn() {
    manager.SwitchScene("scene_1");
    manager.lastVDoorName = doorName;
    manager.saveDataManager.saveData.playerRoleType = SaveDataManager.PlayerRoleType.John;
    manager.saveDataManager.Save("scene_1");
    
  }

  void ChangeToSene5ToJane() {
    manager.SwitchScene("scene_5");
    manager.lastVDoorName = doorName;
    manager.saveDataManager.saveData.playerRoleType = SaveDataManager.PlayerRoleType.Jane;
    manager.saveDataManager.Save("scene_5");
  }

  void PleaseGoNextWay() {
    manager.saveDataManager.saveData.progress --;
    manager.scriptManager.PlayScript("next_way");
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (doorType != VDoorType.Hit) return;
    if (other.gameObject.tag == "ControlPlayer")
      other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

    if (manager.saveDataManager.saveData.progress != progreess) {
      if (isDoor) Default(other);
    } else {
      switch (scriptType) {
        case ScriptType.None:
          Default(other);
          break;
        case ScriptType.ChangeToScene1ToJohn:
          ChangeToScene1ToJohn();
          break;
        case ScriptType.PleaseGoNextWay:
          PleaseGoNextWay();
          break;
        case ScriptType.ChangeToSene5ToJane:
          ChangeToSene5ToJane();
          break;
        case ScriptType.ChangeToScene3ToJohn:
          manager.scriptManager.FinishedEvent += ChangeToScene3ToJohn;
          manager.scriptManager.PlayScript(progreess.ToString());
          break;
      }
    }
  }

  void Default(Collider2D other) {
    if (other.tag == "ControlPlayer") {
      manager.lastVDoorName = doorName;
      // Debug.Log("<color=red>Fatal error:</color>" + nextSceneName);
      manager.SwitchScene(nextSceneName);
      // save data
      GameManager.instance.saveDataManager.Save(nextSceneName, other.gameObject);
      ToDefault();
    }
  }

  void ToDefault() {
    scriptType = ScriptType.None;
  }
}