using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalEffectBehavior : MonoBehaviour {

  public void __SwitchScene(string sceneName) {
    Debug.Log("Switching to scene '" + sceneName + "'");
    SceneManager.LoadSceneAsync(sceneName);
  }
}