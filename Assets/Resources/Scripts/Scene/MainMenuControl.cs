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

  private int count = 0;
  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown(KeyCode.A)) {
      ++count;
      string text = "这是第 " + count + " 句话";
      manager.dialogManager.ShowDialog("系统", text);
    }
    if (Input.GetKeyDown(KeyCode.B)) {
      manager.dialogManager.ShowDialog(null, "这是一句很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长的话");
    }
    if (Input.GetKeyDown(KeyCode.C)) {
      manager.dialogManager.SkipDialog();
    }
    if (Input.GetKeyDown(KeyCode.D)) {
      manager.dialogManager.ShowDialog("Jane", "测试JaneJaneJaneJaneJaneJane");
    }
    if (Input.GetKeyDown(KeyCode.E)) {
      manager.dialogManager.ShowDialog("John", "测试JohnJohnJohnJohnJohnJohn", DialogManager.WhichImage.RightImage);
    }
    if (Input.GetKeyDown(KeyCode.F)) {
      manager.dialogManager.HideDialog();
    }
  }

  public void StartGame() {
    // MainMenuCamera.depth = -1;
    manager.SwitchScene("scene_1");
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