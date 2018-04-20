using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Control : MonoBehaviour {

  public GameObject Singleton_;

  void SpawnSingleton(int progress) {
    if (progress == 13 || progress == 10)
      Singleton_.SetActive(true);
  }

  void Awake() {
    ScriptDisplayer.OnScriptDisplay += SpawnSingleton;
  }
  
  void OnDisable() {
    ScriptDisplayer.OnScriptDisplay -= SpawnSingleton;
  }
}
