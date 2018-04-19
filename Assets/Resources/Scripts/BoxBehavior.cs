using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : Interactable {
  public string itemToGetIdName;
  public string SoundEffect;
  bool triggered = false;
  
  public override void Interact(Creature source) {
    if (triggered) return;
    if (source.CompareTag("ControlPlayer")) {
      triggered = true;
      GameManager.instance.musicManager.PlaySE(SoundEffect);
      var item = GameManager.instance.jsonManager.itemDict[itemToGetIdName];
      GameManager.instance.inventoryManager.AddItem(item);
      DialogManager.instance.SystemDialog("获得道具 " + item.name + " ！");
      Destroy(gameObject);
    }
  }
}
