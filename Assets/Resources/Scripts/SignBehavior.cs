using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBehavior : VirtualDoorControl, Interactable {
  
  void Awake() {
    if (gameObject.layer != LayerMask.NameToLayer("Interactable")) {
      Debug.LogError("Gameobject `" + gameObject.name + "` is not Interactable");
    }
  }

  public void Interact(Creature source) {
    if (source.CompareTag("ControlPlayer")) {
      // 传送
      GameManager.instance.lastVDoorName = doorName;
      GameManager.instance.SwitchScene(nextSceneName);
    }
  }

  void OnTriggerEnter2D(Collider2D other) {}
}
