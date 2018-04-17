using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePotionBehavior : MonoBehaviour {

  public float regenLife = 20f;
  public string seName = "main_menu_hover";

  // Use this for initialization
  void Start () {
    
  }
  
  // Update is called once per frame
  void Update () {
    
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("ControlPlayer")) {
      var creature = other.GetComponent<Creature>();
      GameManager.instance.musicManager.PlaySE(seName);
      creature.Regenerate(regenLife);
      Destroy(gameObject);
    }
  }
}
