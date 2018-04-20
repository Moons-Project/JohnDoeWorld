using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddJane : MonoBehaviour {
  public GameObject jane;
  public Scene3Control scene3Control;
  public GameObject talker;

  public int progress;

  void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == "ControlPlayer" &&
      GameManager.instance.saveDataManager.saveData.progress == progress) {
      GameManager.instance.scriptManager.FinishedEvent += showJane;
    }
  }

  void showJane() {
    jane.SetActive(true);
    GameManager.instance.scriptManager.FinishedEvent -= showJane;
    jane.GetComponent<Creature>().OnDead += scene3Control.OnJaneDead;
    SaveDataManager.instance.saveData.progress--;
    talker.SetActive(false);
    SaveDataManager.instance.Save();
  }
}