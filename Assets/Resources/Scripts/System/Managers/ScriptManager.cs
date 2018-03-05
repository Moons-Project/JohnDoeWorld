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
    string path = "Jsons/Scripts/" + scriptName;
    ScriptList scripts = JsonUtility.FromJson<ScriptList>((Resources.Load(path, typeof(TextAsset)) as TextAsset).text);
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
    // restart input
    FinishedEvent += () => {
      GameManager.instance.inputManager.RestartInput();
    };
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

    //disable input
    GameManager.instance.inputManager.DisableInput();

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
      FinishedEvent();
      return;
    }
    var script = playingScript[playingScriptIndex++];
    manager.dialogManager.ShowDialog(script.speaker, script.content, script.leftOrRight);
  }

  void OnDisable() {
    manager.dialogManager.DialogEnd -= NextScript;
  }

  public delegate void Finished();
  public event Finished FinishedEvent;

}
