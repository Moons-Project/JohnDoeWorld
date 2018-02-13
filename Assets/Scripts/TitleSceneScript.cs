using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneScript : MonoBehaviour {

  private AsyncOperation operation;

  // Use this for initialization
  void Start () {
    
  }
  
  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown(KeyCode.Return)) {
      operation = SceneManager.LoadSceneAsync("TestScene");
    }
    if (operation != null) {
      Debug.Log(operation.progress);
    }
  }
}
