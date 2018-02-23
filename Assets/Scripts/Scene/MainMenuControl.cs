using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

  public Camera MainMenuCamera;
  public GameObject MainPanel;
  public GameObject SettingPanel;
  public GameObject HelpPanel;

  GameManager manager;

  // Use this for initialization
  void Start() {
    manager = GameManager.instance;
    manager.musicManager.PlayBGM("title_bgm");
  }

  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown(KeyCode.A)) {
      manager.musicManager.PlaySE("main_menu_hover");
    }
    if (Input.GetKeyDown(KeyCode.B)) {
      manager.musicManager.StopBGM();
    }
    if (Input.GetKeyDown(KeyCode.N)) {
      manager.musicManager.PlayBGM("title_bgm");
    }
    if (Input.GetKeyDown(KeyCode.P)) {
      manager.SwitchScene("scene_1");
    }
  }

  public void StartGame() {
    MainMenuCamera.depth = -1;
    manager.SwitchScene("test_scene");
  }

  public void Setting() {
    MainPanel.SetActive(false);
    SettingPanel.SetActive(true);
  }

  public void ExitSetting() {
    MainPanel.SetActive(true);
    SettingPanel.SetActive(false);
  }

  public void Help() {
    MainPanel.SetActive(false);
    HelpPanel.SetActive(true);
  }

  public void ExitHelp() {
    MainPanel.SetActive(true);
    HelpPanel.SetActive(false);
  }

  public void Exit() {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }
}