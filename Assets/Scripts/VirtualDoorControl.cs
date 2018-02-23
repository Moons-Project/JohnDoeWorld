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

  private GameManager manager;

  // Use this for initialization
  void Start () {
    manager = GameManager.instance;
  }
  
  // Update is called once per frame
  void Update () {
    
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (doorType != VDoorType.Hit) return;
    if (other.tag == "ControlPlayer") {
      manager.lastVDoorName = doorName;
      manager.SwitchScene(nextSceneName);
    }
  }
}
