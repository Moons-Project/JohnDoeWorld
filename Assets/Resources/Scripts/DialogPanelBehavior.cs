using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPanelBehavior : MonoBehaviour {

  GameManager manager;

  // Use this for initialization
  void Start () {
    manager = GameManager.instance;
  }
  
  // Update is called once per frame
  void Update () {
    
  }

  void OnMouseDown() {
    manager.dialogManager.OnDialogPress();
  }
}
