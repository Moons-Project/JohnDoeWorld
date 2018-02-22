using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

  // Use this for initialization
  void Start() {
    foreach (var item in GameManager.instance.skillDict.itemDict[1].damage) {
      Debug.Log(item);
    }
  }

  // Update is called once per frame
  void Update() {

  }
}