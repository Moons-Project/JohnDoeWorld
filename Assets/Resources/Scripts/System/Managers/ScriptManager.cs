using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour {

  public static ScriptManager instance;

  private GameManager manager;

  [System.Serializable]
  public class Script {
    public string speaker;
    public string content;
    public DialogManager.WhichImage leftOrRight;
  }

  [System.Serializable]
  public class ScriptList {
    public Script[] data;
  }

  public static Script[] ScriptFromJson(string scriptName) {
    string path = "Assets/Resources/Jsons/Scripts/" + scriptName + ".json";
    string json = JDWUtility.ReadFileText(path);
    ScriptList scripts = JsonUtility.FromJson<ScriptList>(json);
    return scripts.data;
  }

  void Awake() {
    if (instance == null) {
      instance = this;
    }
  }

  void Start() {
    manager = GameManager.instance;
    manager.dialogManager.DialogEnd += NextScript;
  }

  public bool isPlaying = false;
  private Script[] playingScript;
  private int playingScriptIndex;

  public void PlayScript(string scriptName) {
    Debug.Log("Playing " + scriptName);
    PlayScript(ScriptFromJson(scriptName));
  }

  public void PlayScript(Script[] scripts) {
    // if (scripts.Length != 0) {
    //   isPlaying = true;
    // } else {
    //   isPlaying = false;
    // }
    isPlaying = true;
    playingScript = scripts;
    playingScriptIndex = 0;
    NextScript();
  }

  public void NextScript() {
    if (!isPlaying) {
      return;
    }
    if (playingScriptIndex >= playingScript.Length) {
      isPlaying = false;
      manager.dialogManager.HideDialog();
      return;
    }
    var script = playingScript[playingScriptIndex++];
    manager.dialogManager.ShowDialog(script.speaker, script.content, script.leftOrRight);
  }

  void OnDisable() {
    manager.dialogManager.DialogEnd -= NextScript;
  }
}
