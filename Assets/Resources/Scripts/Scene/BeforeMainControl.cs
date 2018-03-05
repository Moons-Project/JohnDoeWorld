using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeMainControl : MonoBehaviour {

  // Use this for initialization
  void Start () {
    SceneManager.LoadScene("main_menu_scene");
  }
  
  // Update is called once per frame
  void Update () {
    
  }
}
