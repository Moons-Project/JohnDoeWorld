using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintAreaBehavior : MonoBehaviour {
  public string hint = "Hint here";
  
  void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("ControlPlayer")) {
      GameManager.instance.sysUIManager.ChangeHint(hint);
      GameManager.instance.sysUIManager.ShowHint();
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.CompareTag("ControlPlayer")) {
      GameManager.instance.sysUIManager.HideHint();
    }
  }
}
