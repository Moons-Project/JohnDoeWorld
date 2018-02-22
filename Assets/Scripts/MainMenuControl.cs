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
    manager.musicManager.PlayBGM("title_bgm");
  }
  
  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown(KeyCode.A)) {
      manager.musicManager.PlaySE("main_menu_hover");
    }
    if (Input.GetKeyDown(KeyCode.B)) {
      manager.musicManager.StopBGM();
    }
    if (Input.GetKeyDown(KeyCode.N)) {
      manager.musicManager.PlayBGM("title_bgm");
    }
  }

  public void StartGame() {
    MainMenuCamera.depth = -1;
    manager.SwitchScene("test_scene");
  }
}
