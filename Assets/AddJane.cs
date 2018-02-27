using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddJane : MonoBehaviour {
  public GameObject jane;

  public int progress;
  // Use this for initialization
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == "ControlPlayer" &&
      GameManager.instance.saveDataManager.saveData.progress == progress) {
      GameManager.instance.scriptManager.FinishedEvent += showJane;
    }
  }

  void showJane() {
    jane.SetActive(true);
    GameManager.instance.scriptManager.FinishedEvent -= showJane;
  }
}