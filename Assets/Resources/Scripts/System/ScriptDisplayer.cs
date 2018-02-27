using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDisplayer : MonoBehaviour {
  public int progress = 0;
  public string scriptName = "";

  // Use this for initialization
  void Start () {
    
  }
  
  // Update is called once per frame
  void Update () {
    
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == "ControlPlayer" && 
        GameManager.instance.saveDataManager.saveData.progress == progress) {
          other.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
          GameManager.instance.scriptManager.PlayScript(scriptName);
    }
  }
}
