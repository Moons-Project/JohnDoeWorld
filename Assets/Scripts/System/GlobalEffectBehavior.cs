using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalEffectBehavior : MonoBehaviour {

  public void __SwitchScene(string sceneName) {
    Debug.Log("Switching to scene '" + sceneName + "'");
    SceneManager.LoadSceneAsync(sceneName);
  }

  public void __SwitchScene(object clip) {
    AnimationClip clipReal = clip as AnimationClip;
    clipReal.events = new AnimationEvent[0];
  }
}