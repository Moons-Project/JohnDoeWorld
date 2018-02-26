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
  public enum ScriptType {
    None,
    ChangeToScene1ToJohn
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

  void ChangeToScene1ToJohn() {
    manager.SwitchScene("scene_1");
    manager.saveDataManager.saveData.playerRoleType = SaveDataManager.PlayerRoleType.John;
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (doorType != VDoorType.Hit) return;
    if (0 != progreess) {
      Default(other);
    } else {
      switch (scriptType) {
        case ScriptType.None:
          Default(other);
          break;
        case ScriptType.ChangeToScene1ToJohn:
          ChangeToScene1ToJohn();
          ToDefault();
          break;
      }
    }
  }

  void Default(Collider2D other) {
    if (other.tag == "ControlPlayer") {
      manager.lastVDoorName = doorName;
      // Debug.Log("<color=red>Fatal error:</color>" + nextSceneName);
      manager.SwitchScene(nextSceneName);
    }
  }

  void ToDefault() {
    scriptType = ScriptType.None;
  }
}