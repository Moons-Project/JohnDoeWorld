using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour, Interactable {
  public string itemToGetIdName;
  public string SoundEffect;
  bool triggered = false;
  
  public void Interact(Creature source) {
    if (triggered) return;
    if (source.CompareTag("ControlPlayer")) {
      triggered = true;
      GameManager.instance.musicManager.PlaySE(SoundEffect);
      GameManager.instance.inventoryManager.AddItem(
        GameManager.instance.jsonManager.itemDict[itemToGetIdName]
        );
    }
  }
}
