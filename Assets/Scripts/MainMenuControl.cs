using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

  GameManager manager;

  // Use this for initialization
  void Start () {
    manager = GameManager.instance;
  }
  
  // Update is called once per frame
  void Update () {
    
  }

  public void StartGame() {
    manager.SwitchScene("test_scene");
  }
}
