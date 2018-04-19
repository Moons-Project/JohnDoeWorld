using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBehavior : Interactable {
  
  public string nextSceneName = "";
  public string doorName = "";
  void Awake() {
    if (gameObject.layer != LayerMask.NameToLayer("Interactable")) {
      Debug.LogError("Gameobject `" + gameObject.name + "` is not Interactable");
    }
  }

  void OnTriggerEnter2D(Collider2D other) {}

  public override void Interact(Creature source) {
    if (source.CompareTag("ControlPlayer")) {
      // 传送
      GameManager.instance.lastVDoorName = doorName;
      GameManager.instance.SwitchScene(nextSceneName);
    }
  }
}
