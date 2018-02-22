using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

  public Camera MainMenuCamera;

  GameManager manager;

  // Use this for initialization
  void Start () {
    manager = GameManager.instance;
  }
  
  // Update is called once per frame
  void Update () {
    // if (Input.GetKeyDown(KeyCode.Return)) {
    //   StartGame();
    // }
  }

  public void StartGame() {
    MainMenuCamera.depth = -1;
    manager.SwitchScene("test_scene");
  }
}
