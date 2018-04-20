using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWallBehavior : MonoBehaviour {
  public string hint = "前面有一只强大的魔物，现在的你还无法打败它";

  void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.CompareTag("ControlPlayer")) {
      GameManager.instance.dialogManager.SystemDialog(hint);
    }
  }
}
