using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalEffectBehavior : MonoBehaviour {

  private GameManager manager;

  void Start() {
    manager = GameManager.instance;
  }

  public void RemoveEventByFunctionName(AnimationClip clip, string functionName) {
    Debug.Log("Removing Event " + functionName);
    List<UnityEngine.AnimationEvent> events = new List<UnityEngine.AnimationEvent>();
    foreach (var item in clip.events) {
      if (item.functionName != functionName) {
        events.Add(item);
      }
    }
    clip.events = events.ToArray();
    for (int i = 0; i < clip.events.Length; ++i) {
      Debug.Log(i + " " + clip.events[i].functionName);
    }
  }

  public void __SwitchScene(UnityEngine.AnimationEvent evt) {
    string sceneName = evt.stringParameter;
    object clip = evt.objectReferenceParameter;
    
    Debug.Log("Switching to scene '" + sceneName + "'");
    SceneManager.LoadSceneAsync(sceneName);
    AnimationClip clipReal = clip as AnimationClip;
    RemoveEventByFunctionName(clipReal, "__SwitchScene");
  }

  public void EffectEnd() {
    Debug.Log("Effect End");
    manager.globalEffectManager.effectCamera.depth = -10;
  }
}