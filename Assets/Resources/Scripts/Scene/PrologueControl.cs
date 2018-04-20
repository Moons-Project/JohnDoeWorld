using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueControl : MonoBehaviour {

  public Camera MainCamera;
  public TypewriterEffect typewriter;

  [System.Serializable]
  public class PrologueScripts {
    public string[] data;
  }

  PrologueScripts scripts;
  
  int curScriptIndex = 0;

  void SkipOrNext() {
    if (typewriter.typing) {
      typewriter.Skip();
    } else {
      Debug.Log("Cur script index " + curScriptIndex);
      Debug.Log("Length: " + scripts.data.Length);
      if (curScriptIndex < scripts.data.Length) {
        Debug.Log(scripts.data[curScriptIndex]);
        typewriter.SetText(scripts.data[curScriptIndex], false);
        ++curScriptIndex;
      } else {
        GameManager.instance.SwitchScene("scene_3-1");
      }
    }
  }

  void SkipToScene() {
    GameManager.instance.SwitchScene("scene_3-1");    
  }

  void Awake() {
    InputManager.instance.PressJump += SkipToScene;
    string json = (Resources.Load("Jsons/Scripts/prologue", typeof(TextAsset)) as TextAsset).text;
    scripts = JsonUtility.FromJson<PrologueScripts>(json);
    Debug.Log("Awake");
    typewriter.TypeEnd += ()=> {
      StartCoroutine(_Next());
    };
  }

  void Start() {
    SkipOrNext();
  }

  IEnumerator _Next() {
    yield return new WaitForSeconds(1);
    SkipOrNext();
  }

  void OnDisable() {
    InputManager.instance.PressSubmit -= SkipOrNext;
    InputManager.instance.PressJump -= SkipToScene;
  }
}
